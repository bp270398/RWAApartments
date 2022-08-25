using ClassLibrary.DAL;
using ClassLibrary.Models;
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
            if(id == 0) { return View(viewName:"Error"); }
            ApartmentVM model = new ApartmentVM
            {
                Apartment = repo.SelectApartment(id),
                Tags = repo.SelectTaggedApartment(id),
                Pictures = repo.SelectApartmentPictures(id),
                Reviews = repo.SelectApartmentReviews(id),
                ContactForm = new Models.ContactForm
                {
                    Arrival = DateTime.Now,
                    Departure = DateTime.Now.AddDays(1),
                }
            };
            model.Reviews = repo.SelectApartmentReviews(id);
            double rating = 0;
            model.Reviews.ToList().ForEach(r => rating += r.Stars);
            model.Rating = rating / model.Reviews.Count();

            User user = (User)Session["user"];
            if (user != null)
            {
                model.ContactForm.Email = user.Email;
                model.ContactForm.PhoneNumber = user.PhoneNumber;
                model.ContactForm.Address = user.Address;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ContactForm(ApartmentVM model)
        {
            //To Validate Google recaptcha
            var response = Request["g-recaptcha-response"];
            string secretKey = "6Le1tgYhAAAAAHSY_BdaFc3UgMf7ysxDNr0If0eH";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");

            //check the status is true or not
            if (status == true)
            {
                ViewBag.Message = "Your Google reCaptcha validation success";
                if (model != null && model.Apartment != null)
                {

                    User user = (User)Session["user"];
                    ApartmentReservation res = new ApartmentReservation();
                    res.Apartment = model.Apartment;
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
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ViewBag.Message = "Your Google reCaptcha validation failed";
            }
            return View("Error");
            //  TO DO !!


        }
    }
}