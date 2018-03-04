using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebStore.Api.Controllers;
using WebStore.Api.Security;
using WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Infra.Transactions;

namespace ModernStore.Api.Controllers
{
    public class AccountController : BaseController
    {
        private Customer _customer;
        private readonly ICustomerRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        public AccountController(IOptions<TokenOptions> jwtOptions, IUow uow, ICustomerRepository repository) : base(uow)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [HttpPost]
        [Route("v1/authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm] AuthenticateUserCommand command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.Username),
                new Claim(JwtRegisteredClaimNames.NameId, command.Username),
                new Claim(JwtRegisteredClaimNames.Email, command.Username),
                new Claim(JwtRegisteredClaimNames.Sub, command.Username),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(),
                ClaimValueTypes.Integer64), identity.FindFirst("WebStore")
            };

            var jwt = new JwtSecurityToken(
                issuer : _tokenOptions.Issuer,
                audience : _tokenOptions.Audience,
                claims : claims.AsEnumerable(),
                notBefore : _tokenOptions.NotBefore,
                expires : _tokenOptions.Expiration,
                signingCredentials : _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int) _tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                id = _customer.Id,
                name = _customer.Name.ToString(),
                email = _customer.Email.Address,
                username = _customer.User.Username
                }
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            var customerCommand = _repository.GetByUsername(command.Username);

            if (customerCommand == null)
                return Task.FromResult<ClaimsIdentity>(null);

            var user = new User(customerCommand.UserId, customerCommand.Username,
                customerCommand.Password, command.isRegistered);
            var name = new Name(customerCommand.FirstName, customerCommand.LastName);
            var document = new Document(customerCommand.DocumentNumber);
            var email = new Email(customerCommand.Email);
            var customer = new Customer(customerCommand.Id, name, document, email, user);

            _customer = customer;

            if (!user.Authenticate(command.Username, command.Password))
                return Task.FromResult<ClaimsIdentity>(null);

            return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(customerCommand.Username, "Token"),
                new []
                {
                    new Claim("WebStore", "User")
                }));
        }
    }
}