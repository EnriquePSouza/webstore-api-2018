using System;
using WebStore.Shared.Commands;

namespace WebStore.Domain.StoreContext.QueryResults
{
    public class GetProductListCommandResult : ICommandResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}