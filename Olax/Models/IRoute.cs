using Olax.Enums;

namespace Olax.Models;

public interface IRoute
{
    /// <summary>
    /// Path which means the route of the http url.
    /// </summary>
    string Path { get; set; }
    
    /// <summary>
    /// Requested parameters from the chat-gpt user.
    /// </summary>
    Dictionary<string, Types> Parameters { get; set; }
    
    /// <summary>
    /// Requested body from the chat-gpt user.
    /// </summary>
    Dictionary<string, Types> Body { get; set; }

    void Execute(Olax olex, Dictionary<string, Types> parameters, Dictionary<string, Types> body);
}