using ClassLibrary.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAL
{
    public class DBRepo : IRepo
    {
        private static readonly string CS = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        /*   Create   */

        public int CreateApartment(Apartment apartment)
        {
            SqlParameter[] parameters = {
                new SqlParameter("OwnerId", apartment.ApartmentOwner.Id),
                new SqlParameter("StatusId", apartment.ApartmentStatus.Id),
                new SqlParameter("CityId", apartment.City.Id),
                new SqlParameter("Address", apartment.Address),
                new SqlParameter("Name", apartment.Name),
                new SqlParameter("NameEng",apartment.NameEng),
                new SqlParameter("Price", apartment.Price),
                new SqlParameter("MaxAdults", apartment.MaxAdults),
                new SqlParameter("MaxChildren", apartment.MaxChildren),
                new SqlParameter("TotalRooms", apartment.TotalRooms),
                new SqlParameter("BeachDistance", apartment.BeachDistance),
                new SqlParameter("IDApartment", SqlDbType.Int)
            };

            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, CommandType.StoredProcedure, nameof(CreateApartment), parameters);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public int CreateApartmentOwner(ApartmentOwner apartmentOwner)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name",apartmentOwner.Name),
                new SqlParameter("@IDApartmentOwner",SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateApartmentOwner), parameters);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public int CreateApartmentPicture(ApartmentPicture apartmentPicture)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ApartmentId",apartmentPicture.Apartment.Id),
                new SqlParameter("@Path",apartmentPicture.Path),
                new SqlParameter("@Name", apartmentPicture.Name),
                new SqlParameter("@IsRepresentative", apartmentPicture.IsRepresentative.Value),
                new SqlParameter("@IDApartmentPicture", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, CommandType.StoredProcedure, nameof(CreateApartmentPicture), parameters);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public void CreateApartmentReservation(ApartmentReservation res)
        {

            if (res.User == null)
            {
                SqlParameter[] parameters =
            {
                new SqlParameter("@ApartmentId",res.Apartment.Id),
                new SqlParameter("@Details",res.Details),
                new SqlParameter("@UserId", DBNull.Value),
                new SqlParameter("@UserName",  res.UserName),
                new SqlParameter("@UserEmail",  res.Email),
                new SqlParameter("@UserPhone",  res.PhoneNumber),
                new SqlParameter("@UserAddress",  res.Address),
                new SqlParameter("@IDApartmentReservation", SqlDbType.Int)
            };
                parameters[parameters.Length - 1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(CS, nameof(CreateApartmentReservation), parameters);
            }
            else
            {
                SqlParameter[] parameters =
            {
                new SqlParameter("@ApartmentId",res.Apartment.Id),
                new SqlParameter("@Details",res.Details),
                new SqlParameter("@UserId", res.User.Id),
                new SqlParameter("@UserName", res.UserName),
                new SqlParameter("@UserEmail",  res.Email),
                new SqlParameter("@UserPhone",  res.PhoneNumber),
                new SqlParameter("@UserAddress",  res.Address),
                new SqlParameter("@IDApartmentReservation", SqlDbType.Int)
            };
                parameters[parameters.Length - 1].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(CS, nameof(CreateApartmentReservation), parameters);
            }


        }
        public int CreateCity(City city)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name",city.Name),
                new SqlParameter("@CityId", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateCity), parameters);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public int CreateTag(Tag tag)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@TypeId", tag.Type.Id),
                new SqlParameter("@Name",tag.Name),
                new SqlParameter("@NameEng",tag.NameEng),
                new SqlParameter("@IDTag", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateTag), parameters);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public int CreateTagType(TagType tagType)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name",tagType.Name),
                new SqlParameter("@NameEng",tagType.NameEng),
                new SqlParameter("@IDTagType", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateTagType), parameters);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public int CreateUser(User user)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email",user.Email),
                new SqlParameter("@PasswordHash",user.PasswordHash),
                new SqlParameter("@PhoneNumber",user.PhoneNumber),
                new SqlParameter("@Username",user.Username),
                new SqlParameter("@Address",user.Address),
                new SqlParameter("@UserId", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateUser), parameters);
            return int.Parse(parameters[parameters.Length - 1].ToString());
        }
        public int CreateTaggedApartment(TaggedApartment taggedApartment)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ApartmentId",taggedApartment.ApartmentId),
                new SqlParameter("@TagId",taggedApartment.TagId),
                new SqlParameter("@IDTaggedApartment", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateTaggedApartment), taggedApartment.ApartmentId, taggedApartment.TagId, taggedApartment.Id);
            return (int)parameters[parameters.Length - 1].Value;
        }
        public void CreateApartmentReview(ApartmentReview apartmentReview)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ApartmentId",apartmentReview.Apartment.Id),
                new SqlParameter("@UserId", apartmentReview.User.Id),
                new SqlParameter("@Details", apartmentReview.Details),
                new SqlParameter("@Stars", apartmentReview.Stars),
                new SqlParameter("@ApartmentReviewId", SqlDbType.Int)
            };
            parameters[parameters.Length - 1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CS, nameof(CreateApartmentReview), parameters);
        }

        /*   Delete   */

        public void DeleteApartment(int id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartment), id);
        }
        public void DeleteApartmentOwner(int id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartmentOwner), id);
        }
        public void DeleteApartmentPicture(int id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartmentPicture), id);
        }
        public void DeleteApartmentReservation(int id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartmentReservation), id);
        }
        public void DeleteTag(int id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteTag), id);
        }
        public void DeleteUser(int id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteUser), id);
        }
        public void DeleteTaggedApartment(int apartmentId, int tagId)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteTaggedApartment), apartmentId, tagId);
        }
        public void DeleteApartmentReview(int Id)
        {
            SqlHelper.ExecuteNonQuery(CS, nameof(DeleteApartmentReview), Id);
        }

        /*   Select   */

        public Apartment SelectApartment(int id)
        {
            var tblAapartment = SqlHelper.ExecuteDataset(CS, nameof(SelectApartment), id).Tables[0];
            DataRow row = tblAapartment.Rows[0];
            return new Apartment
            {
                Id = (int)row[nameof(Apartment.Id)],
                CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString()),
                DeletedAt = Convert.IsDBNull(row[nameof(Apartment.DeletedAt)]) ? DateTime.MinValue : DateTime.Parse(row[nameof(Apartment.DeletedAt)].ToString()),
                ApartmentOwner = SelectApartmentOwner((int)row["OwnerId"]),
                ApartmentStatus = SelectApartmentStatus((int)row["StatusId"]),
                City = SelectCity((int)row["CityId"]),
                Address = row[nameof(Apartment.Address)].ToString(),
                Name = row[nameof(Apartment.Name)].ToString(),
                NameEng = row[nameof(Apartment.NameEng)].ToString(),
                Price = double.Parse(row[nameof(Apartment.Price)].ToString()),
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                BeachDistance = row[nameof(Apartment.BeachDistance)] != DBNull.Value ? (int)row[nameof(Apartment.BeachDistance)] : 0
            };
        }
        public ApartmentOwner SelectApartmentOwner(int id)
        {
            var tblApartmentOwner = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentOwner), id).Tables[0];
            DataRow row = tblApartmentOwner.Rows[0];
            return new ApartmentOwner
            {
                Id = id,
                CreatedAt = DateTime.Parse(row[nameof(ApartmentOwner.CreatedAt)].ToString()),
                Name = row[nameof(ApartmentOwner.Name)].ToString(),
            };
        }
        public IList<ApartmentOwner> SelectApartmentOwners()
        {
            IList<ApartmentOwner> apartmentOwners = new List<ApartmentOwner>();
            var tblApartmentOwners = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentOwners)).Tables[0];
            foreach (DataRow row in tblApartmentOwners.Rows)
            {
                apartmentOwners.Add(new ApartmentOwner
                {
                    Id = (int)row[nameof(ApartmentOwner.Id)],
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentOwner.CreatedAt)].ToString()),
                    Name = row[nameof(ApartmentOwner.Name)].ToString(),
                });
            }
            return apartmentOwners;
        }
        public IList<ApartmentPicture> SelectApartmentPictures(int apartmentId)
        {
            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();
            var tblApartmentPictures = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentPictures), apartmentId).Tables[0];
            foreach (DataRow row in tblApartmentPictures.Rows)
            {
                apartmentPictures.Add(new ApartmentPicture
                {
                    Id = (int)row[nameof(ApartmentPicture.Id)],
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentPicture.CreatedAt)].ToString()),
                    DeletedAt = Convert.IsDBNull(row[nameof(ApartmentPicture.DeletedAt)]) ? DateTime.MinValue : DateTime.Parse(row[nameof(ApartmentPicture.DeletedAt)].ToString()),
                    Apartment = SelectApartment(apartmentId),
                    Path = row[nameof(ApartmentPicture.Path)].ToString(),
                    Name = row[nameof(ApartmentPicture.Name)].ToString(),
                    IsRepresentative = bool.Parse(row[nameof(ApartmentPicture.IsRepresentative)].ToString()),
                });
            }
            return apartmentPictures;
        }
        public ApartmentReservation SelectApartmentReservation(int apartmentReservationId)
        {
            var tblApartmentReservations = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentReservation), apartmentReservationId).Tables[0];
            DataRow row = tblApartmentReservations.Rows[0];
            return new ApartmentReservation
            {
                Apartment = SelectApartment((int)row["ApartmentId"]),
                Details = row[nameof(ApartmentReservation.Details)].ToString(),
                User = SelectUser((int)row["UserId"])
            };
        }
        public IList<ApartmentReservation> SelectApartmentReservations()
        {
            IList<ApartmentReservation> apartmentReservations = new List<ApartmentReservation>();
            var tblApartmentReservations = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentReservations)).Tables[0];
            foreach (DataRow row in tblApartmentReservations.Rows)
            {
                apartmentReservations.Add(
                    new ApartmentReservation
                    {
                        Id = (int)row[nameof(ApartmentReservation.Id)],
                        CreatedAt = DateTime.Parse(row[nameof(ApartmentReservation.CreatedAt)].ToString()),
                        Apartment = SelectApartment((int)row["ApartmentId"]),
                        Details = row[nameof(ApartmentReservation.Details)].ToString(),
                        User = SelectUser((int)row["UserId"])
                    }
                );
            }
            return apartmentReservations;
        }
        public IList<Apartment> SelectApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();
            var tblApartments = SqlHelper.ExecuteDataset(CS, nameof(SelectApartments)).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                Apartment apartment = new Apartment
                {
                    Id = (int)row[nameof(Apartment.Id)],
                    CreatedAt = DateTime.Parse(row[nameof(Apartment.CreatedAt)].ToString()),
                    ApartmentOwner = SelectApartmentOwner((int)row["OwnerId"]),
                    ApartmentStatus = SelectApartmentStatus((int)row["StatusId"]),
                    City = SelectCity((int)row["CityId"]),
                    Address = row[nameof(Apartment.Address)].ToString(),
                    Name = row[nameof(Apartment.Name)].ToString(),
                    NameEng = row[nameof(Apartment.NameEng)].ToString(),
                    Price = double.Parse(row[nameof(Apartment.Price)].ToString()),
                    MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                    MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                    TotalRooms = (int)row[nameof(Apartment.TotalRooms)]
                };
                if (row[nameof(Apartment.BeachDistance)] != DBNull.Value)
                {
                    apartment.BeachDistance = int.Parse(row[nameof(Apartment.BeachDistance)].ToString());
                }

                apartments.Add(apartment);
            }
            return apartments;
        }
        public IList<ApartmentStatus> SelectApartmentStatuses()
        {
            IList<ApartmentStatus> statuses = new List<ApartmentStatus>();
            var tblStatuses = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentStatuses)).Tables[0];
            foreach (DataRow row in tblStatuses.Rows)
            {
                statuses.Add(new ApartmentStatus()
                {
                    Id = (int)row[nameof(ApartmentStatus.Id)],
                    Name = row[nameof(ApartmentStatus.Name)].ToString(),
                    NameEng = row[nameof(ApartmentStatus.NameEng)].ToString(),
                });
            }
            return statuses;
        }
        public ApartmentStatus SelectApartmentStatus(int statusId)
        {
            var tblStatuses = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentStatus), statusId).Tables[0];
            DataRow row = tblStatuses.Rows[0];
            return new ApartmentStatus()
            {
                Id = statusId,
                Name = row[nameof(ApartmentStatus.Name)].ToString(),
                NameEng = row[nameof(ApartmentStatus.NameEng)].ToString(),
            };
        }
        public IList<City> SelectCities()
        {
            IList<City> cities = new List<City>();
            var tblCities = SqlHelper.ExecuteDataset(CS, nameof(SelectCities)).Tables[0];
            foreach (DataRow row in tblCities.Rows)
            {
                cities.Add(new City
                {
                    Id = (int)row[nameof(City.Id)],
                    Name = SelectCity((int)row[nameof(City.Id)]).Name,
                });
            }
            return cities;
        }
        public City SelectCity(int cityId)
        {
            var tblCity = SqlHelper.ExecuteDataset(CS, nameof(SelectCity), cityId).Tables[0];
            DataRow row = tblCity.Rows[0];
            return new City
            {
                Id = cityId,
                Name = row[nameof(City.Name)].ToString()
            };
        }
        public Tag SelectTag(int tagId)
        {
            var tblTag = SqlHelper.ExecuteDataset(CS, nameof(SelectTag), tagId).Tables[0];
            DataRow row = tblTag.Rows[0];
            return new Tag()
            {
                Id = tagId,
                Name = row[nameof(Tag.Name)].ToString(),
                NameEng = row[nameof(Tag.NameEng)].ToString()
            };
        }
        public IList<Tag> SelectTags()
        {
            IList<Tag> tags = new List<Tag>();
            var tblTags = SqlHelper.ExecuteDataset(CS, nameof(SelectTags)).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        CreatedAt = DateTime.Parse(row[nameof(Tag.CreatedAt)].ToString()),
                        Type = new TagType
                        {
                            Id = (int)row["TypeId"],
                            Name = SelectTagType((int)row["TypeId"]).Name,
                            NameEng = SelectTagType((int)row["TypeId"]).NameEng
                        },
                        Name = row[nameof(Tag.Name)].ToString(),
                        NameEng = row[nameof(Tag.NameEng)].ToString(),
                        Usage = (int)row[nameof(Tag.Usage)]
                    });
            }
            return tags;
        }
        private TagType SelectTagType(int id)
        {
            var tblTagType = SqlHelper.ExecuteDataset(CS, nameof(SelectTagType), id).Tables[0];
            DataRow row = tblTagType.Rows[0];
            return new TagType()
            {
                Id = id,
                Name = row[nameof(TagType.Name)].ToString(),
                NameEng = row[nameof(TagType.NameEng)].ToString()
            };
        }
        public IList<TagType> SelectTagTypes()
        {
            IList<TagType> tagTypes = new List<TagType>();
            var tblTagTypes = SqlHelper.ExecuteDataset(CS, nameof(SelectTagTypes)).Tables[0];
            foreach (DataRow row in tblTagTypes.Rows)
            {
                tagTypes.Add(
                    new TagType
                    {
                        Name = row[nameof(Tag.Name)].ToString(),
                        NameEng = row[nameof(Tag.NameEng)].ToString()
                    });
            }
            return tagTypes;
        }
        public User SelectUser(int id)
        {
            var tblUser = SqlHelper.ExecuteDataset(CS, nameof(SelectUser), id).Tables[0];
            DataRow row = tblUser.Rows[0];
            return new User
            {
                Id = (int)row[nameof(User.Id)],
                CreatedAt = (row[nameof(User.CreatedAt)].ToString()),
                DeletedAt = (row[nameof(User.DeletedAt)].ToString()),
                Email = row[nameof(User.Email)].ToString(),
                EmailConfirmed = bool.Parse(row[nameof(User.EmailConfirmed)].ToString()),
                PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                SecurityStamp = row[nameof(User.SecurityStamp)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                PhoneNumberConfirmed = bool.Parse(row[nameof(User.PhoneNumberConfirmed)].ToString()),
                LockoutEnabled = bool.Parse(row[nameof(User.LockoutEnabled)].ToString()),
                AccessFailedCount = int.Parse(row[nameof(User.AccessFailedCount)].ToString()),
                Username = row[nameof(User.Username)].ToString(),
                Address = row[nameof(User.Address)].ToString()
            };
        }
        public IList<User> SelectUsers()
        {
            IList<User> users = new List<User>();
            var tblUsers = SqlHelper.ExecuteDataset(CS, nameof(SelectUsers)).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(new User
                {
                    Id = (int)row[nameof(User.Id)],
                    CreatedAt = (row[nameof(User.CreatedAt)].ToString()),
                    DeletedAt = (row[nameof(User.DeletedAt)].ToString()),
                    Email = row[nameof(User.Email)].ToString(),
                    EmailConfirmed = bool.Parse(row[nameof(User.EmailConfirmed)].ToString()),
                    PasswordHash = row[nameof(User.PasswordHash)].ToString(),
                    SecurityStamp = row[nameof(User.SecurityStamp)].ToString(),
                    PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                    PhoneNumberConfirmed = bool.Parse(row[nameof(User.PhoneNumberConfirmed)].ToString()),
                    LockoutEnabled = bool.Parse(row[nameof(User.LockoutEnabled)].ToString()),
                    AccessFailedCount = int.Parse(row[nameof(User.AccessFailedCount)].ToString()),
                    Username = row[nameof(User.Username)].ToString(),
                    Address = row[nameof(User.Address)].ToString()
                });
            }
            return users;
        }
        public IList<Tag> SelectTaggedApartment(int apartmentId)
        {
            IList<TaggedApartment> taggedApartments = new List<TaggedApartment>();
            var tblUsers = SqlHelper.ExecuteDataset(CS, nameof(SelectTaggedApartment), apartmentId).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                taggedApartments.Add(new TaggedApartment
                {
                    Id = (int)row[nameof(User.Id)],
                    TagId = (int)row[nameof(TaggedApartment.TagId)]
                });
            }
            IList<Tag> tags = new List<Tag>();
            taggedApartments.ToList().ForEach(ta => tags.Add(SelectTag(ta.TagId)));
            return tags;
        }
        public IList<ApartmentReview> SelectApartmentReviews(int apartmentId)
        {
            IList<ApartmentReview> apartmentReviews = new List<ApartmentReview>();
            var tblReviews = SqlHelper.ExecuteDataset(CS, nameof(SelectApartmentReviews), apartmentId).Tables[0];
            foreach (DataRow row in tblReviews.Rows)
            {
                apartmentReviews.Add(new ApartmentReview
                {
                    Id = apartmentId,
                    Apartment = SelectApartment((int)row["ApartmentId"]),
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentReview.CreatedAt)].ToString()),
                    Details = row[nameof(ApartmentReview.Details)].ToString(),
                    Stars = (int)row[nameof(ApartmentReview.Stars)],
                    User = SelectUser((int)row["UserId"])
                });
            }
            return apartmentReviews;
        }
        public IList<ApartmentReview> SelectUserReviews(int userId)
        {
            IList<ApartmentReview> userReviews = new List<ApartmentReview>();
            var tblReviews = SqlHelper.ExecuteDataset(CS, nameof(SelectUserReviews), userId).Tables[0];
            foreach (DataRow row in tblReviews.Rows)
            {
                userReviews.Add(new ApartmentReview
                {
                    Id = (int)row[nameof(ApartmentReview.Id)],
                    Apartment = SelectApartment((int)row["ApartmentId"]),
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentReview.CreatedAt)].ToString()),
                    Details = row[nameof(ApartmentReview.Details)].ToString(),
                    Stars = (int)row[nameof(ApartmentReview.Stars)],
                    User = SelectUser(userId)
                });
            }
            return userReviews;
        }
        public IList<ApartmentReservation> SelectUserReservations(int userId)
        {
            IList<ApartmentReservation> userReservations = new List<ApartmentReservation>();
            var tblReservations = SqlHelper.ExecuteDataset(CS, nameof(SelectUserReservations), userId).Tables[0];
            foreach (DataRow row in tblReservations.Rows)
            {
                userReservations.Add(new ApartmentReservation
                {
                    Id = (int)row[nameof(ApartmentReservation.Id)],
                    Apartment = SelectApartment((int)row["ApartmentId"]),
                    CreatedAt = DateTime.Parse(row[nameof(ApartmentReservation.CreatedAt)].ToString()),
                    Details = row[nameof(ApartmentReservation.Details)].ToString(),
                    User = SelectUser(userId)
                });
            };
            return userReservations;
        }

        /*   Update   */

        public void UpdateApartment(Apartment apartment)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OwnerId", apartment.ApartmentOwner.Id),
                new SqlParameter("@StatusId", apartment.ApartmentStatus.Id),
                new SqlParameter("@CityId", apartment.City.Id),
                new SqlParameter("@Address", apartment.Address),
                new SqlParameter("@Name", apartment.Name),
                new SqlParameter("@NameEng", apartment.NameEng),
                new SqlParameter("@Price", apartment.Price),
                new SqlParameter("@MaxAdults", apartment.MaxAdults),
                new SqlParameter("@MaxChildren", apartment.MaxChildren),
                new SqlParameter("@TotalRooms", apartment.TotalRooms),
                new SqlParameter("@BeachDistance", apartment.BeachDistance),
                new SqlParameter("@IDApartment", apartment.Id),

            };
            SqlHelper.ExecuteNonQuery(CS, nameof(UpdateApartment), parameters);
        }
        public void UpdateApartmentReservation(ApartmentReservation apartmentReservation)
        {
            SqlParameter[] parameters =
                        {
                new SqlParameter("@ApartmentId", apartmentReservation.Apartment.Id),
                new SqlParameter("@Details", apartmentReservation.Details),
                new SqlParameter("@UserId",apartmentReservation.User.Id),
                new SqlParameter("@UserName",apartmentReservation.User.Username),
                new SqlParameter("@UserEmail",apartmentReservation.User.Email),
                new SqlParameter("@UserPhone",apartmentReservation.User.PhoneNumber),
                new SqlParameter("@UserAddress",apartmentReservation.User.Address),
                new SqlParameter("@IDApartmentReservation",apartmentReservation.Id)

            };
            SqlHelper.ExecuteNonQuery(CS, nameof(UpdateApartmentReservation), parameters);
        }
        public void UpdateUser(User user)
        {
            SqlParameter[] parameters =
                        {
                new SqlParameter("@UserId", user.Id),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PhoneNumber",user.PhoneNumber),
                new SqlParameter("@Username",user.Username),
                new SqlParameter("@Address",user.Address)
            };
            SqlHelper.ExecuteNonQuery(CS, nameof(UpdateUser), parameters);
        }
        public void UpdateApartmentReview(ApartmentReview apartmentReview)
        {
            SqlParameter[] parameters =
                        {
                new SqlParameter("@ApartmentId", apartmentReview.Apartment.Id),
                new SqlParameter("@UserId", apartmentReview.User.Id),
                new SqlParameter("@Details",apartmentReview.Details),
                new SqlParameter("@Stars",apartmentReview.Stars),
                new SqlParameter("@ApartmentReviewId",apartmentReview.Id)
            };
            SqlHelper.ExecuteNonQuery(CS, nameof(UpdateApartmentReview), parameters);
        }
        public void UpdateApartmentPicture(ApartmentPicture apartmentPicture)
        {
            SqlParameter[] parameters =
                        {
                new SqlParameter("@IDApartmentPicture", apartmentPicture.Id),
                new SqlParameter("@Name",apartmentPicture.Name),
                new SqlParameter("@IsRepresentative",apartmentPicture.IsRepresentative),
            };
            SqlHelper.ExecuteNonQuery(CS, nameof(UpdateApartmentPicture), parameters);
        }

    }
}
