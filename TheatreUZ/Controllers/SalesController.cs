using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class SalesController : Controller
    {
        private TheatreUZContext db = new TheatreUZContext();

        public string AllSales()
        {
            var sales = db.Sales.ToList();

            try
            {
                return JsonConvert.SerializeObject(sales.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }); ;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Spectacle).Include(s => s.State).Include(s => s.User);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.SpectacleID = new SelectList(db.Spectacles, "ID", "Name");
            ViewBag.StateID = new SelectList(db.States, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,SpectacleID,StateID,Amount,RegDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                sale.ID = Guid.NewGuid();
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpectacleID = new SelectList(db.Spectacles, "ID", "Name", sale.SpectacleID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", sale.StateID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", sale.UserID);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpectacleID = new SelectList(db.Spectacles, "ID", "Name", sale.SpectacleID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", sale.StateID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", sale.UserID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,SpectacleID,StateID,Amount,RegDate")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpectacleID = new SelectList(db.Spectacles, "ID", "Name", sale.SpectacleID);
            ViewBag.StateID = new SelectList(db.States, "ID", "Name", sale.StateID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", sale.UserID);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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
