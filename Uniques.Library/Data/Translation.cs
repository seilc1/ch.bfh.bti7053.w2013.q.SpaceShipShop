using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
	public class Translation
	{
		[Column(Order = 0),Key]
		public string Key { get; set; }

		[Column(Order = 1), Key]
		public int Lcid { get; set; }

		public string Text { get; set; }
	}
}
