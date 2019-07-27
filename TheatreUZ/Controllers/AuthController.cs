using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;

namespace TheatreUZ.Controllers
{
    public class AuthController : Controller
    {
        IRepository repo;

        public AuthController(IRepository r)
        {
            repo = r;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string userStr)
        {
            User user = null;
            JsonResult result = new JsonResult();

            try
            {
                user = JsonConvert.DeserializeObject<User>(userStr);
            }
            catch
            {

            }

            User dbUser = null;

            try
            {
                dbUser = repo.GetUserByEmail(user.Email);
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
                try
                {
                    var response = repo.SaveUser(user);

                    Session["UserID"] = response.ID.ToString();
                    Session["UserName"] = response.ResponseObjects.ElementAt(0) as string;
                    Session["Role"] = (response.ResponseObjects.ElementAt(1) as Role).Name;
                }
                catch
                {

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