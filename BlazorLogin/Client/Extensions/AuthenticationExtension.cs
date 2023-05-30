using System.Security.Claims;
using Blazored.SessionStorage;
using BlazorLogin.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorLogin.Client.Extensions;

public class AuthenticationExtension : AuthenticationStateProvider
{
    private readonly ISessionStorageService _sessionStorage;
    private ClaimsPrincipal _withoutInformation = new ClaimsPrincipal(new ClaimsIdentity());

    public AuthenticationExtension(ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public async Task UpdateAuthenticationState(SessionDTO? userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>{
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role,userSession.Role)
            }, "JwtAuth"));

            await _sessionStorage.SaveStorage("UserSession", userSession);
        }
        else
        {
            claimsPrincipal = _withoutInformation;
            await _sessionStorage.RemoveItemAsync("UserSession");
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userSession = await _sessionStorage.GetStorage<SessionDTO>("UserSession");

        if (userSession is null)
        {
            return await Task.FromResult(new AuthenticationState(_withoutInformation));
        }

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>{
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role,userSession.Role)
            }, "JwtAuth"));

        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }
}