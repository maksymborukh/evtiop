using evtiop.BLL.DTO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace evtiop.BLL.Validation
{
    public class CustomerValidation : ValidationRule
    {
        public Customer Customer = new Customer();

        public string type { get; set; }

        public ValidationResult IsValidEmail(object email)
        {
            if (email == null || string.IsNullOrEmpty(email.ToString()))
                return new ValidationResult(false, "Email address is required");
            else if (email.ToString().IndexOf("@") > -1)
                if (email.ToString().IndexOf(".", email.ToString().IndexOf("@")) > email.ToString().IndexOf("@"))
                    return new ValidationResult(true, null);
            return new ValidationResult(false, "Email address must be valid email address format.\n" + "For example 'someone@example.com' ");
        }

        public ValidationResult IsValidPassword(object pass)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");

            if (pass == null || string.IsNullOrEmpty(pass.ToString()))
                return new ValidationResult(false, "Password is required");
            else if (!hasNumber.IsMatch(pass.ToString()))
                return new ValidationResult(false, "Password must contain at least  a number");
            else if (!hasUpperChar.IsMatch(pass.ToString()))
                return new ValidationResult(false, "Password must contain at least one upper case letter");
            else if (pass.ToString().Length < 8)
                return new ValidationResult(false, "Password must be at least 8 characters long");
            else
                return new ValidationResult(true, null);
        }

        public ValidationResult IsValidName(object name)
        {
            Regex hasOnlyLetters = new Regex(@"^[a-zA-Z]+$");
            Regex hasMinimumChars = new Regex(@".{2,}");

            if (name == null || string.IsNullOrEmpty(name.ToString()))
                return new ValidationResult(false, "Name is required");
            else if (!hasOnlyLetters.IsMatch(name.ToString()))
                return new ValidationResult(false, "Name must contain only letters");
            else if (!hasMinimumChars.IsMatch(name.ToString()))
                return new ValidationResult(false, "Name must be at least 2 characters long");
            else
                return new ValidationResult(true, null);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (type == "Email")
                return IsValidEmail(value);
            else if (type == "Name")
                return IsValidName(value);
            else if (type == "Password")
                return IsValidPassword(value);

            return ValidationResult.ValidResult;
        }
    }
}
