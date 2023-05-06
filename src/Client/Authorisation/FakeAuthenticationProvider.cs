

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Authorisation
{
     public class FakeAuthenticationProvider : AuthenticationStateProvider
    {

        public ClaimsPrincipal Current { get; private set; } = Customer;
        public static ClaimsPrincipal Anonymous => new(new ClaimsIdentity(new[]
     {
            new Claim(ClaimTypes.Name, "Anonymous"),
    }));

        public static ClaimsPrincipal Customer =>
             new(
                 new ClaimsIdentity(new[]
                                            {
                                              new Claim(ClaimTypes.NameIdentifier, "ID1"),
                                              new Claim(ClaimTypes.Name, "Lisa"),
                                              new Claim(ClaimTypes.Email, "fake-customer@gmail.com"),
                                              new Claim(ClaimTypes.Role, "Customer"),
                                            }
                                     , "Fake Authentication"
                                    )
                 );
        public static ClaimsPrincipal Customer2 =>
             new(
                 new ClaimsIdentity(new[]
                                            {
                                              new Claim(ClaimTypes.NameIdentifier, "ID2"),
                                              new Claim(ClaimTypes.Name, "Jake"),
                                              new Claim(ClaimTypes.Email, "fake-customer@gmail.com"),
                                              new Claim(ClaimTypes.Role, "Customer"),
                                            }
                                     , "Fake Authentication"
                                    )
                 );
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(Current));
        }

        public void ChangeAuthenticationState(ClaimsPrincipal claimsPrincipal)
        {
            Current = claimsPrincipal;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
