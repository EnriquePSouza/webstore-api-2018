using FluentValidator;
using WebStore.Domain.StoreContext.Commands.OrderCommands.Inputs;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Handlers
{
    public class OrderHandler : Notifiable,
        ICommandHandler<RegisterOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(RegisterOrderCommand command)
        {
            var customer = _customerRepository.GetById(command.CustomerId);

            var order = new Order(customer, command.DeliveryFee, command.Discount);

            foreach (var item in command.Items)
            {
                var product = _productRepository.GetById(item.Product);
                order.AddItem(new OrderItem(product, item.Quantity));
            }

            AddNotifications(order.Notifications);            

            if (Valid)
                _orderRepository.Save(order);
            
            return new RegisterOrderCommandResult(order.Number);
        }        
    }
}