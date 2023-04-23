# Olax
**Olax is an open-source library that enables users to build their GPT plugins using CSharp with minimal effort.**


## Why Olax?

---
Olax helps you build and host a Chat-GPT plugin with minimal effort, and I assume this is the first library that does so.

## Usage

---
* Create a C# project.
* Setup a config
```csharp
 var config = new Config()
{
    Logo = @"plugin logo",
    Version = "plugin version",
    PluginName = "plugin name",
    ModelName = "plugin model name",
    Description = "plugin description",
    Host = "host",
    Port = port,
    ContactEmail = "your contact email",
    LegalUrlInfo = "your website info"
};
```

* Create an instance of OlaxContext
```csharp
var olax = new OlaxContext(config, false);
```

* Add your routes
```csharp
olax.Routes.Add(...);
```

* Host your plugin
```csharp
olax.Start();
```