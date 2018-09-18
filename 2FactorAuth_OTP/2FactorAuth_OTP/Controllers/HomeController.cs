using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace _2FactorAuth_OTP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        [HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];

            if(username == "test" && password == "test")
            {
                var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");

                var secret = "Test_Secret";

                var postData = "api_key=aea90184";
                postData += "&api_secret=B5PMLwiCDajXYLzv";
                postData += "&to=41798371406";
                postData += "&from=\"\"NEXMO\"\"";
                postData += "&text=\"" + secret + "\"";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                ViewBag.Message = responseString;
            }
            else
            {
                ViewBag.Message = "Wrong Credentials";
            }

            return View();
        }

        [HttpPost]
        public ActionResult TokenLogin()
        {
            var token = Request["token"];

            if(token == "Test_Secret")
            {
                return RedirectToAction("About");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}