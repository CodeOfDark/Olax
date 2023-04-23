using System.Net;
using System.Text;
using Olax;
using Olax.Models;
using Olax.Structs;

namespace Todo;

public class GetTodosRoute : IRoute
{
    public string Route => "get_todo";
    public string OperationId => "get_todo";
    public string Summary => "get a list of todo";
    public HttpMethod HttpMethod => HttpMethod.Get;
    public List<Option> Parameters => new ();
    
    public Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters)
    {
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < Global.Todos.Count; i++)
        {
            stringBuilder.AppendLine($"Index: {i}");
            stringBuilder.AppendLine($"Description: {Global.Todos[i].Description}");
        }

        return new Response
        {
            Content = stringBuilder.ToString(),
            HttpStatusCode = HttpStatusCode.OK
        };
    }
}