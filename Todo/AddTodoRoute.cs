using System.Net;
using Olax;
using Olax.Enums;
using Olax.Models;
using Olax.Structs;

namespace Todo;

public class AddTodoRoute : IRoute
{
    public string Route => "add_todo";
    public string OperationId => "add_todo";
    public string Summary => "add a todo item in the list";
    public HttpMethod HttpMethod => HttpMethod.Delete;
    public List<Option> Parameters => new()
    {
        new Option()
        {
            Name = "description",
            Type = Types.String
        }
    };
    
    public Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters)
    {
        if (parameters.Count != 1)
        {
            return new Response
            {
                Content = "Description not found in the list",
                HttpStatusCode = HttpStatusCode.OK
            };
        }
        
        Global.Todos.Add(new Todo
        {
            Description = (string)parameters[0].Value
        });
        
        return new Response
        {
            Content = "Your todo item has been added to your list.",
            HttpStatusCode = HttpStatusCode.OK
        };
    }
}