using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreUZ;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class SpectaclesController : Controller
    {
        private TheatreUZContext db = new TheatreUZContext();

        // GET: Spectacles
        public ActionResult Index()
        {
            var spectacles = db.Spectacles.Include(s => s.Genre).Include(s => s.State);
            return View(spectacles.ToList());
        }

        // GET: Spectacles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spectacle spectacle = db.Spectacles.Find(id);
            if (spectacle == null)
            {
                return HttpNotFound();
            }
            return View(spectacle);
        }

        // GET: Spectacles/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name");
            ViewBag.StateID = new SelectList(db.States, "ID", "Name");
            return View();
        }

        // POST: Spectacles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GenreID,StateID,Name,Cost,PlayDate,RegDate")] Spectacle spectacle)
        {
            if (ModelState.IsValid)
            {
                spectacle.ID = Guid.NewGuid();
                db.Spectacles.Add(spectacle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", spectacle.GenreID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", spectacle.StateID);
            return View(spectacle);
        }

        // GET: Spectacles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spectacle spectacle = db.Spectacles.Find(id);
            if (spectacle == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", spectacle.GenreID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", spectacle.StateID);
            return View(spectacle);
        }

        // POST: Spectacles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GenreID,StateID,Name,Cost,PlayDate,RegDate")] Spectacle spectacle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spectacle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", spectacle.GenreID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", spectacle.StateID);
            return View(spectacle);
        }

        // GET: Spectacles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spectacle spectacle = db.Spectacles.Find(id);
            if (spectacle == null)
            {
                return HttpNotFound();
            }
            return View(spectacle);
        }

        // POST: Spectacles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Spectacle spectacle = db.Spectacles.Find(id);
            db.Spectacles.Remove(spectacle);
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
