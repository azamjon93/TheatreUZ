using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string userStr)
        {
            JsonResult result = new JsonResult();

            User user = null;

            try
            {
                user = JsonConvert.DeserializeObject<User>(userStr);
            }
            catch (Exception)
            {

            }

            var userHandler = UserQueryHandlerFactory.Build(new OneUserByEmailQuery(user.Email));

            User dbUser = null;
            try
            {
                dbUser = userHandler.Get();
            }
            catch (Exception)
            {

            }

            if (dbUser == null)
            {
                result.Data = "404";
            }
            else
            {
                if (OwnSecurity.Hash(user.PasswordHash) == dbUser.PasswordHash)
                {
                    result.Data = "200";

                    Session["UserID"] = dbUser.ID.ToString();
                    Session["UserName"] = dbUser.Name;
                    Session["Role"] = dbUser.Role.Name;
                }
                else
                {
                    result.Data = "404";
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddUser(string userStr)
        {
            User user = null;

            try
            {
                user = JsonConvert.DeserializeObject<User>(userStr);
            }
            catch (Exception)
            {

            }

            if (user != null)
            {
                var handler = UserSaveCommandHandlerFactory.Build(new UserSaveCommand(user));
                var response = handler.Execute();

                if (response.Success)
                {
                    Session["UserID"] = response.ID.ToString();
                    Session["UserName"] = response.ResponseObjects.ElementAt(0) as string;
                    Session["Role"] = (response.ResponseObjects.ElementAt(1) as Role).Name;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}