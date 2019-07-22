using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class RolesController : Controller
    {
        public string AllRoles()
        {
            var handler = RoleQueryHandlerFactory.Build(new AllRolesQuery());
            var roles = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(roles.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            var handler = RoleQueryHandlerFactory.Build(new AllRolesQuery());

            return View(handler.Get());
        }

        public ActionResult GetRole(Guid id)
        {
            var handler = RoleQueryHandlerFactory.Build(new OneRoleQuery(id));

            return View(handler.Get());
        }

        public ActionResult AddRole()
        {
            var handler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            ViewBag.StateID = new SelectList(handler.Get(), "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddRole(Role item)
        {
            var handler = RoleSaveCommandHandlerFactory.Build(new RoleSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditRole(Guid id)
        {
            var roleQueryHandler = RoleQueryHandlerFactory.Build(new OneRoleQuery(id));
            var role = roleQueryHandler.Get();

            var StatesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var states = StatesQueryHandler.Get();

            ViewBag.StateID = new SelectList(states, "ID", "Name", role.StateID);

            return View(role);
        }

        public ActionResult DeleteRole(Guid id)
        {
            var handler = RoleQueryHandlerFactory.Build(new OneRoleQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = RoleDeleteCommandHandlerFactory.Build(new RoleDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
