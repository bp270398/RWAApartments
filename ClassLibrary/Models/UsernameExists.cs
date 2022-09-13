using ClassLibrary.DAL;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassLibrary.Models
{
    public class UsernameExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            User user = value as User;
            if (user != null)
            {
                IRepo repo = RepoFactory.GetRepo();
                IList<User> existingUsers = repo.SelectUsers();
                if (existingUsers.Any(u => u.Username.Equals(user.Username)))
                {
                    return ValidationResult.Success;
                }
            }
            var errorMessage = FormatErrorMessage(context.DisplayName);
            return new ValidationResult(errorMessage);
        }
    }
}