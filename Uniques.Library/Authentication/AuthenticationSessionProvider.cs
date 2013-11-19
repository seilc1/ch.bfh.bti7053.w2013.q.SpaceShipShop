using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniques.Library.Caching;
using Uniques.Library.Data;

namespace Uniques.Library.Authentication
{
    public class AuthenticationSessionProvider
    {
        private const string CacheKey = "__User";

        private readonly ICacheRepository _cacheRepository;
        private readonly AuthenticationProvider _authProvider;
        private readonly Func<UniquesDataContext> _dbContextGetter;

        public bool Authenticated
        {
            get { return _cacheRepository[CacheKey] != null; }
        }

        public User CurrentUser
        {
            get { return _cacheRepository[CacheKey] as User; }
        }

        public AuthenticationSessionProvider(Func<UniquesDataContext> dbContextGetter, ICacheRepository cacheRepository, AuthenticationProvider authProvider)
        {
            _cacheRepository = cacheRepository;
            _authProvider = authProvider;
            _dbContextGetter = dbContextGetter;
        }

        public bool Authenticate(LoginModelWithEmail loginmodel)
        {
            return Authenticate(_dbContextGetter().Users.FirstOrDefault(u => u.Email == loginmodel.Email), loginmodel.Password);
        }

        public bool Authenticate(LoginModelWithUserName loginmodel)
        {
            return Authenticate(_dbContextGetter().Users.FirstOrDefault(u => u.Loginname == loginmodel.Loginname), loginmodel.Password);
        }

        private bool Authenticate(User login, string password)
        {
            if (_authProvider.Validate(password, login.Password, login.PasswordSalt))
            {
                _cacheRepository[CacheKey] = login;
                return true;
            }

            return false;
        }

        public void Destroy()
        {
            _cacheRepository.Delete(CacheKey);
        }
    }
}
