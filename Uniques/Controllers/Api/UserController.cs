using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Uniques.Library.Data;

namespace Uniques.Controllers.Api
{
    public class UserController : ApiController
    {
        private UniquesDataContext _dbContext;

        private UniquesDataContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = new UniquesDataContext());
            }
        }

        public List<User> Get()
        {
            return DbContext.Users.ToList();
        }

        public User Get(int id)
        {
            return DbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string displayname)
        {
            return DbContext.Users.FirstOrDefault(u => u.Displayname == displayname);
        }

        public User Put([FromBody]User user)
        {
            user.LastAction = DateTime.Now;
            user.PasswordSalt = "Salz";
            
            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            return user;
        }
    }
}
