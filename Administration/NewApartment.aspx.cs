using ClassLibrary.DAL;
using ClassLibrary.Models;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class NewApartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadDdlStatus();
                // new city
                // add AgnoliaPlaces to tbAddress
                // make tbBeachdistance optional
                // make addTags work
                // handle pictures
                // btnSubmit_OnClick

            }
        }
        Apartment newApartment = new Apartment();

        private void LoadDdlStatus()
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
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CreateApartment();
            Response.Redirect(String.Format("ApartmentInfo.aspx?id={0}", newApartment.Id.ToString()));
        }
        private void CreateApartment()
        {
            IList<ApartmentOwner> apartmentOwners = ((IRepo)Application["database"]).SelectApartmentOwners();
            apartmentOwners.ToList().ForEach(owner =>
            {
                if (owner.Name == tbOwner.Text)
                {
                    newApartment.ApartmentOwner = owner;
                }
            });
            if (newApartment.ApartmentOwner == null)
            {
                int ownerId = ((IRepo)Application["database"]).CreateApartmentOwner(new ApartmentOwner()
                {
                    Name = tbOwner.Text,
                    CreatedAt = DateTime.Now,
                });
                newApartment.ApartmentOwner= ((IRepo)Application["database"]).SelectApartmentOwner(ownerId);
            }
            newApartment.NameEng = tbNameEng.Text;
            newApartment.Name = tbNameCro.Text;
            newApartment.ApartmentStatus = ((IRepo)Application["database"]).SelectApartmentStatus(int.Parse(ddlStatus.SelectedValue));
            IList<City> cities = ((IRepo)Application["database"]).SelectCities();
            cities.ToList().ForEach(c =>
            {
                if (c.Name.ToLower().Equals(tbCity.Text.ToLower()))
                {
                    newApartment.City = c;
                }
            });
            if (newApartment.City == null)
            {
                int cityId = 0;
                ((IRepo)Application["database"]).SelectCities().ToList().ForEach(c =>
                {
                    if (c.Name.ToLower().Equals(tbCity.Text.ToLower()))
                    {
                        cityId = c.Id;
                    }
                });
                if (cityId != 0)
                {
                    newApartment.City = ((IRepo)Application["database"]).SelectCity(cityId);
                }
                else
                {
                    ((IRepo)Application["database"]).CreateCity(new City { Name = tbCity.Text });
                    newApartment.City = ((IRepo)Application["database"]).SelectCity(cities.Count + 1);
                }
            }
            newApartment.Address = tbAddress.Text;
            if (!tbBeachDistance.Text.Equals(""))
            {
                newApartment.BeachDistance = int.Parse(tbBeachDistance.Text);
            }
            else
            {
                newApartment.BeachDistance = 0;
            }
            newApartment.MaxAdults = int.Parse(tbMaxAdults.Text);
            newApartment.MaxChildren = int.Parse(tbMaxChildren.Text);
            newApartment.TotalRooms = int.Parse(tbTotalRooms.Text);
            newApartment.Price = double.Parse(tbPrice.Text);

            newApartment.Id = ((IRepo)Application["database"]).CreateApartment(newApartment);
        }




    }
}