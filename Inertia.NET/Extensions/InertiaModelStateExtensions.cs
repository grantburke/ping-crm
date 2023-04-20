using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inertia.NET.Extensions;

public static class InertiaModelStateExtensions
{
    public static Dictionary<string, List<string>> GetInertiaErrors(this ModelStateDictionary state)
    {
        var errorList = new Dictionary<string, List<string>>();
        foreach (var key in state.Keys)
            errorList.Add($"{char.ToLower(key[0])}{key.Substring(1)}", state[key]?.Errors?.Select(s => s.ErrorMessage).ToList() ?? new List<string>());
        return errorList;
    }
}