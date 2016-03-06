using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myBar.Models;

namespace myBar.Controllers
{
    public class ProductServingsController : Controller
    {
        private myBarEntities db = new myBarEntities();

        // GET: ProductServings
        public ActionResult Index()
        {
            var productServings = db.ProductServings.Include(p => p.AspNetUser).Include(p => p.AspNetUser1).Include(p => p.Product).Include(p => p.Unit);
            return View(productServings.ToList());
        }

        // GET: ProductServings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductServing productServing = db.ProductServings.Find(id);
            if (productServing == null)
            {
                return HttpNotFound();
            }
            return View(productServing);
        }

        // GET: ProductServings/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title");
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Title");
            return View();
        }

        // POST: ProductServings/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,UnitID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProductServing productServing)
        {
            if (ModelState.IsValid)
            {
                db.ProductServings.Add(productServing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", productServing.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", productServing.ModifiedBy);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", productServing.ProductID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Title", productServing.UnitID);
            return View(productServing);
        }

        // GET: ProductServings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductServing productServing = db.ProductServings.Find(id);
            if (productServing == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", productServing.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", productServing.ModifiedBy);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", productServing.ProductID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Title", productServing.UnitID);
            return View(productServing);
        }

        // POST: ProductServings/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,UnitID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProductServing productServing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productServing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", productServing.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", productServing.ModifiedBy);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", productServing.ProductID);
            ViewBag.UnitID = new SelectList(db.Units, "UnitID", "Title", productServing.UnitID);
            return View(productServing);
        }

        // GET: ProductServings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductServing productServing = db.ProductServings.Find(id);
            if (productServing == null)
            {
                return HttpNotFound();
            }
            return View(productServing);
        }

        // POST: ProductServings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductServing productServing = db.ProductServings.Find(id);
            db.ProductServings.Remove(productServing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
