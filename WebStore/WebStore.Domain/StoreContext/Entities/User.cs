using System;
using System.Text;
using FluentValidator;
using FluentValidator.Validation;

namespace WebStore.Domain.StoreContext.Entities
{
    public class User : Notifiable
    {
        public User(Nullable<Guid> id, string username, string password, bool isRegistered)
        {
            Id = id == null ? Guid.NewGuid() : id;
            Username = username;
            Password = isRegistered ? password : EncryptPassword(password);
            Active = true;
        }

        public Nullable<Guid> Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }

        public bool Authenticate(string username, string password)
        {
            if (Username == username && Password == EncryptPassword(password))
                return true;

            AddNotification("User", "Usuário ou senha inválidos");
            return false;
        }

        public void ValidadePassword(string password, string confirmPassword)
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .AreEquals(EncryptPassword(confirmPassword), EncryptPassword(password), "Password", "As senhas não coincidem")
            );
        }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        private string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            var password = (pass += "|2d331cca-f6c0-40c0-bb43-6e32989c2881");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}