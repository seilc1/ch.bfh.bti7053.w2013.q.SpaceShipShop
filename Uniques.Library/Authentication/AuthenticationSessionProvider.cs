using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Uniques.Library.Caching;
using Uniques.Library.Data;

namespace Uniques.Library.Authentication
{
    public class AuthenticationSessionProvider
    {
        private const string CacheKey = "__User";

        private readonly AuthenticationProvider _authProvider;
        private readonly Func<UniquesDataContext> _dbContextGetter;

        private readonly Regex _emailRegex = new Regex(Common.Constants.EmailRegExp);

        public bool Authenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }

        public AuthenticationSessionProvider(Func<UniquesDataContext> dbContextGetter, AuthenticationProvider authProvider)
        {
            _authProvider = authProvider;
            _dbContextGetter = dbContextGetter;
        }

        public bool Authenticate(LoginModel loginmodel)
        {
            User user;

            if (_emailRegex.IsMatch(loginmodel.Id))
            {
                user = _dbContextGetter().Users
                    .FirstOrDefault(u => u.Email.Equals(loginmodel.Id, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                user = _dbContextGetter().Users
                    .FirstOrDefault(u => u.Loginname.Equals(loginmodel.Id, StringComparison.InvariantCultureIgnoreCase));
            }

            return user != null && Authenticate(user, loginmodel.Password);
        }

        private bool Authenticate(User login, string password)
        {
            if (_authProvider.Validate(password, login.Password, login.PasswordSalt))
            {
                FormsAuthentication.SetAuthCookie(login.Loginname, false);
                AuthenticateRequest();

                return true;
            }

            return false;
        }

        public void Destroy()
        {
            FormsAuthentication.SignOut();
        }

        public void AuthenticateRequest()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                GenericPrincipal prin = new GenericPrincipal(new GenericIdentity(ticket.Name), null);
                HttpContext.Current.User = prin;
            }
        }
    }
}
