using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.StoreContext.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.ValueObjects;

namespace WebStore.Api.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        [Route("customers")]
        public List<Customer> Get()
        {
            var name = new Name("Enrique", "Souza");
            var document = new Document("46718115533");
            var email = new Email("enrique@gmail.com");
            var customer = new Customer(name, document, email, "5524783902349");
            var customers = new List<Customer>();
            customers.Add(customer);

            return customers;
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
        public Customer Post([FromBody] CreateCustomerCommand command)
        {
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);
            var customer = new Customer(name, document, email, command.Phone);

            return customer;
        }

        [HttpPut]
        [Route("customers/{id}")]
        public Customer Put([FromBody] CreateCustomerCommand command)
        {
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);
            var customer = new Customer(name, document, email, command.Phone);

            return customer;
        }

        [HttpDelete]
        [Route("customers/{id}")]
        public object Delete()
        {
            return new object { message = "Cliente removido com sucesso!" };
        }
    }
}