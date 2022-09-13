using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class Tags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadTags();
                LoadTagTypes();
            }

        }

       

        private void LoadTagTypes()
        {
            var CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(CS))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SelectTagTypes";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                
                connection.Close();
            }
        }

        private void LoadTags()
        {
            repeaterTags.DataSource = ((IRepo)Application["database"]).SelectTags();
            repeaterTags.DataBind();
        }

        protected void repeaterTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                int usage = int.Parse((item.FindControl("Usage") as Label).Text);
                if (usage != 0)
                {
                    item.FindControl("btnDeleteTag").Visible = false;
                }
            }
        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            List<Tag> tags = new List<Tag>();
            foreach(RepeaterItem item in repeaterTags.Items)
            {
                int currentTagId = int.Parse((item.FindControl("Id") as Label).Text);
                tags.Add(((IRepo)Application["database"]).SelectTag(currentTagId));
            }
            var lbSender = (LinkButton)sender;
            var parentItem = (RepeaterItem)lbSender.Parent;

            var lId = (Label)parentItem.FindControl("Id");
            var tagId = int.Parse(lId.Text);
            var existingTag = tags.FirstOrDefault(x => x.Id == tagId);
            tags.Remove(existingTag);
            ((IRepo)Application["database"]).DeleteTag(existingTag.Id);
            LoadTags();

        }

        protected void btnAddTag_Click1(object sender, EventArgs e)
        {
            Response.Redirect("NewTag.aspx");
        }

        protected void btnAddTagType_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewTagType.aspx");
        }
    }
}