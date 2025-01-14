using System.Text.RegularExpressions;
using UserManagementAPI.Models;

namespace UserManagementAPI.Validators
{
    public static class UserValidator
    {
        public static bool IsValid(User user, out string? errorMessage)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                errorMessage = "Name cannot be empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(user.Email) || !IsValidEmail(user.Email))
            {
                errorMessage = "Invalid email address.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        private static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}