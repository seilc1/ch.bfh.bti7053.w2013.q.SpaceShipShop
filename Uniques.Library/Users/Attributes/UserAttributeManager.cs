﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniques.Library.Caching;
using Uniques.Library.Data;

namespace Uniques.Library.Users.Attributes
{
    /// <summary>
    /// TODO: CONCURRENCY.
    /// </summary>
    public class UserAttributeManager
    {
        private const string UserAttributeCacheKey = "__UserAttributeManagerAttributeCache";

        private readonly Func<UniquesDataContext> _dbContextGetter;

        private readonly ICacheRepository _cache;

        public UserAttributeManager(Func<UniquesDataContext> dbContextGetter, ICacheRepository cache)
        {
            _dbContextGetter = dbContextGetter;
            _cache = cache;
        } 

        public IEnumerable<UserAttribute> UserAttributes
        {
            get { return UserAttributeDictionary.Values; }
        }

        private Dictionary<int, UserAttribute> UserAttributeDictionary
        {
            get
            {
                return _cache[UserAttributeCacheKey] as Dictionary<int, UserAttribute> 
                    ?? (_cache[UserAttributeCacheKey] = LoadUserAttributeDictionary()) as Dictionary<int, UserAttribute>;
            }
            set { _cache[UserAttributeCacheKey] = value; }
        }

		private Dictionary<int, UserAttribute> LoadUserAttributeDictionary()
		{
			return new Dictionary<int, UserAttribute>(_dbContextGetter()
					.UserAttributes
					.Include("Category")
					.ToDictionary(attr => attr.Id, attr => attr));
		}

        private void SetUserAttributeToCache(UserAttribute attribute)
        {
            var dict = UserAttributeDictionary;

            if (dict.ContainsKey(attribute.Id))
            {
                dict[attribute.Id] = attribute;
            }
            else
            {
                dict.Add(attribute.Id, attribute);
            }

            UserAttributeDictionary = dict;
        }

		public IEnumerable<UserAttribute> GetAttributesByCategory(string textKey)
		{
			return UserAttributes.Where(attr => attr.Category.TextKey == textKey);
		}

		public IEnumerable<UserAttribute> GEtAttributesByCategory(int categoryId)
		{
			return UserAttributes.Where(attr => attr.CategoryId == categoryId);
		}

        public UserAttribute GetAttribute(int id)
        {
            return UserAttributes.FirstOrDefault(attr => attr.Id == id);
        }

        public UserAttribute GetAttribute(string name)
        {
            return UserAttributes.FirstOrDefault(attr => attr.TextKey.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public UserAttribute PutAttribute(UserAttribute attribute)
        {
            return UserAttributes.Any(attr => attr.Id == attribute.Id)
                       ? UpdateAttribute(attribute)
                       : AddAttribute(attribute);
        }

        private UserAttribute AddAttribute(UserAttribute attribute)
        {
            var dbContext = _dbContextGetter();
            dbContext.UserAttributes.Add(attribute);
            dbContext.SaveChanges();

            SetUserAttributeToCache(attribute);

            return attribute;
        }

        private UserAttribute UpdateAttribute(UserAttribute attribute)
        {
            var dbContext = _dbContextGetter();

            var dbAttr = dbContext.UserAttributes.Single(attr => attr.Id == attribute.Id);
            dbAttr = attribute;
            dbContext.SaveChanges();
            SetUserAttributeToCache(dbAttr);

            return dbAttr;
        }

        public void RemoveAttribute(UserAttribute attribute)
        {
            var dict = UserAttributeDictionary;
            if (dict.ContainsKey(attribute.Id))
            {
                var dbContext = _dbContextGetter();
                
                dbContext.UserAttributes.Remove(dbContext.UserAttributes.First(ua => ua.Id == attribute.Id));
                dbContext.SaveChanges();

                dict.Remove(attribute.Id);

                UserAttributeDictionary = dict;
            }
        }

		public UserAttributeCategory AddAttributeCategory(UserAttributeCategory category)
		{
			var dbContext = _dbContextGetter();
			dbContext.UserAttributeCategories.Add(category);
			dbContext.SaveChanges();

			return category;
		}

		public void RemoveAttributeCategory(UserAttributeCategory category)
		{
			var dbContext = _dbContextGetter();
			var attrCategory = dbContext
								.UserAttributeCategories
								.FirstOrDefault(cat => cat.Id == category.Id
													|| cat.TextKey.Equals(category.TextKey, StringComparison.CurrentCultureIgnoreCase));

			if (attrCategory != null)
			{
				dbContext.UserAttributeCategories.Remove(attrCategory);
				dbContext.SaveChanges();
			}
		}
    }
}
