using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Infra.Transactions;

namespace WebStore.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerHandler _handler;

        public CustomerController(IUow uow, CustomerHandler handler)
            :base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [AllowAnonymous] // Access Permission Without Authentication
        [Route("v1/customers")]
        public async Task<IActionResult> Post([FromBody]RegisterCustomerCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}