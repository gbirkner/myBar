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
    public class PlacesController : Controller {
        private myBarEntities db = new myBarEntities();

        // GET: Places
        public ActionResult Index() {
            var places = db.Places.Include(p => p.AspNetUser).Include(p => p.AspNetUser1);
            return View( places.ToList() );
        }

        // GET: Places/Details/5
        public ActionResult Details( int? id ) {
            if (id == null) {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Place place = db.Places.Find(id);
            if (place == null) {
                return HttpNotFound();
            }
            return View( place );
        }

        // GET: Places/Create
        public ActionResult Create() {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Places/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceID,Title,Description,Capacity,smoking")] Place place)
        {
            place.CreatedBy = User.Identity.GetUserId();
            place.CreatedDate = DateTime.Now;
            place.ModifiedBy = User.Identity.GetUserId();
            place.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", place.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", place.ModifiedBy);
            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", place.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", place.ModifiedBy);
            return View(place);
        }

        // POST: Places/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceID,Title,Description,Capacity,smoking,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Place place)
        {
            place.ModifiedBy = User.Identity.GetUserId();
            place.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", place.CreatedBy);
            ViewBag.ModifiedBy = new SelectList(db.AspNetUsers, "Id", "Email", place.ModifiedBy);
            return View(place);
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
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
