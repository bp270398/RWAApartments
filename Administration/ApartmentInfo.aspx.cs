using Administration.User_Controls;
using ClassLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class ApartmentInfo : System.Web.UI.Page
    {
        int apartmentId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (Request.QueryString["id"] != null && !Page.IsPostBack)
            {
                apartmentId = int.Parse(Request.QueryString["id"]);
                LoadApartmentInfo();
            }
        }

        private void LoadApartmentInfo()
        {           
            apartmentInfoControl.Apartment = ((IRepo)Application["database"]).SelectApartment(apartmentId);

            (apartmentInfoControl.FindControl("lNameEng") as Label).Text = apartmentInfoControl.Apartment.NameEng;
            (apartmentInfoControl.FindControl("lStatus") as Label).Text = apartmentInfoControl.Apartment.ApartmentStatus.NameEng;
            (apartmentInfoControl.FindControl("lCreatedAt") as Label).Text = apartmentInfoControl.Apartment.CreatedAt.ToString();
            (apartmentInfoControl.FindControl("lOwner") as Label).Text = apartmentInfoControl.Apartment.Name;
            
            (apartmentInfoControl.FindControl("lPrice") as Label).Text = apartmentInfoControl.Apartment.Price.ToString();
            (apartmentInfoControl.FindControl("lMaxAdults") as Label).Text = apartmentInfoControl.Apartment.MaxAdults.ToString();
            (apartmentInfoControl.FindControl("lMaxChildten") as Label).Text = apartmentInfoControl.Apartment.MaxChildren.ToString();
            (apartmentInfoControl.FindControl("lTotalRooms") as Label).Text = apartmentInfoControl.Apartment.TotalRooms.ToString();
            
        }
    }
}