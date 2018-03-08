using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace WebStore.Api.Security
{
    // The Token is an requested key to allow access to the solucion
    public class TokenOptions
    {
        // Requester
        public string Issuer { get; set; }

        // Keyword
        public string Subject { get; set; }

        // Receiver
        public string Audience { get; set; }

        // Limit Date for establishing a Expiry Date
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;

        // Request Date
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        // Expiry Date
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromDays(2);

        // Set The Expiry Date
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        
        // Token Functions
        public Func<Task<string>> JtiGenerator =>
          () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }

    }
}