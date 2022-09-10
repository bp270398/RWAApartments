using ClassLibrary.DAL;
using ClassLibrary.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using Public.Models.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class ApartmentController : Controller
    {
        readonly IRepo repo = RepoFactory.GetRepo();
        public ActionResult Apartment(int id)
        {
            Session["aptId"] = id;
            if(id == 0 || id.Equals(null)) { return View(viewName:"Error"); }
            ApartmentVM model = new ApartmentVM
            {
                Apartment = repo.SelectApartment(id),
                Tags = repo.SelectTaggedApartment(id),
                Pictures = repo.SelectApartmentPictures(id),
                Reviews = repo.SelectApartmentReviews(id),
                NewReview = new ApartmentReview { Stars = 5},
                ContactForm = new Models.ContactForm
                {
                    Arrival = DateTime.Now,
                    Departure = DateTime.Now.AddDays(1),
                }
            };
            ViewBag.Id = id;
            model.Reviews = repo.SelectApartmentReviews(id);
            double rating = 0;
            model.Reviews.ToList().ForEach(r => rating += r.Stars);
            model.Rating = Math.Round(rating / model.Reviews.Count(),2);


            User user = (User)Session["user"];
            if (user != null)
            {
                model.ContactForm.Email = user.Email;
                model.ContactForm.PhoneNumber = user.PhoneNumber;
                model.ContactForm.Address = user.Address;
            }
            return View(model);
        }
        public ActionResult ContactForm(ApartmentVM model)
        {
            if (model != null && model.ContactForm != null)
            {
                int aptId =int.Parse(Session["aptId"].ToString());
                User user = (User)Session["user"];
                ApartmentReservation res = new ApartmentReservation();
                res.Apartment = repo.SelectApartment(aptId);
                res.User = user ?? null;
                res.Details = model.ContactForm.GetDetails();
                res.UserName = model.ContactForm.FName + " " + model.ContactForm.LName;
                res.Email = model.ContactForm.Email;
                res.PhoneNumber = model.ContactForm.PhoneNumber;
                res.Address = model.ContactForm.Address;

                repo.CreateApartmentReservation(res);

                if ((User)Session["user"] != null)
                {
                    return RedirectToAction("Manage", "User");
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult ReviewForm(ApartmentReview model)
        {
            ApartmentReview apartmentReview = new ApartmentReview();
            int aptId = int.Parse(Session["aptId"].ToString());
            if (model != null)
            {
                if ((User)Session["user"] != null)
                {
                    apartmentReview.User = (User)Session["user"];
                }
                apartmentReview.Apartment = repo.SelectApartment(aptId) ?? null;
                apartmentReview.Stars = model.Stars;
                apartmentReview.Details = model.Details;
                repo.CreateApartmentReview(apartmentReview);

                if((User)Session["user"] != null)
                {
                    return RedirectToAction("Manage", "User");
                }
            }
            return RedirectToAction("Apartment", aptId);

        }
    }
}