using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniques.Library.Data;

namespace Uniques.Library.Users.Attributes
{
    public class UserAttributeValueManager
    {
        private readonly Func<UniquesDataContext> _dbContext;

        public UserAttributeValueManager(Func<UniquesDataContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserAttributeValue> GetValues(User user)
        {
            return _dbContext()
                .UserAttributeValues
                .Include("AttributeType")
                .Where(attr => attr.User.Id == user.Id);
        }

        public UserAttributeValue SetValue(UserAttributeValue attributeValue)
        {
            var dbContext = _dbContext();

            var attribute = dbContext.UserAttributeValues
                                        .FirstOrDefault(attr => attr.User.Id == attributeValue.User.Id 
                                                    && attr.AttributeType.Id == attributeValue.AttributeType.Id);
            if (attribute != null)
            {
                attribute.Value = attributeValue.Value;
            }
            else
            {
                attribute = dbContext.UserAttributeValues.Create();

                attribute.User = dbContext.Users.Single(u => u.Id == attributeValue.User.Id);
                attribute.AttributeType = dbContext.UserAttributes.Single(attrType => attrType.Id == attributeValue.AttributeType.Id);
                attribute.Value = attributeValue.Value;

                dbContext.UserAttributeValues.Add(attribute);
            }

            dbContext.SaveChanges();

            return attribute;
        }

        public void DeleteValue(UserAttributeValue attributeValue)
        {
            var dbContext = _dbContext();
            dbContext.UserAttributeValues.Remove(dbContext.UserAttributeValues.First(uattr => uattr.Id == attributeValue.Id));
            dbContext.SaveChanges();
        }
    }
}
