using System;
using System.ComponentModel.DataAnnotations;

namespace Uniques.Library.Data
{
	public class Image
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public Guid FileId { get; set; }

		[MaxLength(255)]
		public string Description { get; set; }

		public User Owner { get; set; }

		public bool IsPortrait { get; set; }
	}
}
