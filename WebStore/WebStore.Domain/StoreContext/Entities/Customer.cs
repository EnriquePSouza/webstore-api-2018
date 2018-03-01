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
            BirthDate = null;
            Document = document;
            Email = email;
            User = user;

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(Document.Notifications);
        }
        public Name Name { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public User User { get; private set; }

        public void Update(Name name, DateTime birthDate)
        {
            AddNotifications(name.Notifications);

            Name = name;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}