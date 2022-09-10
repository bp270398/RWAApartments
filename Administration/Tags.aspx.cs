﻿using ClassLibrary.DAL;
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
            else
            {
                toggleForm(true);
            }
        }

        private void toggleForm(bool status)
        {
            tbTagNameCro.Enabled = status;
            tbTagNameEng.Enabled = status;
            tbTagTypeNameCro.Enabled = status;
            tbTagTypeNameEng.Enabled = status;
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
                ddlTagTypes.DataTextField = dataSet.Tables[0].Columns["NameEng"].ToString();
                ddlTagTypes.DataValueField = dataSet.Tables[0].Columns["Id"].ToString();
                ddlTagTypes.DataSource = dataSet.Tables[0];
                ddlTagTypes.DataBind();
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

        
        protected void btnOpenModalNewTagType_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
        }

        protected void btnOpenModalNewTag_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
        }

        protected void btnAddTagType_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).CreateTagType(new TagType()
            {
                Name = tbTagTypeNameCro.Text,
                NameEng = tbTagTypeNameEng.Text
            });

            string alertCommand = "alert('Successfully inserted a new tag type: " + tbTagTypeNameEng.Text + ".')";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", alertCommand, true);

            tbTagTypeNameCro.Text = "";
            tbTagTypeNameEng.Text = "";

        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).CreateTag(new Tag()
            {
                Name = tbTagNameCro.Text,
                NameEng = tbTagNameEng.Text
            });

            string alertCommand = "alert('Successfully inserted a new tag:  " + tbTagNameEng.Text + " .')";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", alertCommand, true);

            tbTagNameCro.Text = "";
            tbTagNameEng.Text = "";
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
    }
}