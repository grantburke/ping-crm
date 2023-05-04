namespace Inertia.NET.Models;

public record InertiaPageObject(
    string component,
    Dictionary<string, object?> props,
    string url,
    string version);