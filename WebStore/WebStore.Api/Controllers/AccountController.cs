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
using WebStore.Api.Security;
using WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Infra.Transactions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebStore.Api.Controllers
{
    public class AccountController : BaseController
    {
        private bool _registeredUser;
        private Customer _customer;
        private readonly ICustomerRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        public AccountController(IOptions<TokenOptions> jwtOptions, IUow uow, ICustomerRepository repository) : base(uow)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            // Converts First Letter of Token to Lowercase
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [HttpPost]
        [AllowAnonymous] // Access Permission Without Authentication
        [Route("v1/authenticate")]
        // Configure Swagger interface to send a service of an Content Type: application/x-www-form-urlencoded
        [SwaggerOperation("AuthURLEncoded")]
        public async Task<IActionResult> Post([FromForm] AuthenticateUserCommand command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            // Validate User Informations and Return User Roles.
            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            // Saved informations for Identity consults.
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

            // Json Web Token
            var jwt = new JwtSecurityToken(
                issuer : _tokenOptions.Issuer,
                audience : _tokenOptions.Audience,
                claims : claims.AsEnumerable(),
                notBefore : _tokenOptions.NotBefore,
                expires : _tokenOptions.Expiration,
                signingCredentials : _tokenOptions.SigningCredentials);

            // Encode Token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Generate Response Token
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

        // Validate Token Options
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

        // Converts a DateTime to Unix Timestamp Format
        private static long ToUnixEpochDate(DateTime date) =>
            (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        // User Informations and Roles Validate Method 
        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            var customerCommand = _repository.GetByUsername(command.Username);

            if (customerCommand == null)
                return Task.FromResult<ClaimsIdentity>(null);

            _registeredUser = true;

            var user = new User(customerCommand.UserId, customerCommand.Username,
                customerCommand.Password, _registeredUser);
            var name = new Name(customerCommand.FirstName, customerCommand.LastName);
            var document = new Document(customerCommand.Document);
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