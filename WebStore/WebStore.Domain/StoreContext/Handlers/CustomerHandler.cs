using FluentValidator;
using WebStore.Domain.StoreContext.CustomerCommands.Inputs;
using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Domain.StoreContext.Services;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler:
        Notifiable,
        ICommandHandler<CreateCustomerCommand>,
        ICommandHandler<AddAddressCommand>
        {
            private readonly ICustomerRepository _repository;
            private readonly IEmailService _emailService;

            public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
            {
                _repository = repository;
                _emailService = emailService;
            }

            public ICommandResult Handle(CreateCustomerCommand command)
            {
                // Check if CPF already exists in the database
                if (_repository.CheckDocument(command.Document))
                    AddNotification("Document", "Este CPF já está em uso");

                // Check if E-mail already exists in the database
                if (_repository.CheckEmail(command.Email))
                    AddNotification("Email", "Este E-mail já está em uso");

                // Create VOs
                var name = new Name(command.FirstName, command.LastName);
                var document = new Document(command.Document);
                var email = new Email(command.Email);

                // Create Entities
                var customer = new Customer(name, document, email, command.Phone);

                // Check Entities and VOs
                AddNotifications(name.Notifications);
                AddNotifications(document.Notifications);
                AddNotifications(email.Notifications);
                AddNotifications(customer.Notifications);

                if (Invalid)
                    return new CommandResult(
                        false,
                        "Por favor, corrija os campos abaixo",
                        Notifications);

                // Save Client
                _repository.Save(customer);

                // Send welcome mail
                _emailService.Send(email.Address, "hello@webstore.com", "Bem vindo", "Seja bem vindo a Web Store!");

                // Return result to screen
                return new CommandResult(true, "Bem vindo a Web Store!", new
                {
                    Id = customer.Id,
                        Name = name.ToString(),
                        Email = email.Address
                });
            }

            public ICommandResult Handle(AddAddressCommand command)
            {
                throw new System.NotImplementedException();
            }
        }
}