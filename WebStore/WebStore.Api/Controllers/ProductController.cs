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
        // Select in database a list of Products
        public IActionResult Get()
        {
            return Ok(_repository.Get());
        }
    }
}