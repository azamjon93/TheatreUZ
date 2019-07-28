using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

namespace TheatreUZ.Controllers
{
    public class UsersController : Controller
    {
        IRepository repo;

        public UsersController(IRepository r)
        {
            repo = r;
        }

        public string AllUsers()
        {
            try
            {
                return JsonConvert.SerializeObject(repo.GetAllUsers(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
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
                return View(repo.GetAllUsers());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetUser(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetUser(id));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AddUser()
        {
            if (TAuth.IsAdmin())
            {
                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
                ViewBag.RoleID = new SelectList(repo.GetAllRoles(), "ID", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddUser(User item)
        {
            if (TAuth.IsAdmin())
            {
                repo.SaveUser(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditUser(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                var user = repo.GetUser(id);

                ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", user.StateID);
                ViewBag.RoleID = new SelectList(repo.GetAllRoles(), "ID", "Name", user.RoleID);

                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteUser(Guid id)
        {
            if (TAuth.IsAdmin())
            {
                return View(repo.GetUser(id));
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
                repo.DeleteUser(id);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UserAllInfo()
        {
            if (TAuth.IsLogged() && !TAuth.IsAdmin())
            {
                var user = repo.GetUser(Guid.Parse((string)Session["UserID"]));
                var sales = repo.GetAllSales();

                UserAllInfoModel model = new UserAllInfoModel
                {
                    User = user,
                    Sales = sales.Where(s => s.UserID == user.ID && s.State.Name == "Active").ToList()
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteSale(Guid id)
        {
            if (TAuth.IsLogged() && !TAuth.IsAdmin())
            {
                repo.DeleteSale(id);

                return RedirectToAction("UserAllInfo", new { id = Guid.Parse((string)Session["UserID"]) });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
