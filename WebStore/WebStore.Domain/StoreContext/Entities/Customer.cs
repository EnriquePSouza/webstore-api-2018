using System;
using WebStore.Domain.StoreContext.ValueObjects;
using WebStore.Shared.Entities;

namespace WebStore.Domain.StoreContext.Entities
{
    public class Customer : Entity
    {
        protected Customer() { }

        public Customer(Name name, Document document, Email email, User user)
        {
            Name = name;
            Document = document;
            Email = email;
            User = user;

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(Document.Notifications);
        }
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