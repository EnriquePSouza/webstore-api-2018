using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Infra.Transactions;

namespace WebStore.Api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderHandler _handler;
        public OrderController(IUow uow, OrderHandler handler) : base(uow)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/orders")]
        public async Task<IActionResult> Post([FromBody] RegisterOrderCommand command)
        {
            var result = _handler.Handle(command);
            return await Response(result, _handler.Notifications);
        }
    }
}