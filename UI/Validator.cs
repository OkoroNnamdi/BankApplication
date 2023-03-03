using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBankApp.Display
{
    public   class Validator
    {
        public static T convert<T>(string prompt)
        {
            // Valid the user input
            bool vaild = false;
            string userInput;
            while (!vaild)
            {
                userInput = Utility.GetUserInput(prompt);
                try
                {
                   var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        return (T)converter.ConvertFromString(userInput);
                    }
                    else
                    {
                        return default;
                    }
                }catch 

                {
                    Utility.PrintMessage("Invalid Input. Try again", false);
                    Console.Clear();
                }
            }
            return default;
        }
        public static  bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z",RegexOptions.IgnoreCase );
        }
        public static   bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            if (input.Length < 6)
            {
                ErrorMessage = "Password should be at least 6 character";
                return false;
            }
            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one lower case letter.";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one upper case letter.";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one numeric value.";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one special case character.";
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ValidateUserName(string userName)
        {
            bool hasDigitorsmallletter = false;
            char c = userName[0];
            if (char.IsDigit(c))
            {
                hasDigitorsmallletter = true;
                Console.WriteLine("Account Name should not start with Digit");
            }else if (char.IsLower(c))
            {
                hasDigitorsmallletter = true;
                Console.WriteLine("First Letter should not be lowercase");
            }

            return hasDigitorsmallletter;
           // Console.WriteLine("Account Name should not start with Digit"); ;

        }

    }
}
