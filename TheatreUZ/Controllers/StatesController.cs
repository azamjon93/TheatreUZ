﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class StatesController : Controller
    {
        private TheatreUZContext db = new TheatreUZContext();

        public string AllStates()
        {
            var states = db.States.ToList();

            try
            {
                return JsonConvert.SerializeObject(states.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public ActionResult Index()
        {
            var query = new AllStatesQuery();
            var handler = StateQueryHandlerFactory.Build(query);
            
            return View(handler.Get());
        }

        public ActionResult GetState(Guid id)
        {
            var query = new OneStateQuery(id);
            var handler = StateQueryHandlerFactory.Build(query);
            return View(handler.Get());
        }

        public ActionResult AddState()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddState(State item)
        {
            var command = new StateSaveCommand(item);
            var handler = StateSaveCommandHandlerFactory.Build(command);
            var response = handler.Execute();
            
            return RedirectToAction("Index");
        }

        public ActionResult EditState(Guid id)
        {
            var query = new OneStateQuery(id);
            var handler = StateQueryHandlerFactory.Build(query);
            return View(handler.Get());
        }

        public ActionResult DeleteState(Guid id)
        {
            var query = new OneStateQuery(id);
            var handler = StateQueryHandlerFactory.Build(query);
            return View(handler.Get());
        }

        [HttpPost]
        public ActionResult DeleteStateConfirmed(Guid id)
        {
            var command = new StateDeleteCommand(id);
            var handler = StateDeleteCommandHandlerFactory.Build(command);
            var response = handler.Execute();

            return RedirectToAction("Index");
        }



        //// GET: States
        //public ActionResult Index()
        //{
        //    return View(db.States.ToList());
        //}

        //// GET: States/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    State state = db.States.Find(id);
        //    if (state == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(state);
        //}

        //// GET: States/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: States/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name,RegDate")] State state)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        state.ID = Guid.NewGuid();
        //        db.States.Add(state);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(state);
        //}

        //// GET: States/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    State state = db.States.Find(id);
        //    if (state == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(state);
        //}

        //// POST: States/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,RegDate")] State state)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(state).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(state);
        //}

        //// GET: States/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    State state = db.States.Find(id);
        //    if (state == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(state);
        //}

        //// POST: States/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    State state = db.States.Find(id);
        //    db.States.Remove(state);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
