using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myBar.Models;
using Microsoft.AspNet.Identity;

namespace myBar.Controllers
{
    public class ProductTypesController : Controller
    {
        private myBarEntities db = new myBarEntities();

        // GET: ProductTypes
        public ActionResult Index()
        {
            var productTypes = db.ProductTypes.Include(p => p.AspNetUser).Include(p => p.AspNetUser1);
            return View(productTypes.ToList());
        }

        // GET: ProductTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // GET: ProductTypes/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: ProductTypes/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductTypeID,Title,Description,Margin")] ProductType productType)
        {

            productType.CreatedBy = User.Identity.GetUserId();
            productType.CreatedDate = DateTime.Now;
            productType.ModifiedBy = User.Identity.GetUserId();
            productType.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ProductTypes.Add(productType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", productType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", productType.ModifiedBy);
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", productType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", productType.ModifiedBy);
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductTypeID,Title,Description,Margin,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", productType.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", productType.ModifiedBy);
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = db.ProductTypes.Find(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType productType = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(productType);
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
