﻿using System;
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

        public IEnumerable<UserAttributeValueSet> GetValues(User user)
        {
            return _dbContext()
                .UserAttributeValues
                .Include("AttributeType")
                .Where(attr => attr.User.Id == user.Id)
                .Select(attr => new UserAttributeValueSet()
                                     {
                                        AttributeTypeId = attr.AttributeType.Id,
                                        Id = attr.Id,
                                        Value = attr.Value
                                     });
        }

        public UserAttributeValueSet SetValue(int userid, UserAttributeValueSet attributeValue)
        {
            var dbContext = _dbContext();

            var attribute = dbContext.UserAttributeValues
                                        .FirstOrDefault(attr => attr.User.Id == userid
                                                    && attr.AttributeType.Id == attributeValue.AttributeTypeId);
            if (attribute != null)
            {
                attribute.Value = attributeValue.Value;
            }
            else
            {
                attribute = dbContext.UserAttributeValues.Create();

                attribute.User = dbContext.Users.Single(u => u.Id == userid);
                attribute.AttributeType = dbContext.UserAttributes.Single(attrType => attrType.Id == attributeValue.AttributeTypeId);
                attribute.Value = attributeValue.Value;

                dbContext.UserAttributeValues.Add(attribute);
            }

            dbContext.SaveChanges();

            attributeValue.Id = attribute.Id;

            return attributeValue;
        }

        public void DeleteValue(UserAttributeValueSet attributeValue)
        {
            var dbContext = _dbContext();
            dbContext.UserAttributeValues.Remove(dbContext.UserAttributeValues.First(uattr => uattr.Id == attributeValue.Id));
            dbContext.SaveChanges();
        }
    }
}
