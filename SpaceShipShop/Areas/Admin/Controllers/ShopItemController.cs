using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceShipShop.Areas.Admin.Models;
using SpaceShipShop.Models;

namespace SpaceShipShop.Areas.Admin.Controllers
{
    public class ShopItemController : Controller
    {
	    private DataContext db = new DataContext();

	    private readonly Lazy<IList<ShopItemAttributeType>> _allAttributes; 

	    private IEnumerable<ShopItemAttributeType> AllAttributes { get { return _allAttributes.Value; } }

		public ShopItemController()
		{
			_allAttributes = new Lazy<IList<ShopItemAttributeType>>(() => db.ShopItemAttributeTypes.ToList());
		}

		private IEnumerable<ShopItemAttributeType> AvailableAttributesFor(int shopItemId)
		{
			var firstOrDefault = db.ShopItems.FirstOrDefault(item => item.Id == shopItemId && item.Attributes.Any());
			if (firstOrDefault != null)
			{
				var definedAttributes = firstOrDefault.Attributes.Select(attr => attr.AttributeType);
				return AllAttributes.Except(definedAttributes);
			}

			return AllAttributes;
		}

	    //
        // GET: /Admin/ShopItem/

        public ActionResult Index()
        {
            return View(db.ShopItems.ToList());
        }

        //
        // GET: /Admin/ShopItem/Details/5

        public ActionResult Details(int id = 0)
        {
            ShopItem shopitem = db.ShopItems.First(s => s.Id == id);

	        shopitem.Attributes = db.ShopItemAttributes.Include(attr => attr.AttributeType).Where(i => i.Item.Id == id).ToList();

            if (shopitem == null)
            {
                return HttpNotFound();
            }
            return View(shopitem);
        }

        //
        // GET: /Admin/ShopItem/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/ShopItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopItem shopitem)
        {
            if (ModelState.IsValid)
            {
                db.ShopItems.Add(shopitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shopitem);
        }

        //
        // GET: /Admin/ShopItem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ShopItem shopitem = db.ShopItems.Find(id);
            if (shopitem == null)
            {
                return HttpNotFound();
            }
            return View(shopitem);
        }

        //
        // POST: /Admin/ShopItem/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopItem shopitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopitem);
        }

        //
        // GET: /Admin/ShopItem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ShopItem shopitem = db.ShopItems.Find(id);
            if (shopitem == null)
            {
                return HttpNotFound();
            }
            return View(shopitem);
        }

        //
        // POST: /Admin/ShopItem/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopItem shopitem = db.ShopItems.Find(id);
			if (shopitem.Attributes != null)
			{
				shopitem.Attributes.ForEach(attr => db.ShopItemAttributes.Remove(attr));
			}
			
			db.ShopItemAttributes.Where(attr => attr.Item.Id == id).ToList().ForEach(a => db.ShopItemAttributes.Remove(a));

			shopitem = db.ShopItems.Find(id);
            db.ShopItems.Remove(shopitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

		public ActionResult AddAttribute(int shopItemId)
		{
			return View(new ShopItemAttributeModel() { ShopItemId = shopItemId, AvailableAttributes = AvailableAttributesFor(shopItemId) });
		}

		[HttpPost]
		public ActionResult AddAttribute(ShopItemAttributeModel model)
		{
			var shopItem = db.ShopItems.First(item => model.ShopItemId == model.ShopItemId);

			if (shopItem != null)
			{
				if (shopItem.Attributes == null)
				{
					shopItem.Attributes = new List<ShopItemAttribute>();
				}

				var type = db.ShopItemAttributeTypes.First(attrType => attrType.Id == model.ShopItemAttributeType);

				shopItem.Attributes.Add(new ShopItemAttribute() { Value = model.Value, AttributeType = type});
				db.SaveChanges();
			}

			return RedirectToAction("Details", new {id = model.ShopItemId});
		}

		public ActionResult EditAttribute(int shopItemId, int attributeMappingId)
		{


			return View(new ShopItemAttributeModel() { ShopItemId = shopItemId, AvailableAttributes = AvailableAttributesFor(shopItemId) });
		}

		[HttpPost]
		public ActionResult EditAttribute(ShopItemAttributeModel model)
		{
			
			return RedirectToAction("Details", new { id = model.ShopItemId });
		}


    }
}