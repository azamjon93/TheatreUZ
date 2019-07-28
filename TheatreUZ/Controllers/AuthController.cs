using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using TheatreUZ.Models;
using TheatreUZ.Security;

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
        
        public JsonResult LogIn(string userStr)
        {
            bool success = false;
            string message = "There was an error";
            
            try
            {
                User user = JsonConvert.DeserializeObject<User>(userStr);
                User dbUser = repo.GetUserByEmail(user.Email);
                if (dbUser == null)
                {
                    message = "User with e-mail/password was not found";
                }
                else
                {
                    if (TAuth.Hash(user.PasswordHash) == dbUser.PasswordHash)
                    {
                        Session["User"] = dbUser;
                        Session["UserID"] = dbUser.ID.ToString();
                        Session["UserName"] = dbUser.Name;
                        Session["Role"] = dbUser.Role.Name;

                        success = true;
                        message = "OK";
                    }
                    else
                    {
                        message = "User with e-mail/password was not found";
                    }
                }
            }
            catch
            {

            }
            
            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult AddUser(string userStr)
        {
            bool success = false;
            string message = "There was an error";

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
                User x = repo.GetUserByEmail(user.Email);

                if (x == null)
                {
                    try
                    {
                        var response = repo.SaveUser(user);

                        Session["User"] = user;
                        Session["UserID"] = response.ID.ToString();
                        Session["UserName"] = response.ResponseObjects.ElementAt(0) as string;
                        Session["Role"] = (response.ResponseObjects.ElementAt(1) as Role).Name;

                        success = true;
                        message = "OK";
                    }
                    catch
                    {

                    }
                }
                else
                {
                    message = "User with this e-mail is existing! Use another e-mail.";
                }
            }

            return Json(new { Success = success, Message = message }, JsonRequestBehavior.AllowGet);
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