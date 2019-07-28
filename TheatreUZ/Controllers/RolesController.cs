using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

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
            if (TAuth.IsAdmin())
            {
                return View(repo.GetAllRoles());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetRole(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetRole(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddRole()
        {
            if (TAuth.IsAdmin())
            {
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddRole(Role item)
        {
            repo.SaveRole(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditRole(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                var role = repo.GetRole(id);

                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", role.StateID);

                return View(role);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteRole(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetRole(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                repo.DeleteRole(id);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
