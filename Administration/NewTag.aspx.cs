using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class NewTag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadTagTypes();
            }
        }

        private void LoadTagTypes()
        {
            ddlTagType.DataSource = ((IRepo)Application["database"]).SelectTagTypes();
            ddlTagType.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int id = int.Parse(ddlTagType.SelectedValue);
            TagType type = ((IRepo)Application["database"]).SelectTagTypes().FirstOrDefault(tag => tag.Id == id);
            ((IRepo)Application["database"]).CreateTag(new ClassLibrary.Models.Tag
            {
                Name = tbTagName.Text,
                NameEng = tbTagNameEng.Text,
                Type = type
            });
            Response.Redirect("Tags.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tags.aspx");

        }
    }
}