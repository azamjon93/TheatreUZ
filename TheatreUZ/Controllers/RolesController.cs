using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class RolesController : Controller
    {
        IRepository repo;

        public RolesController(IRepository r)
        {
            repo = r;
        }

        public string AllRoles()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAllRoles(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            return View(repo.GetAllRoles());
        }

        public ActionResult GetRole(Guid id)
        {
            return View(repo.GetRole(id));
        }

        public ActionResult AddRole()
        {
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(Role item)
        {
            repo.SaveRole(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditRole(Guid id)
        {
            var role = repo.GetRole(id);
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", role.StateID);
            return View(role);
        }

        public ActionResult DeleteRole(Guid id)
        {
            return View(repo.GetRole(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteRole(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
