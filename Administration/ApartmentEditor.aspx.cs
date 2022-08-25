using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Administration
{
    public partial class ApartmentEditor : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                string qryString = Request.QueryString["id"];
                int? id = null;
                if (!string.IsNullOrEmpty(qryString))
                {
                    id = int.Parse(qryString);
                }
                if (id.HasValue)
                {
                    var apartment = ((IRepo)Application["database"]).SelectApartment(id.Value);
                    LoadApartmentData(apartment);
                }
                RebindApartmentOwners();
                RebindCities();
                RebindStatuses();
                RebindTags();
            }
        }

        private void LoadApartmentData(Apartment apartment)
        {
            ddlStatus.SelectedValue = apartment.ApartmentStatus.Id.ToString();
            ddlApartmentOwner.SelectedValue = apartment.ApartmentOwner.Id.ToString();
            tbName.Text = apartment.Name;
            tbNameEng.Text = apartment.NameEng;
            tbAddress.Text = apartment.Address;
            ddlCity.SelectedValue = apartment.City.Id.ToString();
            tbPrice.Text = apartment.Price.ToString();
            tbMaxAdults.Text = apartment.MaxAdults.ToString();
            tbMaxChildren.Text = apartment.MaxChildren.ToString();
            tbTotalRooms.Text = apartment.TotalRooms.ToString();
            tbBeachDistance.Text = apartment.BeachDistance?.ToString();

            repTags.DataSource = ((IRepo)Application["database"]).SelectTaggedApartment(apartment.Id);
            repTags.DataBind();

            IList<ApartmentPicture> existingPictures = ((IRepo)Application["database"]).SelectApartmentPictures(apartment.Id);
            repApartmentPictures.DataSource = existingPictures;
            repApartmentPictures.DataBind();

        }

        private void RebindApartmentOwners()
        {
            ddlApartmentOwner.DataSource = ((IRepo)Application["database"]).SelectApartmentOwners();
            ddlApartmentOwner.DataBind();
        }
        private void RebindCities()
        {
            ddlCity.DataSource = ((IRepo)Application["database"]).SelectCities();
            ddlCity.DataBind();
        }
        private void RebindStatuses()
        {
            ddlStatus.DataSource = ((IRepo)Application["database"]).SelectApartmentStatuses();
            ddlStatus.DataBind();
        }
        private void RebindTags()
        {
            ddlTags.DataSource = ((IRepo)Application["database"]).SelectTags();
            ddlTags.DataBind();
        }

        protected void ddlTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags();
            var newTag = GetSelectedTag();
            if (tags.Any(x => x.Id == newTag.Id))
                return;
            tags.Add(newTag);
            repTags.DataSource = tags;
            repTags.DataBind();
        }

        private Tag GetSelectedTag()
        {
            var newTag = new Tag
            {
                Id = int.Parse(ddlTags.SelectedItem.Value),
                Name = ddlTags.SelectedItem.Text
            };
            return newTag;

        }

        private List<Tag> GetRepeaterTags()
        {
            var repTagsItems = repTags.Items;
            var tags = new List<Tag>();
            foreach (RepeaterItem item in repTagsItems)
            {
                var tag = new Tag
                {
                    Id = int.Parse((item.FindControl("hidTagId") as HiddenField).Value),
                    Name = (item.FindControl("txtTagName") as Label).Text
                };
                tags.Add(tag);
            }
            return tags;

        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags();
            var lbSender = (LinkButton)sender;
            var parentItem = (RepeaterItem)lbSender.Parent;

            var hidTagId = (HiddenField)parentItem.FindControl("hidTagId");
            var tagId = int.Parse(hidTagId.Value);
            var existingTag = tags.FirstOrDefault(x => x.Id == tagId);
            tags.Remove(existingTag);
            repTags.DataSource = tags;
            repTags.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var files = SaveUploadedImagesToDisk();
            var apartmentPictures =
            files.Select(path => new ApartmentPicture
            {
                Path = path,
                Name = Path.GetFileNameWithoutExtension(path),
                IsRepresentative = false,
                
            }).ToList();

            if (Request.QueryString["id"] == null)
            {
                int apartmentId = ((IRepo)Application["database"]).CreateApartment(GetApartmentFromForm());
                GetRepeaterTags().ForEach(tag =>
                {
                    ((IRepo)Application["database"]).CreateTaggedApartment(new TaggedApartment
                    {
                        ApartmentId = apartmentId,
                        TagId = tag.Id
                    });
                });
                List<int> picIds = new List<int>();
                apartmentPictures.ForEach(apartmentPicture =>
                {
                    apartmentPicture.Apartment = ((IRepo)Application["database"]).SelectApartment(apartmentId);
                    int id = ((IRepo)Application["database"]).CreateApartmentPicture(apartmentPicture);
                    picIds.Add(id);
                });
            }
            else
            {
                //save Apartment
                Apartment apartment = GetApartmentFromForm();
                apartment.Id = int.Parse(Request.QueryString["id"]);
                ((IRepo)Application["database"]).UpdateApartment(apartment);

                //save Tags
                IList<Tag> oldTags = ((IRepo)Application["database"]).SelectTaggedApartment(int.Parse((Request.QueryString["id"])));
                List<Tag> newTags = GetRepeaterTags();
                // remove unnecessary tags
                oldTags.ToList().ForEach(oldTag =>
                {
                    if (!newTags.Any(newTag => newTag.Id == oldTag.Id))
                    {
                        ((IRepo)Application["database"]).DeleteTaggedApartment(apartment.Id, oldTag.Id);
                    }
                });
                // add new tags
                newTags.ForEach(newTag =>
                {
                    if (!oldTags.Any(oldTag => oldTag.Id == newTag.Id))
                    {
                        ((IRepo)Application["database"]).CreateTaggedApartment(new TaggedApartment { ApartmentId = apartment.Id, TagId = newTag.Id });
                    }
                });

                //save Photos
                apartmentPictures.ForEach(apartmentPicture =>
                {
                    List<int> picIds = new List<int>();

                    apartmentPicture.Apartment = ((IRepo)Application["database"]).SelectApartment(apartment.Id);
                    int id = ((IRepo)Application["database"]).CreateApartmentPicture(apartmentPicture);
                    picIds.Add(id);
                });
                List<ApartmentPicture> picturesForRemoval = new List<ApartmentPicture>();
                GetRepeaterPictures().TryGetValue("picturesForRemoval", out picturesForRemoval);

                picturesForRemoval.ForEach(picture =>
                {
                    ((IRepo)Application["database"]).DeleteApartmentPicture(picture.Id);
                });

            }
            Response.Redirect("Default.aspx");
        }

        private Apartment GetApartmentFromForm()
        {
            int statusId = int.Parse(ddlStatus.SelectedValue);
            int? cityId = int.Parse(ddlCity.SelectedValue);
            if (cityId == 0)
                cityId = null;
            int ownerId = int.Parse(ddlApartmentOwner.SelectedValue);
            double price = double.Parse(tbPrice.Text);
            int? maxAdults = null;
            if (!string.IsNullOrEmpty(tbMaxAdults.Text))
                maxAdults = int.Parse(tbMaxAdults.Text);
            int? maxChildren = null;
            if (!string.IsNullOrEmpty(tbMaxChildren.Text))
                maxChildren = int.Parse(tbMaxChildren.Text);
            int? totalRooms = null;
            if (!string.IsNullOrEmpty(tbTotalRooms.Text))
                totalRooms = int.Parse(tbTotalRooms.Text);
            int? beachDistance = null;
            if (!string.IsNullOrEmpty(tbBeachDistance.Text))
            {
                beachDistance = int.Parse(tbBeachDistance.Text);
                return new Apartment
                {
                    ApartmentOwner = ((IRepo)Application["database"]).SelectApartmentOwner(ownerId),
                    ApartmentStatus = ((IRepo)Application["database"]).SelectApartmentStatus(statusId),
                    City = ((IRepo)Application["database"]).SelectCity(cityId.Value),
                    Address = tbAddress.Text,
                    Name = tbName.Text,
                    NameEng = tbNameEng.Text,
                    Price = price,
                    MaxAdults = maxAdults.Value,
                    MaxChildren = maxChildren.Value,
                    TotalRooms = totalRooms.Value,
                    BeachDistance = beachDistance.Value,

                };
            }

            return new Apartment
            {
                ApartmentOwner = ((IRepo)Application["database"]).SelectApartmentOwner(ownerId),
                ApartmentStatus = ((IRepo)Application["database"]).SelectApartmentStatus(statusId),
                City = ((IRepo)Application["database"]).SelectCity(cityId.Value),
                Address = tbAddress.Text,
                Name = tbName.Text,
                NameEng = tbNameEng.Text,
                Price = price,
                MaxAdults = maxAdults.Value,
                MaxChildren = maxChildren.Value,
                TotalRooms = totalRooms.Value,
            };
        }

        private List<string> SaveUploadedImagesToDisk()
        {
            string PROJECT_DIR = AppDomain.CurrentDomain.BaseDirectory;
            string SOLUTION_DIR = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            string STARTUP_PATH = HttpContext.Current.Server.MapPath("~");
            //C:\Users\beata\Desktop\Projects\RWA\RWAApartments\ClassLibrary\

            string DIR = SOLUTION_DIR + "\\ClassLibrary\\Content\\Pictures\\";

            var files = new List<string>();
            if (uplImages.HasFiles)
            {
                if (!Directory.Exists(DIR))
                {
                    Directory.CreateDirectory(DIR);

                }
                foreach (HttpPostedFile uploadedFile in uplImages.PostedFiles)
                {
                    var uplImagePath = Path.Combine(DIR, uploadedFile.FileName);
                    uploadedFile.SaveAs(uplImagePath);
                    files.Add(uplImagePath);
                }
            }
            return files;
        }

        private Dictionary<string, List<ApartmentPicture>> GetRepeaterPictures()
        {
            var repApartmentPicturesItems = repApartmentPictures.Items;
            var pictures = new List<ApartmentPicture>();
            var picturesForRemoval = new List<ApartmentPicture>();

            foreach (RepeaterItem item in repApartmentPicturesItems)
            {
                var pic = new ApartmentPicture
                {
                    Id = int.Parse((item.FindControl("hidApartmentPictureId") as HiddenField).Value),
                    Name = (item.FindControl("txtApartmentPicture") as TextBox).Text,
                    Path = (item.FindControl("imgApartmentPicture") as Image).ImageUrl,
                    IsRepresentative = (item.FindControl("cbIsRepresentative") as CheckBox).Checked
                };

                if (!(item.FindControl("cbDelete") as CheckBox).Checked)
                {
                    pictures.Add(pic);
                }
                else
                {
                    picturesForRemoval.Add(pic);
                }
            }
            Dictionary<string, List<ApartmentPicture>> dict = new Dictionary<string, List<ApartmentPicture>>
            {
                { nameof(pictures), pictures },
                { nameof(picturesForRemoval), picturesForRemoval }
            };
            return dict;
        }
    }
}