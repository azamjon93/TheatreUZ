﻿using System;
using System.Linq;
using System.Web.Mvc;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id = 1)
        {
            return View(GetSpectacles(id));
        }

        private SpectaclePageModel GetSpectacles(int currentPage)
        {
            int maxRows = 9;

            var allSpectaclesHandler = SpectacleQueryHandlerFactory.Build(new AllSpectaclesQuery());
            var allSpectacles = allSpectaclesHandler.Get();
            var spectacles = allSpectacles.OrderBy(s => s.PlayDate).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();

            SpectaclePageModel pageModel = null;

            if (spectacles.Count == 0)
            {
                pageModel = new SpectaclePageModel()
                {
                    Spectacles = new System.Collections.Generic.List<Spectacle>(),
                    PageInfo = new PageInfo { PageNumber = currentPage, PageSize = 0, TotalItems = allSpectacles.Count() }
                };
            }
            else
            {
                pageModel = new SpectaclePageModel
                {
                    Spectacles = spectacles,
                    PageInfo = new PageInfo { PageNumber = currentPage, PageSize = spectacles.Count, TotalItems = allSpectacles.Count() }
                };
            }

            return pageModel;
        }

        public ActionResult SpectacleReadMore(Guid id)
        {
            var spectacleHandler = SpectacleQueryHandlerFactory.Build(new OneSpectacleQuery(id));
            var salesHandler = SaleQueryHandlerFactory.Build(new AllSalesQuery());
            var spectacle = spectacleHandler.Get();
            var x = salesHandler.Get().Where(s => s.SpectacleID == id).Sum(a => a.Amount);
            SpectacleReadModel srm = new SpectacleReadModel { Spectacle = spectacle, Image = "~/Content/Images/scene2.jpg", Remain = spectacle.TicketsCount - x };
            return View(srm);
        }

        public JsonResult ToBook(string sale)
        {
            JsonResult result = new JsonResult
            {
                Data = true
            };

            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}