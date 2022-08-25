using ClassLibrary.DAL;
using Public.Models.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Public.Controllers
{
    public class HomeController : Controller
    {
        IRepo repo = RepoFactory.GetRepo();

        public ActionResult Index(ApartmentsVM model)
        {
            model.ApartmentVMs = new Dictionary<int, ApartmentVM>();

            if (model == null)
            {
                model = new ApartmentsVM();
                repo.SelectApartments().ToList().ForEach(apartment =>
                {
                    model.ApartmentVMs.Add(apartment.Id, new ApartmentVM
                    {
                        Apartment = apartment,
                        Pictures = repo.SelectApartmentPictures(apartment.Id),
                        Tags = repo.SelectTaggedApartment(apartment.Id),
                        Reviews = repo.SelectApartmentReviews(apartment.Id),
                    });
                });
            }
            else
            {
                if (model.ApartmentVMs != null)
                {
                    model.ApartmentVMs.Clear();
                }
                repo.SelectApartments().ToList().ForEach(apartment =>
                {
                    bool flag = true; //
                    if (model.Rooms.HasValue && apartment.TotalRooms != model.Rooms.Value) { flag &= false; }
                    if (model.Adults.HasValue && apartment.MaxAdults != model.Adults.Value) { flag &= false; }
                    if (model.Children.HasValue && apartment.MaxChildren != model.Children.Value) { flag &= false; }
                    if (model.CityId.HasValue && apartment.City.Id != model.CityId.Value) { flag &= false; }
                    if (model.IsAvailable == true)
                    {
                        if (apartment.ApartmentStatus.NameEng != "Vacant")
                        { flag &= false; }
                    }
                    if (flag)
                    {
                        model.ApartmentVMs.Add(apartment.Id, new ApartmentVM
                        {
                            Apartment = apartment,
                            Pictures = repo.SelectApartmentPictures(apartment.Id),
                            Tags = repo.SelectTaggedApartment(apartment.Id),
                            Reviews = repo.SelectApartmentReviews(apartment.Id)
                        });
                    }
                });
            }
            model.Cities = repo.SelectCities();
            model.ApartmentVMs.Values.ToList().ForEach(vm =>
            {
                double rating = 0;
                vm.Reviews.ToList().ForEach(r => rating += r.Stars);
                rating = rating == 0 ? rating : rating /= vm.Reviews.Count();
                vm.Rating = rating;
            });

            return View(model);
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
    }
}