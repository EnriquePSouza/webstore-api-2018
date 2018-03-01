using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidator;
using WebStore.Infra.Transactions;

namespace WebStore.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUow _uow;

        public HomeController(IUow uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("")]
        public object Get()
        {
            return new { version = "Version 0.0.1" };
        }

        public new IActionResult Response(object result, IEnumerable<Notification> notifications)
        {
            if (!notifications.Any())
            {
                try
                {
                    _uow.Commit();
                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }
                catch
                {
                    return BadRequest(new
                    {
                        success = false,
                        errors = new[] { "Ocorreu uma falha interna no servidor." }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = notifications
                });
            }
        }
    }
}
