using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
    public class UniquesDataContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CompleteUser> CompleteUsers { get; set; }

        public DbSet<UserAttribute> UserAttributes { get; set; }

        public DbSet<UserAttributeValue> UserAttributeValues { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}
