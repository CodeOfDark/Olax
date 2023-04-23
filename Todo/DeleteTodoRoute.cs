using System.Net;
using Olax;
using Olax.Enums;
using Olax.Models;
using Olax.Structs;

namespace Todo;

public class DeleteTodoRoute : IRoute
{
    public string Route => "remove_todo";
    public string OperationId => "remove_todo";
    public string Summary => "remove a todo item from the list";
    public HttpMethod HttpMethod => HttpMethod.Delete;
    public List<Option> Parameters => new()
    {
        new Option()
        {
            Name = "index",
            Type = Types.Integer
        }
    };
    
    public Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters)
    {
        if (parameters.Count != 1)
        {
            return new Response
            {
                Content = "Index not found in the list",
                HttpStatusCode = HttpStatusCode.OK
            };
        }
        
        Global.Todos.RemoveAt((int)parameters[0].Value);
        return new Response
        {
            Content = "Your todo item has been deleted from your list.",
            HttpStatusCode = HttpStatusCode.OK
        };
    }
}