using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
	public class Translation
	{
		[Key]
		public string Key { get; set; }

		[Key]
		public int Lcid { get; set; }

		public string Text { get; set; }
	}
}
