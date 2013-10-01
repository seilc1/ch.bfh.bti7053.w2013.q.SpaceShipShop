using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaceShipShop.Models;

namespace SpaceShipShop.Areas.Admin.Controllers
{
    public class ShopItemAttributeTypeController : Controller
    {
        private DataContext db = new DataContext();

        //
        // GET: /Admin/ShopItemAttributeType/

        public ActionResult Index()
        {
            return View(db.ShopItemAttributeTypes.ToList());
        }

        //
        // GET: /Admin/ShopItemAttributeType/Details/5

        public ActionResult Details(int id = 0)
        {
            ShopItemAttributeType shopitemattributetype = db.ShopItemAttributeTypes.Find(id);
            if (shopitemattributetype == null)
            {
                return HttpNotFound();
            }
            return View(shopitemattributetype);
        }

        //
        // GET: /Admin/ShopItemAttributeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/ShopItemAttributeType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopItemAttributeType shopitemattributetype)
        {
            if (ModelState.IsValid)
            {
                db.ShopItemAttributeTypes.Add(shopitemattributetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shopitemattributetype);
        }

        //
        // GET: /Admin/ShopItemAttributeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ShopItemAttributeType shopitemattributetype = db.ShopItemAttributeTypes.Find(id);
            if (shopitemattributetype == null)
            {
                return HttpNotFound();
            }
            return View(shopitemattributetype);
        }

        //
        // POST: /Admin/ShopItemAttributeType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShopItemAttributeType shopitemattributetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shopitemattributetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shopitemattributetype);
        }

        //
        // GET: /Admin/ShopItemAttributeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ShopItemAttributeType shopitemattributetype = db.ShopItemAttributeTypes.Find(id);
            if (shopitemattributetype == null)
            {
                return HttpNotFound();
            }
            return View(shopitemattributetype);
        }

        //
        // POST: /Admin/ShopItemAttributeType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShopItemAttributeType shopitemattributetype = db.ShopItemAttributeTypes.Find(id);
            db.ShopItemAttributeTypes.Remove(shopitemattributetype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}