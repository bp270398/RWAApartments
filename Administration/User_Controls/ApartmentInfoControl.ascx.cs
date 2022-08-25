using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration.User_Controls
{
    public partial class ApartmentInfoControl : System.Web.UI.UserControl
    {
        public Apartment Apartment { get; set; }
        IRepo repo = RepoFactory.GetRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            if (Apartment != null && !IsPostBack)
            {
                FillData();
                FillAppliedTags();
                FillPictures();
            }
        }

        private void FillPictures()
        {
            IList<ApartmentPicture> apartmentPictures = ((IRepo)Application["database"]).SelectApartmentPictures(Apartment.Id);
            if(apartmentPictures.Count > 0)
            {
                rPictures.DataSource = apartmentPictures;
                rPictures.DataBind();
            }
            else
            {
                lInfoPictures.Text = "No pictures found.";
            }
        }

        private void FillAppliedTags()
        {
            IList<Tag> tags = ((IRepo)Application["database"]).SelectTaggedApartment(Apartment.Id);
            if(tags.Count > 0) 
            {
                rTag.DataSource = tags;
                rTag.DataBind();
            }
            else
            {
                lInfoTags.Text = "No tags applied.";
            }
        }
        private void FillData()
        {
            lNameEng.Text = Apartment.NameEng + " (" + Apartment.Name + ")";
            lStatus.Text = Apartment.ApartmentStatus.NameEng;
            lCreatedAt.Text = Apartment.CreatedAt.ToShortDateString();
            lOwner.Text = Apartment.ApartmentOwner.Name;
            lAddress.Text = Apartment.Address + ", " + Apartment.City.Name;
            
            
            lPrice.Text = Apartment.Price.ToString();
            lMaxAdults.Text = Apartment.MaxAdults.ToString();
            lMaxChildten.Text = Apartment.MaxChildren.ToString();
            lTotalRooms.Text = Apartment.TotalRooms.ToString();
        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
       
        protected void trBeachDistance_Load(object sender, EventArgs e)
        {

            if (Apartment!=null && !IsPostBack)
            {
                if (!Apartment.BeachDistance.HasValue && Apartment.BeachDistance.Value != 0)
                {
                    (this.FindControl("trBeachDistance") as PlaceHolder).Visible = false;
                }
                else
                {
                    lBeachDistance.Text = Apartment.BeachDistance.ToString() + "m";
                }
            }
            
        }

        protected void rEditTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                List<Tag> appliedTags = (List<Tag>)((IRepo)Application["database"]).SelectTaggedApartment(Apartment.Id);

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string tagName = ((item.FindControl("cbTag") as CheckBox).Text);
                    if (appliedTags.Any(tag => tag.NameEng == tagName))
                    {
                        (item.FindControl("cbTag") as CheckBox).Checked = true;
                    }
                }
            }
        }

        protected void btnDeleteApartment_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteApartment(int.Parse(Request.QueryString["id"]));
            string alertCommand = "alert('Apartment successfully deleted.')";  // TODO: alert("Tag [TagName] succcessfully deleted");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", alertCommand, true);
            Response.Redirect("Default.aspx");

        }

        protected void btnBackToHomepage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("ApartmentEditor.aspx?id={0}", Request.QueryString["id"]));
        }
    }
}