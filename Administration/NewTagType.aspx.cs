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
    public partial class NewTagType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tags.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IList<TagType> types = ((IRepo)Application["database"]).SelectTagTypes();
            TagType newType = new TagType
            {
                Name = tbTagTypeName.Text,
                NameEng = tbTagTypeNameEng.Text,
            };
            ((IRepo)Application["database"]).CreateTagType(newType);
            Response.Redirect("Tags.aspx");
        }
    }
}