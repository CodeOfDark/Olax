namespace Olax.Structs;

/// <summary>
/// Plugin Configuration
/// </summary>
public struct Config
{
    /// <summary>
    /// Path for the plugin logo
    /// </summary>
    public string Logo { get; init; }
    
    /// <summary>
    /// Plugin Version.
    /// </summary>
    public string Version { get; init; }
    
    /// <summary>
    /// Plugin name that is going to be used for the plugin.
    /// </summary>
    public string PluginName { get; init; }
    
    /// <summary>
    /// Model name of the plugin should be all small letters with no whitespaces.
    /// </summary>
    public string ModelName { get; init; }
    
    /// <summary>
    /// Plugin description.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Contact email.
    /// </summary>
    public string ContactEmail { get; init; }
    
    /// <summary>
    /// Legal url info.
    /// </summary>
    public string LegalUrlInfo { get; init; }
    
    /// <summary>
    /// Host for the plugin to be hosted on.
    /// </summary>
    public string Host { get; init; }
    
    /// <summary>
    /// Port for the plugin to be used on.
    /// </summary>
    public short Port { get; init; }
}