using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SpaceShipShop.Models;

namespace SpaceShipShop
{
	public class DataContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<ShopItem> ShopItems { get; set; }

		public DbSet<ShopItemAttributeType> ShopItemAttributeTypes { get; set; }
	}
}