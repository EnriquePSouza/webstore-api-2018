using System;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.QueryResults
{
    public class RegisterCustomerCommandResult : ICommandResult
    {
        public RegisterCustomerCommandResult()
        {
            
        }
        public RegisterCustomerCommandResult(Guid? id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}