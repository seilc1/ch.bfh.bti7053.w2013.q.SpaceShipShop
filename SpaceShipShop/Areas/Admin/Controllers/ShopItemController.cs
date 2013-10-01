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
    public class ShopItemController : Controller
    {
        private DataContext db = new DataContext();

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
            ShopItem shopitem = db.ShopItems.Find(id);
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
            db.ShopItems.Remove(shopitem);
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