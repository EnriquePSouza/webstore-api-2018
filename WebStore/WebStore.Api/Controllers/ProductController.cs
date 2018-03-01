using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.StoreContext.Repositories;

namespace WebStore.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/products")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_repository.Get());
        }
    }
}