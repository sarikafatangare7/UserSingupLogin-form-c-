using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserSignupLogin.Models;
//class.System.String;

namespace UserSignupLogin.Controllers
{
    public class HomeController : Controller
    {
        DBUserSignupLoginEntities db = new DBUserSignupLoginEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBUserInfoes.ToList());
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(TBUserInfo tBUserInfo)
        {
            if (db.TBUserInfoes.Any(x => x.UserName == tBUserInfo.UserName))
            {
                ViewBag.Notification = "This account has already existed!!";
                return View();
            }
            else
            {
                db.TBUserInfoes.Add(tBUserInfo);
                db.SaveChanges();

                Session["ID"] = tBUserInfo.ID.ToString();
                Session["UserName"] = tBUserInfo.UserName.ToString();
                return RedirectToAction("Index", "Home");

            }
            
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBUserInfo tBUserInfo)
        {
            var checkLogin = db.TBUserInfoes.Where(x => x.UserName.Equals(tBUserInfo.UserName) && x.Password.Equals(tBUserInfo.Password)).FirstOrDefault();
            if(checkLogin != null)
            {
                Session["ID"] = tBUserInfo.ID.ToString();
                Session["UserName"] = tBUserInfo.UserName.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }
    }
}