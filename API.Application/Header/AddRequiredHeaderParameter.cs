using System.Web.Http.Description;
using Swashbuckle.Swagger;
using System.Collections.Generic;

namespace application.Header
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        //public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        //{
        //    if (operation.parameters == null)
        //        operation.parameters = new List<Swashbuckle.Swagger.Parameter>();


        //        operation.parameters.Add(new Parameter
        //        {
        //            name = "RefreshToken",
        //            @in = "header",
        //            type = "string",
        //            description = "Refresh Token to stay connected",
        //            required = true
        //        });


        //}
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            throw new System.NotImplementedException();
        }
    }
}
