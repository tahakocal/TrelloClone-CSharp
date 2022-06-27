﻿using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Do");
        }

        #region Register

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(User registerDetails)
        {
            if (ModelState.IsValid)
            {
                using (var databaseContext = new ToDoDatabaseEntities())
                {

                    User reglog = new User();

                    reglog.FirstName = registerDetails.FirstName;
                    reglog.LastName = registerDetails.LastName;
                    reglog.Mail = registerDetails.Mail;
                    reglog.Password = registerDetails.Password;

                    databaseContext.User.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "Kayıt Olundu, Sağ Yukarıdan Giriş Yapabilirsin";
                return View("Register");
            }
            else
            {

                return View("Register", registerDetails);
            }
        }

        #endregion

        #region Login

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user)
        {

            if (ModelState.IsValid)
            {
                using (var databaseContext = new ToDoDatabaseEntities())
                {
                    Session["LoginUser"] = user;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Yanlış Mail Veya Şifre !");
                return View();
            }
        }


      

        #endregion

        #region Logout

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Account");
        }

        #endregion

    }
}