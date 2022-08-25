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
    public partial class Users : System.Web.UI.Page
    {
        private IList<ClassLibrary.Models.User> users;
        protected void Page_Load(object sender, EventArgs e)
        {
            users = ((IRepo)Application["database"]).SelectUsers();
            lblResult.Visible = false;

            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                loadUsers();
                toggleForm(false);
            }
            else
            {
                toggleForm(true);
            }
        }

        private void toggleForm(bool status)
        {
            txtUsername.Enabled = status;
            txtEmail.Enabled = status;
            txtPhoneNumber.Enabled = status;
            txtAddress.Enabled = status;
        }

        private void loadUsers()
        {
            lbUsers.DataSource = users;
            lbUsers.DataValueField = "Id";
            lbUsers.DataTextField = "Username";
            lbUsers.DataBind();
        }

        protected void lbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userId = int.Parse(lbUsers.SelectedValue);
            var selectedUser = users.SingleOrDefault(u => u.Id == userId);
            if (selectedUser != null)
            {
                txtUsername.Text = selectedUser.Username;
                txtEmail.Text = selectedUser.Email;
                txtAddress.Text = selectedUser.Address;
                txtPhoneNumber.Text = selectedUser.PhoneNumber;
            }
        }

        protected void updateUser_Click(object sender, EventArgs e)
        {
            var userId = int.Parse(lbUsers.SelectedValue);
            var selectedUser = users.SingleOrDefault(u => u.Id == userId);

            selectedUser.Username = txtUsername.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.Address = txtAddress.Text;
            selectedUser.PhoneNumber = txtPhoneNumber.Text;

            try
            {
                // Update DB
                ((IRepo)Application["database"]).UpdateUser(selectedUser);

                // Update SESSION
                if (((User)Session["user"]).Id == userId)
                {
                    Session["user"] = selectedUser;
                }

                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblResult.Text = "Pogreška: Korisnik s tim korisničkim imenom postoji!";
                            lblResult.Visible = true;
                            txtUsername.Text = "";
                            break;
                    }
                }
            }
        }
    }
}