using WebStore.Domain.StoreContext.Entities;
using WebStore.Domain.StoreContext.Services;

namespace WebStore.Tests.Mocks
{
    public class MockEmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            
        }
    }
}