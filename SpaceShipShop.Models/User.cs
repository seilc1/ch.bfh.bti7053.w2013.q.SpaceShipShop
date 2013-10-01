using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipShop.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[MinLength(6)]
		[MaxLength(20)]
		public string LoginName { get; set; }

		[MinLength(6)]
		[MaxLength(20)]
		public string DisplayName { get; set; }

		[MinLength(6)]
		[MaxLength(32)]
		public string Password { get; set; }

		[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$")]
		public string Email { get; set; }

		public UserStatus Status { get; set; }
	}

	public enum UserStatus
	{
		Inactive = 0,
		Active = 1,
		Poor = 5
	}
}
