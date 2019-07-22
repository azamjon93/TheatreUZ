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

        public ActionResult AddUser()
        {
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var rolesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());

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

            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name");
            ViewBag.RoleID = new SelectList(rolesQueryHandler.Get(), "ID", "Name");

            var user = userQueryHandler.Get();

            return View(user);
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
