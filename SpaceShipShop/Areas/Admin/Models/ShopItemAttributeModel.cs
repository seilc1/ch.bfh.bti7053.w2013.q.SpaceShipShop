using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaceShipShop.Models;

namespace SpaceShipShop.Areas.Admin.Models
{
	public class ShopItemAttributeModel
	{
		public int ShopItemId { get; set; }

		public int ShopItemAttributeType { get; set; }

		public string Value { get; set; }

		public IEnumerable<ShopItemAttributeType> AvailableAttributes { get; set; }
	}
}