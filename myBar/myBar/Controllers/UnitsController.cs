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
    public class UnitsController : Controller
    {
        private myBarEntities db = new myBarEntities();

        // GET: Units
        public ActionResult Index()
        {
            var units = db.Units.Include(u => u.AspNetUser).Include(u => u.AspNetUser1);
            return View(units.ToList());
        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }


        [Authorize (Roles = "Administrator, Backoffice")]
        // GET: Units/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Units/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize( Roles = "Administrator, Backoffice" )]
        public ActionResult Create([Bind(Include = "UnitID,Title,Description,BaseFactor")] Unit unit)
        {
            unit.CreatedBy = User.Identity.GetUserId();
            unit.CreatedDate = DateTime.Now;
            unit.ModifiedBy = User.Identity.GetUserId();
            unit.ModifiedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Units.Add(unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", unit.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", unit.ModifiedBy);
            return View(unit);
        }

        // GET: Units/Edit/5
        [Authorize( Roles = "Administrator, Backoffice" )]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", unit.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", unit.ModifiedBy);
            return View(unit);
        }

        // POST: Units/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize( Roles = "Administrator, Backoffice" )]
        public ActionResult Edit([Bind(Include = "UnitID,Title,Description,BaseFactor")] Unit unit)
        {
            unit.ModifiedBy = User.Identity.GetUserId();
            unit.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", unit.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", unit.ModifiedBy);
            return View(unit);
        }

        // GET: Units/Delete/5
        [Authorize( Roles = "Administrator, Backoffice" )]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize( Roles = "Administrator, Backoffice" )]
        public ActionResult DeleteConfirmed(int id)
        {
            Unit unit = db.Units.Find(id);
            db.Units.Remove(unit);
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
