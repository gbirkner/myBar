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

    [Authorize]
    public class OrderStatesController : Controller
    {
        private myBarEntities db = new myBarEntities();

        // GET: OrderStates
        public ActionResult Index()
        {
            var orderStates = db.OrderStates.Include(o => o.AspNetUser).Include(o => o.AspNetUser1);
            return View(orderStates.ToList());
        }

        // GET: OrderStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderState orderState = db.OrderStates.Find(id);
            if (orderState == null)
            {
                return HttpNotFound();
            }
            return View(orderState);
        }

        // GET: OrderStates/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: OrderStates/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderStateID,Title,Description")] OrderState orderState)
        {
            orderState.CreatedBy = User.Identity.GetUserId();
            orderState.CreatedDate = DateTime.Now;
            orderState.ModifiedBy = User.Identity.GetUserId();
            orderState.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.OrderStates.Add(orderState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", orderState.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", orderState.ModifiedBy);
            return View(orderState);
        }

        // GET: OrderStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderState orderState = db.OrderStates.Find(id);
            if (orderState == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", orderState.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", orderState.ModifiedBy);
            return View(orderState);
        }

        // POST: OrderStates/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderStateID,Title,Description")] OrderState orderState)
        {
            orderState.ModifiedBy = User.Identity.GetUserId();
            orderState.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(orderState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", orderState.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", orderState.ModifiedBy);
            return View(orderState);
        }

        // GET: OrderStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderState orderState = db.OrderStates.Find(id);
            if (orderState == null)
            {
                return HttpNotFound();
            }
            return View(orderState);
        }

        // POST: OrderStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderState orderState = db.OrderStates.Find(id);
            db.OrderStates.Remove(orderState);
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
