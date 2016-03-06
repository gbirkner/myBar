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
    public class ProductsController : Controller
    {
        private myBarEntities db = new myBarEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.AspNetUser).Include(p => p.AspNetUser1).Include(p => p.ProductState).Include(p => p.ProductType).Include(p => p.Unit);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ProductStateID = new SelectList(db.ProductStates, "ProductStateID", "Title");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title");
            ViewBag.StockUnitID = new SelectList(db.Units, "UnitID", "Title");
            return View();
        }

        // POST: Products/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Title,Description,Picture,DayExpire,StockUnitID,ProductTypeID,ProductStateID,Margin,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", product.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", product.ModifiedBy);
            ViewBag.ProductStateID = new SelectList(db.ProductStates, "ProductStateID", "Title", product.ProductStateID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title", product.ProductTypeID);
            ViewBag.StockUnitID = new SelectList(db.Units, "UnitID", "Title", product.StockUnitID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", product.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", product.ModifiedBy);
            ViewBag.ProductStateID = new SelectList(db.ProductStates, "ProductStateID", "Title", product.ProductStateID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title", product.ProductTypeID);
            ViewBag.StockUnitID = new SelectList(db.Units, "UnitID", "Title", product.StockUnitID);
            return View(product);
        }

        // POST: Products/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Title,Description,Picture,DayExpire,StockUnitID,ProductTypeID,ProductStateID,Margin,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", product.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", product.ModifiedBy);
            ViewBag.ProductStateID = new SelectList(db.ProductStates, "ProductStateID", "Title", product.ProductStateID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title", product.ProductTypeID);
            ViewBag.StockUnitID = new SelectList(db.Units, "UnitID", "Title", product.StockUnitID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
