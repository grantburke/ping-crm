namespace Inertia.NET.Models;

public record InertiaPageObject(
    string component,
    object props,
    string url,
    string version);