using FluentValidator;
using WebStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.QueryResults;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable,
        ICommandHandler<RegisterCustomerCommand>
        {
            private readonly ICustomerRepository _customerRepository;

            public CustomerHandler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public ICommandResult Handle(RegisterCustomerCommand command)
            {
                if (_customerRepository.DocumentExists(command.Document))
                {
                    AddNotification("Document", "Este CPF já está em uso!");
                    return null;
                }

                var name = new Name(command.FirstName, command.LastName);
                var document = new Document(command.Document);
                var email = new Email(command.Email);
                var user = new User(command.UserId, command.Username, command.Password, command.isRegistered);
                var customer = new Customer(command.Id, name, document, email, user);

                user.ValidadePassword(command.Password, command.ConfirmPassword);

                AddNotifications(name.Notifications);
                AddNotifications(document.Notifications);
                AddNotifications(email.Notifications);
                AddNotifications(user.Notifications);
                AddNotifications(customer.Notifications);

                if (!Valid)
                    return null;

                _customerRepository.Save(customer);

                return new RegisterCustomerCommandResult(customer.Id, customer.Name.ToString());
            }
        }
}