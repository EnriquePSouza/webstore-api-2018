using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.StoreContext.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Domain.StoreContext.Queries;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Shared.Commands;

namespace WebStore.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerHandler _handler;
        public CustomerController(ICustomerRepository repository, CustomerHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("customers")]
        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return _repository.Get();
        }

        [HttpGet]
        [Route("customers/{id}")]
        public Customer GetById(Guid id)
        {
            var name = new Name("Enrique", "Souza");
            var document = new Document("46718115533");
            var email = new Email("enrique@gmail.com");
            var customer = new Customer(name, document, email, "5524783902349");

            return customer;
        }

        [HttpGet]
        [Route("customers/{id}/orders")]
        public List<Order> GetOrders(Guid id)
        {
            var name = new Name("Enrique", "Souza");
            var document = new Document("46718115533");
            var email = new Email("enrique@gmail.com");
            var customer = new Customer(name, document, email, "5524783902349");
            var order = new Order(customer);
            var mouse = new Product("Mouse Gamer", "Mouse Gamer", "mouse.jpg", 100M, 10);
            var monitor = new Product("Monitor Gamer", "Monitor Gamer", "monitor.jpg", 100M, 10);
            order.AddItem(monitor, 5);
            order.AddItem(mouse, 5);
            var orders = new List<Order>();
            orders.Add(order);

            return orders;
        }

        [HttpPost]
        [Route("customers")]
        public ICommandResult Post([FromBody] CreateCustomerCommand command)
        {
            var result = (CreateCustomerCommandResult) _handler.Handle(command);
            return result;
        }

        // todo : Update e Delete
    }
}