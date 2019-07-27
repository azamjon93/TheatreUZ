using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

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
            return View(repo.GetAllUsers());
        }

        public ActionResult GetUser(Guid id)
        {
            return View(repo.GetUser(id));
        }

        public ActionResult AddUser()
        {
            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name");
            ViewBag.RoleID = new SelectList(repo.GetAllRoles(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User item)
        {
            repo.SaveUser(item);
            return RedirectToAction("Index");
        }

        public ActionResult EditUser(Guid id)
        {
            var user = repo.GetUser(id);

            ViewBag.StateID = new SelectList(repo.GetAllStates(), "ID", "Name", user.StateID);
            ViewBag.RoleID = new SelectList(repo.GetAllRoles(), "ID", "Name", user.RoleID);

            return View(user);
        }

        public ActionResult DeleteUser(Guid id)
        {
            return View(repo.GetUser(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            repo.DeleteUser(id);
            return RedirectToAction("Index");
        }

        public ActionResult UserAllInfo(Guid id)
        {
            var user = repo.GetUser(id);
            var sales = repo.GetAllSales();

            UserAllInfoModel model = new UserAllInfoModel
            {
                User = user,
                Sales = sales.Where(s => s.UserID == id && s.State.Name == "Active").ToList()
            };

            return View(model);
        }

        public ActionResult DeleteSale(Guid id)
        {
            var sh = SaleDeleteCommandHandlerFactory.Build(new SaleDeleteCommand(id));

            if (sh.Execute().Success)
            {
                var uid = (string)Session["UserID"];

                return RedirectToAction("UserAllInfo", new { id = Guid.Parse(uid) });
            }

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
