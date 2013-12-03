using System;
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

		private const string UserAttributeCategoryCacheKey = "__UserAttributeManagerAttributeCategoryCache";

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

		public IEnumerable<UserAttributeCategory> UserAttributeCategories
		{
			get
			{
				return _cache[UserAttributeCategoryCacheKey] as IEnumerable<UserAttributeCategory> ??
						(_cache[UserAttributeCategoryCacheKey] = LoadUserAttributeCategory()) as
						IEnumerable<UserAttributeCategory>;
			}
			set { _cache[UserAttributeCategoryCacheKey] = value; }
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

		private IEnumerable<UserAttributeCategory> LoadUserAttributeCategory()
		{
			return _dbContextGetter().UserAttributeCategories.ToList();
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

		public IEnumerable<UserAttribute> GetAttributesByCategory(int categoryId)
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

		public UserAttributeCategory PutAttributeCategory(UserAttributeCategory category)
		{
			var existing = UserAttributeCategories.FirstOrDefault(c => c.Id == category.Id);
			var dbContext = _dbContextGetter();

			if (existing != null)
			{
				existing = dbContext.UserAttributeCategories.First(cat => cat.Id == existing.Id);
				existing.TextKey = category.TextKey;

				dbContext.SaveChanges();

				category = existing;
			}
			else
			{
				dbContext.UserAttributeCategories.Add(category);
				dbContext.SaveChanges();

				UserAttributeCategories = new List<UserAttributeCategory>(UserAttributeCategories) { category };
			}


			return category;
		}

		public void RemoveAttributeCategory(UserAttributeCategory category)
		{
			var existing = UserAttributeCategories.FirstOrDefault(c => c.Id == category.Id || c.TextKey.Equals(category.TextKey, StringComparison.CurrentCultureIgnoreCase));
			
			if (existing != null)
			{
				var dbContext = _dbContextGetter();
				var attrCategory = dbContext
									.UserAttributeCategories
									.FirstOrDefault(cat => cat.Id == existing.Id);

				if (attrCategory != null)
				{
					dbContext.UserAttributeCategories.Remove(attrCategory);
					dbContext.SaveChanges();

					UserAttributeCategories = UserAttributeCategories.Where(cat => cat.Id != existing.Id).ToList();
				}
			}
		}
	}
}
