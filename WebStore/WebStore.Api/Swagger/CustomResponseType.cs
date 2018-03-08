using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebStore.Api.Swagger
{
    public class CustomResponseType : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId == "AuthURLEncoded")
            {
                operation.Consumes.Add("application/x-www-form-urlencoded");
            }
        }
    }
}