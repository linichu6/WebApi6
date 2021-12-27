using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WebApi6.Repository;

namespace WebApi6.Handlers
{
    public class UserAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IUserRepository _repo;
        public UserAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            IUserRepository patienRepository,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _repo = patienRepository;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization Header not found");

            var auth =  AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            var bs = Convert.FromBase64String(auth.Parameter);

            string[] credential = Encoding.UTF8.GetString(bs).Split(":");
            if(credential.Length != 2)
                return AuthenticateResult.Fail("Authorization Header not correct");

            string user = credential[0];
            string password = credential[1];

            var u = _repo.GetUser(user);
            if(u == null)
                return AuthenticateResult.Fail("User not found");

            if (u.Password == password)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, u.Username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("TODO");
        }
    }
}
