using FluentValidator;
using WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Handlers
{
    public class OrderHandler : Notifiable,
        ICommandHandler<RegisterOrderCommand>
        {
            private bool _registeredUser;
            private readonly ICustomerRepository _customerRepository;
            private readonly IProductRepository _productRepository;
            private readonly IOrderRepository _orderRepository;

            public OrderHandler(ICustomerRepository customerRepository,
                IProductRepository productRepository, IOrderRepository orderRepository)
            {
                _customerRepository = customerRepository;
                _productRepository = productRepository;
                _orderRepository = orderRepository;
            }

            public ICommandResult Handle(RegisterOrderCommand command)
            {
                var customerCommand = _customerRepository.GetById(command.CustomerId);

                _registeredUser = true;

                var name = new Name(customerCommand.FirstName, customerCommand.LastName);
                var document = new Document(customerCommand.Document);
                var email = new Email(customerCommand.Email);
                var user = new User(customerCommand.UserId, customerCommand.Username, customerCommand.Password, _registeredUser);
                var customer = new Customer(customerCommand.Id, name, document, email, user);

                var order = new Order(command.Id, customer, command.DeliveryFee, command.Discount);

                foreach (var item in command.Items)
                {
                    var productCommand = _productRepository.GetById(item.ProductId);
                    var product = new Product(productCommand.Id, productCommand.Title,
                        productCommand.Image, productCommand.Price, productCommand.QuantityOnHand);
                    order.AddItem(new OrderItem(item.Id, order, product, item.Quantity));
                }

                AddNotifications(order.Notifications);

                if (Valid)
                    _orderRepository.Save(order);

                return new RegisterOrderCommandResult(order.Number);
            }
        }
}