using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
	public class UserAttributeCategory
	{
		public int Id { get; set; }

		public string TextKey { get; set; }

		public virtual List<UserAttribute> Attributes { get; set; }
	}
}
