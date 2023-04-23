using System.Net;
using Olax.Structs;

namespace Olax.Models;

public interface IRoute
{
    /// <summary>
    /// The route of the http url.
    /// </summary>
    string Route { get; }

    /// <summary>
    /// Operation id of the request.
    /// </summary>
    string OperationId { get; }
    
    /// <summary>
    /// Summary of the route
    /// </summary>
    string Summary { get; }
    
    /// <summary>
    /// Request type.
    /// </summary>
    HttpMethod HttpMethod { get; }
    
    /// <summary>
    /// Requested parameters from the chat-gpt user.
    /// </summary>
    List<Option> Parameters { get; }

    /// <summary>
    /// Executing the operation
    /// </summary>
    /// <param name="olaxContext">context.</param>
    /// <param name="httpListenerRequest">the listener.</param>
    /// <param name="parameters">received parameters from the gpt-user.</param>
    /// <returns>Returns the response to chat-gpt.</returns>
    Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters);

}