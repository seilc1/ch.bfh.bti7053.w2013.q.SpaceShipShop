using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Uniques.Library.Data
{
	public class UserAttributeCategory
	{
		[Key]
		public int Id { get; set; }

		public string TextKey { get; set; }
	}
}
