using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShipShop.Models
{
	public class ShopItem
	{
		[Key]
		public int Id { get; set; }

		[MinLength(4)]
		[MaxLength(50)]
		public string Name { get; set; }

		public string Description { get; set; }

		public double Price { get; set; }

		public List<ShopItemAttribute> Attributes { get; set; }
	}

	public class ShopItemAttributeType
	{
		[Key]
		public int Id { get; set; }

		[MinLength(4)]
		[MaxLength(50)]
		public string Name { get; set; }

		public string Description { get; set; }
	}

	public class ShopItemAttribute
	{
		[Key]
		public int Id { get; set; }

		public ShopItem Item { get; set; }

		public string Value { get; set; }

		public ShopItemAttributeType AttributeType { get; set; }
	}
}
