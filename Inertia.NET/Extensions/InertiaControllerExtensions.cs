using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Inertia.NET.Models;

namespace Inertia.NET.Extensions;

public static class InertiaControllerExtensions
{
    public static IActionResult InertiaRender(this Controller controller, string component, object? props = null)
    {
        var request = controller.HttpContext.Request;
        var response = controller.HttpContext.Response;
        var url = $"{request.Path.Value}{request.QueryString.Value}";
        var version = Guid.NewGuid().ToString().Replace("-", string.Empty);
        var user = controller.HttpContext.User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        var propsDictionary = props?.ToDictionary() ?? new Dictionary<string, object?>();
        if (user.Any())
            propsDictionary?.TryAdd("auth", user);
        var inertiaPageObject = new InertiaPageObject(
            component,
            propsDictionary ?? new Dictionary<string, object?>(),
            url,
            version);

        request.Headers.TryGetValue("X-Requested-With", out var xRequestedWith);
        request.Headers.TryGetValue("X-Inertia", out var xInertia);
        request.Headers.TryGetValue("X-Inertia-Version", out var xInertiaVersion);
        request.Headers.TryGetValue("X-Inertia-Partial-Data", out var xInertiaPartialData);
        request.Headers.TryGetValue("X-Inertia-Partial-Component", out var xInertiaPartialComponent);

        if (xRequestedWith == "XMLHttpRequest" && xInertia == "true")
        {
            response.Headers.TryAdd("X-Inertia", xInertia);
            response.Headers.TryAdd("Vary", "Accept");
            return controller.Ok(inertiaPageObject);
        }

        // TODO: Handle partial requests

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return controller.View("_Inertia", JsonSerializer.Serialize(inertiaPageObject, jsonOptions));
    }
    
    private static Dictionary<string, object?> ToDictionary(this object obj)
    {
        var type = obj.GetType();
        var properties = type.GetProperties();
        return properties.ToDictionary(property => JsonNamingPolicy.CamelCase.ConvertName(property.Name), property => property.GetValue(obj, null));
    }
}
