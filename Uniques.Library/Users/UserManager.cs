using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniques.Library.Authentication;
using Uniques.Library.Data;

namespace Uniques.Library.Users
{
    public class UserManager
    {
        private readonly Func<UniquesDataContext> _dbContextGetter;
        private readonly AuthenticationProvider _authProvider;

        public UserManager(Func<UniquesDataContext> dbContextGetter, AuthenticationProvider authProvider)
        {
            _dbContextGetter = dbContextGetter;
            _authProvider = authProvider;
        }

        private bool IsEmailUnique(string email)
        {
            return !_dbContextGetter().Users.All(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }

        private bool IsLoginnameUnique(string loginname)
        {
            return !_dbContextGetter().UserLogins.All(u => u.Loginname.Equals(loginname, StringComparison.CurrentCultureIgnoreCase));
        }

        public User Add(User user)
        {
            if (!IsEmailUnique(user.Email))
            {
                throw new ArgumentException("E-Mail not unique.");
            }
            if (!IsLoginnameUnique(user.Loginname))
            {
                throw new ArgumentException("Loginname not unique.");
            }

            string salt;

            user.Password = _authProvider.EncryptPassword(user.Password, out salt);
            user.PasswordSalt = salt;
            user.LastAction = DateTime.Now;

            var dbContext = _dbContextGetter();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;
        }

        public User Get(int id)
        {
            return _dbContextGetter().Users.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string displayname)
        {
            return _dbContextGetter().Users.FirstOrDefault(u => u.Displayname == displayname);
        }
    }
}
