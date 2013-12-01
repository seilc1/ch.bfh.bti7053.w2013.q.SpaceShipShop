using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
	public class UserAttribute
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(30)]
		public string TextKey { get; set; }

		[MaxLength(30)]
		public string DefaultText { get; set; }

		[MaxLength(255)]
		public string DefaultDescription { get; set; }

		[DefaultValue(false)]
		public bool Searchable { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public UserAttributeCategory Category { get; set; }
	}
}
