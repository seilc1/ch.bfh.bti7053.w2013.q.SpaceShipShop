using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniques.Library.Data;

namespace Uniques.Library.Users.SimpleSearch
{
	public class SimpleSearchProvider
	{
	    private readonly Func<UniquesDataContext> _dbContextGetter;
	    private readonly UserManager _userManager;

		public List<string> SearchableFields
		{
			get { return null; }
		}

		public List<string> AllowedConditions
		{
			get { return new List<string>() { "And", "Or", "Equals", "Like", "GreaterThan", "SmallerThan" };}
		}

        public SimpleSearchProvider(Func<UniquesDataContext> dbContextGetter, UserManager userManager)
        {
            _userManager = userManager;
            _dbContextGetter = dbContextGetter;
        }

        public IEnumerable<MinimalUser> Search(string searchTerm)
        {
            string cleanedSearchTerm = StripWhereClausal(searchTerm);

            return _dbContextGetter().UserAttributeValues
                .Where(attr => attr.AttributeType.Searchable && attr.Value.Contains(cleanedSearchTerm))
                .Select(attr => attr.User)
                .Distinct()
                .Select(_userManager.Reduce);
        }

        private string StripWhereClausal(string searchTerm)
        {
            return searchTerm.Substring(6, searchTerm.Length - 7);
        }
	}
}
