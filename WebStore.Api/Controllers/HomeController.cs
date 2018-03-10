using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Api.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public object Get()
        {
            return new { version = "Version 1" };
        }

    }
}