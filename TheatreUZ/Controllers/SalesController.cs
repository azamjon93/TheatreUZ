using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class SalesController : Controller
    {
        public string AllSales()
        {
            var handler = SaleQueryHandlerFactory.Build(new AllSalesQuery());
            var sales = handler.Get();

            try
            {
                return JsonConvert.SerializeObject(sales.ToList(), Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            var handler = SaleQueryHandlerFactory.Build(new AllSalesQuery());

            return View(handler.Get());
        }

        public ActionResult GetSale(Guid id)
        {
            var handler = SaleQueryHandlerFactory.Build(new OneSaleQuery(id));

            return View(handler.Get());
        }

        public ActionResult AddSale()
        {
            var usersQueryHandler = UserQueryHandlerFactory.Build(new AllUsersQuery());
            var spectaclesQueryHandler = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery());
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());

            ViewBag.UserID = new SelectList(usersQueryHandler.Get(), "ID", "Name");
            ViewBag.SpectacleID = new SelectList(spectaclesQueryHandler.Get(), "ID", "Name");
            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddSale(Sale item)
        {
            var handler = SaleSaveCommandHandlerFactory.Build(new SaleSaveCommand(item));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        public ActionResult EditSale(Guid id)
        {
            var saleQueryHandler = SaleQueryHandlerFactory.Build(new OneSaleQuery(id));
            var spectaclesQueryHandler = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery());
            var statesQueryHandler = StateQueryHandlerFactory.Build(new AllStatesQuery());
            var usersQueryHandler = UserQueryHandlerFactory.Build(new AllUsersQuery());

            var notification = saleQueryHandler.Get();

            ViewBag.UserID = new SelectList(usersQueryHandler.Get(), "ID", "Name", notification.UserID);
            ViewBag.SpectacleID = new SelectList(spectaclesQueryHandler.Get(), "ID", "Name", notification.SpectacleID);
            ViewBag.StateID = new SelectList(statesQueryHandler.Get(), "ID", "Name", notification.StateID);

            return View(notification);
        }

        public ActionResult DeleteSale(Guid id)
        {
            var handler = SaleQueryHandlerFactory.Build(new OneSaleQuery(id));

            return View(handler.Get());
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var handler = SaleDeleteCommandHandlerFactory.Build(new SaleDeleteCommand(id));
            var response = handler.Execute();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
