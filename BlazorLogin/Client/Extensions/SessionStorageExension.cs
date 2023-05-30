using System.Text.Json;
using Blazored.SessionStorage;

namespace BlazorLogin.Client.Extensions;

public static class SessionStorageExension
{
    public static async Task SaveStorage<T>(this ISessionStorageService sessionStorageService,
    string key, T item) where T : class
    {
        var itemJSON = JsonSerializer.Serialize(item);
        await sessionStorageService.SetItemAsStringAsync(key, itemJSON);
    }

    public static async Task<T?> GetStorage<T>(this ISessionStorageService sessionStorageService,
    string key) where T:class
    {
        var itemJSON = await sessionStorageService.GetItemAsStringAsync(key);

        if (itemJSON != null)
        {
            var item = JsonSerializer.Deserialize<T>(itemJSON);
            return item;
        }

        return null;
    }
}