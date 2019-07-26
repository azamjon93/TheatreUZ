using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class UsersController : Controller
    {
        public string AllUsers()
        {
            var handler = UserQueryHandlerFactory.Build(new AllUsersQuery());
            var users = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(users.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            var handler = UserQueryHandlerFactory.Build(new AllUsersQuery());

            return View(handler.Get());
        }

        public ActionResult GetUser(Guid id)
        {
            var handler = UserQueryHandlerFactory.Build(new OneUserQuery(id));

            return View(handler.Get());
        }

        public ActionResult UserAllInfo(Guid id)
        {
            var userHandler = UserQueryHandlerFactory.Build(new OneUserQuery(id));
            var salesHandler = SaleQueryHandlerFactory.Build(new AllSalesQuery());

            UserAllInfoModel model = new UserAllInfoModel
            {
                User = userHandler.Get(),
                Sales = salesHandler.Get().Where(s => s.UserID == id && s.State.Name == "Active").ToList()
            };

            return View(model);
        }

        public ActionResult AddUser()
        {
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var rolesQueryHandler = RoleQueryHandlerFactory.Build(new AllRolesQuery());

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name");
            ViewBag.RoleID = new SelectList(rolesQueryHandler.Get(), "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User item)
        {
            var handler = UserSaveCommandHandlerFactory.Build(new UserSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditUser(Guid id)
        {
            var userQueryHandler = UserQueryHandlerFactory.Build(new OneUserQuery(id));
            var rolesQueryHandler = RoleQueryHandlerFactory.Build(new AllRolesQuery());
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());

            var user = userQueryHandler.Get();

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name", user.StateID);
            ViewBag.RoleID = new SelectList(rolesQueryHandler.Get(), "ID", "Name", user.RoleID);

            return View(user);
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

        public ActionResult DeleteUser(Guid id)
        {
            var handler = UserQueryHandlerFactory.Build(new OneUserQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = UserDeleteCommandHandlerFactory.Build(new UserDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
