using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Users.SimpleSearch
{
	public class SimpleSearchProvider
	{
		public List<string> SearchableFields
		{
			get { return null; }
		}

		public List<string> AllowedConditions
		{
			get { return new List<string>() { "And", "Or", "Equals", "Like", "GreaterThan", "SmallerThan" };}
		}
	}
}
