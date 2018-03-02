using System;
using FluentValidator;
using WebStore.Domain.StoreContext.ValueObjects;

namespace WebStore.Domain.StoreContext.Entities
{
    public class Customer : Notifiable
    {
        public Customer(Nullable<Guid> id, Name name, Document document, Email email, User user)
        {
            Id = id == null ? Guid.NewGuid() : id;
            Name = name;
            Document = document;
            Email = email;
            User = user;

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(Document.Notifications);
        }
        public Nullable<Guid> Id { get; private set; }
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public User User { get; private set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}