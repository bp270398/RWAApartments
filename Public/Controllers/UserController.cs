using ClassLibrary.DAL;
using ClassLibrary.Models;
using Public.Models.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class UserController : Controller
    {
        IRepo repo = RepoFactory.GetRepo();

        

        public ActionResult Manage(User user)
        {
            user = (ClassLibrary.Models.User)Session["user"];

            if (user == null)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                UserVM model = new UserVM
                {
                    User = repo.SelectUser(user.Id),
                    Reservations = repo.SelectUserReservations(user.Id),
                    Reviews = repo.SelectUserReviews(user.Id)
                };
                return View("Manage", model);
            }

        }

        public ActionResult LogIn(User user)
        {
            if (user == null)
            {
                user = new User();
                Session["user"] = null;
                return View();
            }
            else
            {
                if (user.PasswordHash != null)
                {
                    user.PasswordHash = Cryptography.HashPassword(user.PasswordHash);
                }
                if (IsAuthenticated(user))
                {
                    Session["user"] = repo.SelectUsers().First(u => u.Username == user.Username);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    Session["user"] = null;
                    return View();
                }
            }

        }

        public bool IsAuthenticated(User user)
        {
            return repo.SelectUsers().Any(u => (u.Username == user.Username) && (u.PasswordHash == user.PasswordHash));
        }

        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(User user)
        {
            var repo = RepoFactory.GetRepo();

            if (user == null)
            {
                user = new User();
                Session["user"] = null;
                return View();
            }
            else
            {

                if (repo.SelectUsers().Any(u => (u.Username == user.Username)))
                {
                    // Username exists
                    Session["user"] = repo.SelectUsers().First(u => u.Username == user.Username);
                    return RedirectToAction("Index", "Home");

                }
                else if (repo.SelectUsers().Any(u => (u.Username == user.Username) && (u.PasswordHash == user.PasswordHash)))
                {
                    // User exists > log in
                    Session["user"] = null;
                    return View();
                }
                else if (user.Username != null && user.PasswordHash != null)
                {
                    user.PasswordHash = Cryptography.HashPassword(user.PasswordHash);
                    int newUserId = repo.CreateUser(user);
                    Session["user"] = repo.SelectUser(newUserId);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }

        }

    }
}