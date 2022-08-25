using Administration.User_Controls;
using ClassLibrary.DAL;
using ClassLibrary.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class Default : Page
    {
        IList<Apartment> apartments = new List<Apartment>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadDdlItemsStatus();
                LoadDdlItemsCities();
                CookieToForm();
                RebindApartmentList();
            }

        }


        private void LoadDdlItemsCities()
        {
            var CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(CS))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SelectCities";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                ddlCity.DataTextField = dataSet.Tables[0].Columns["Name"].ToString();
                ddlCity.DataValueField = dataSet.Tables[0].Columns["Id"].ToString();
                ddlCity.DataSource = dataSet.Tables[0];
                ddlCity.DataBind();
                connection.Close();
            }
            ddlCity.Items.Add(new ListItem { Value = "0", Text = "All" });
            ddlCity.SelectedValue = "0";
        }
        private void LoadDdlItemsStatus()
        {
            var CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(CS))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SelectApartmentStatuses";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                ddlStatus.DataTextField = dataSet.Tables[0].Columns["NameEng"].ToString();
                ddlStatus.DataValueField = dataSet.Tables[0].Columns["Id"].ToString();
                ddlStatus.DataSource = dataSet.Tables[0];
                ddlStatus.DataBind();
                connection.Close();
            }
            ddlStatus.Items.Add(new ListItem { Value = "0", Text = "All" });
            ddlStatus.SelectedValue = "0";
        }

        private void RebindApartmentList()
        {
            LoadApartmentList(int.Parse(ddlCity.SelectedValue), int.Parse(ddlStatus.SelectedValue));
        }
        private void LoadApartmentList(int? cityId, int? statusId)
        {
            apartments = ((IRepo)Application["database"]).SelectApartments();
            IList<Apartment> filteredApartments = new List<Apartment>();


            if ((!cityId.HasValue && !statusId.HasValue) || (cityId.Value == 0 && statusId.Value == 0))
            {
                filteredApartments = apartments;
            }
            else if (cityId.HasValue && cityId.Value != 0)
            {
                apartments.ToList().ForEach(apartment =>
                {
                    if (apartment.City.Id == cityId.Value)
                    {
                        filteredApartments.Add(apartment);
                    }
                });
                if (statusId.HasValue && statusId.Value != 0)
                {
                    filteredApartments = filteredApartments.ToList().FindAll(apt => apt.ApartmentStatus.Id == statusId.Value);
                }
            }
            else if (statusId.HasValue && statusId.Value != 0)
            {
                apartments.ToList().ForEach(apartment =>
                {
                    if (apartment.ApartmentStatus.Id == statusId.Value)
                    {
                        filteredApartments.Add(apartment);
                    }
                });
            }
            
            if (filteredApartments.Count == 0)
            {
                lblResult.Visible = true;
            }
            else
            {
                lblResult.Visible = false;
            }
            repeaterApartments.DataSource = filteredApartments;
            repeaterApartments.DataBind();

        }

        protected string ShowCityName(string cityId)
        {
            City city = ((IRepo)Application["database"]).SelectCity(int.Parse(cityId));
            return city.Name;
        }
        protected string ShowApartmentPictures(string apartmentId)
        {
            IList<ApartmentPicture> apartmentPictures = ((IRepo)Application["database"]).SelectApartmentPictures(int.Parse(apartmentId));
            return apartmentPictures.Count.ToString();
        }

        protected void repeaterApartments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewApartment")
            {
                Apartment apartment = ((IRepo)Application["database"]).SelectApartment(int.Parse(e.CommandArgument.ToString()));

                Response.Redirect(String.Format("ApartmentInfo.aspx?id={0}", apartment.Id.ToString()));

            }
            if (e.CommandName == "EditApartment")
            {
                Apartment apartment = ((IRepo)Application["database"]).SelectApartment(int.Parse(e.CommandArgument.ToString()));

                Response.Redirect(String.Format("ApartmentEditor.aspx?id={0}", apartment.Id.ToString()));

            }
        }

        protected void btnAddApartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentEditor.aspx");
        }


        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartmentList();
            FormToCookie();
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindApartmentList();
            FormToCookie();
        }

        private void FormToCookie()
        {
            HttpCookie cookie = Request.Cookies["ApartmentList"];
            if (cookie == null)
            {
                cookie = new HttpCookie("ApartmentsList");
            }
            cookie["Status"] = ddlStatus.SelectedValue;
            cookie["City"] = ddlCity.SelectedValue;
            cookie.Expires = DateTime.Now.AddMinutes(30);
        }
        private void CookieToForm()
        {
            HttpCookie cookie = Request.Cookies["ApartmentList"];
            if (cookie != null)
            {
                if (cookie["Status"] != null)
                {
                    ddlStatus.SelectedValue = cookie["Status"];
                }
                if (cookie["City"] != null)
                {
                    ddlCity.SelectedValue = cookie["City"];
                }
            }
        }
    }
}