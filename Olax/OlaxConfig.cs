namespace Olax;

/// <summary>
/// Plugin Configuration
/// </summary>
public struct OlaxConfig
{
    /// <summary>
    /// Plugin Version.
    /// </summary>
    public string Version;
    
    /// <summary>
    /// Plugin name that is going to be used for the plugin.
    /// </summary>
    public string PluginName;
    
    /// <summary>
    /// Model name of the plugin should be all small letters with no whitespaces.
    /// </summary>
    public string ModelName; 
    
    /// <summary>
    /// Plugin description.
    /// </summary>
    public string Description;

    /// <summary>
    /// Contact email.
    /// </summary>
    public string ContactEmail;
    
    /// <summary>
    /// Legal url info.
    /// </summary>
    public string LegalUrlInfo;
    
    /// <summary>
    /// Host for the plugin to be hosted on.
    /// </summary>
    public string Host;
    
    /// <summary>
    /// Port for the plugin to be used on.
    /// </summary>
    public short Port;
}