using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DAL
{
    public interface IRepo
    {
        /* User */
        IList<User> SelectUsers();
        int CreateUser(User user);
        User SelectUser(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);

        /* Apartment */
        IList<Apartment> SelectApartments();
        int CreateApartment(Apartment apartment);
        void UpdateApartment(Apartment apartment);
        Apartment SelectApartment(int id);
        void DeleteApartment(int id);

        /* Apartment Status */
        IList<ApartmentStatus> SelectApartmentStatuses();
        ApartmentStatus SelectApartmentStatus(int statusId);


        /* Apartment Reservation */
        IList<ApartmentReservation> SelectApartmentReservations();
        void CreateApartmentReservation(ApartmentReservation apartmentReservation);
        ApartmentReservation SelectApartmentReservation(int apartmentReservationId);
        IList<ApartmentReservation> SelectUserReservations(int userId);
        void UpdateApartmentReservation(ApartmentReservation apartmentReservation);
        void DeleteApartmentReservation(int id);

        /* Tag */
        IList<Tag> SelectTags();
        Tag SelectTag(int id);
        int CreateTag(Tag tag);
        void DeleteTag(int id);

        /* TagType */
        IList<TagType> SelectTagTypes();
        int CreateTagType(TagType tagType);

        /* Apartment Picture */
        IList<ApartmentPicture> SelectApartmentPictures(int apartmentId);
        int CreateApartmentPicture(ApartmentPicture apartmentPicture);
        void UpdateApartmentPicture(ApartmentPicture apartmentPicture);
        void DeleteApartmentPicture(int id);

        /* Apartment Owner */
        IList<ApartmentOwner> SelectApartmentOwners();
        int CreateApartmentOwner(ApartmentOwner apartmentOwner);
        ApartmentOwner SelectApartmentOwner(int id);
        void DeleteApartmentOwner(int id);

        /* Apartment Review */
        IList<ApartmentReview> SelectApartmentReviews(int apartmentId);
        IList<ApartmentReview> SelectUserReviews(int userId);

        void CreateApartmentReview(ApartmentReview apartmentReview);
        void DeleteApartmentReview(int Id);
        void UpdateApartmentReview(ApartmentReview apartmentReview);

        /* Tagged Apartment */
        IList<Tag> SelectTaggedApartment(int apartmentId);
        int CreateTaggedApartment(TaggedApartment taggedApartment);
        void DeleteTaggedApartment(int id, int tagId);

        /* City */
        IList<City> SelectCities();
        City SelectCity(int apartmentId);
        int CreateCity(City city);
    }
}
