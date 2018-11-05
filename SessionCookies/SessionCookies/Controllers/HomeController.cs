using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SessionCookies.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if(Request.Cookies.AllKeys.Contains("username"))
            {
                return RedirectToAction("About");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
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

        [HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];
            var stayLoggedIn = Request["checkbox"];

            if (username == "test" && password == "test")
            {
                if(stayLoggedIn == "on")
                {
                    HttpCookie httpcookie = new HttpCookie("username");
                    httpcookie.Value = username;
                    httpcookie.Expires = DateTime.Now.AddDays(14);
                    ControllerContext.HttpContext.Response.Cookies.Add(httpcookie);
                }
                else
                {
                    Session.Add("username", username);
                }
                return RedirectToAction("About");
            }
            else
            {
                ViewBag.Message = "Wrong Credentials";
            }

            return RedirectToAction("Index");
        }
    }
}