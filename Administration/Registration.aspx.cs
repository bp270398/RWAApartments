using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    Response.Redirect("Apartments.aspx");
                }

                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            if (IsPostBack && ViewState["user"] != null)
            {
                User u = (User)ViewState["user"];
                ((IRepo)Application["database"]).CreateUser(u);
                Session["user"] = u;

                var method = Request.HttpMethod.ToLower();
                RenderData(u, method);
            }
            base.OnPreRender(e);
        }
        protected void Username_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.ToLower() == "admin")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User u = new User
                {
                    Email = txtEmail.Text,
                    Username = txtUsername.Text,
                    PasswordHash = Cryptography.HashPassword(txtPassword.Text),
                    PhoneNumber = txtPhoneNumber.Text,
                    Address = txtAddress.Text
                };
                ViewState["user"] = u;
            }
        }
        private void RenderData(User u, string method)
        {
            PanelForma.Visible = false;
            PanelIspis.Visible = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='results' class='container p-4'><fieldset>");
            sb.Append($"<legend>{method.ToUpper()} request</legend>");
            sb.Append("<div class='mb-3'><label class='form-label'>Username: </label>");
            sb.Append($"<label class='fw-bold'>{u.Username}</label>");
            sb.Append("</div>");
            sb.Append("<div class='mb-3'><label class='form-label'>Email: </label>");
            sb.Append($"<label class='fw-bold'>{u.Email}</label>");
            sb.Append("</div>");
            sb.Append("<div class='mb-3'><label class='form-label'>Phone number: </label>");
            sb.Append($"<label class='fw-bold'>{u.PhoneNumber}</label>");
            sb.Append("</div>");
            sb.Append($"<a href='/Apartments.aspx' class='btn btn-primary'>Continue</a>");
            sb.Append("</fieldset></div>");

            LiteralControl ispis = new LiteralControl(sb.ToString());
            PanelIspis.Controls.Add(ispis);
        }

        protected void CheckUser_ServerValidate(object source, ServerValidateEventArgs args)
        {
            IList<User> users = ((IRepo)Application["database"]).SelectUsers();
            var user = users.SingleOrDefault(u => u.Username == args.Value);

            if (user != null)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}