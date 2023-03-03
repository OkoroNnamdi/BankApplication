using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyBankApp.Domain.Entities;

namespace MyBankApp.Display
{
    
    internal   static class AppDisplay
    {
        internal const string cur = "N";
        public static void Welcome()
        {
            // clear the screen
            Console.Clear();
   
            // Give title to the application
            Console.Title = "My Bank Application";
            // set welcome message
            Console.WriteLine("\n\n>>>>>>>>>>Welcome To My Bank Application<<<<<<<<<<<<<<<<<<<<\n\n");

            Utility.PressEnterToContinue ();
           
        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();
            tempUserAccount.AccountNumber = Validator.convert<long>("Your User Account Number: \n");
            tempUserAccount.Password = Convert.ToInt32(Utility.GetSecretInput("Enter your secret code:\n"));
            return tempUserAccount;
        }
        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking AccountNumber and Password...");
            Utility.PrintDotAnimation();
        }
        internal static void PrintLockScreen()
        {
            Console.Clear ();
            Utility.PrintMessage("Your account is locked",true);
            Utility.PressEnterToContinue();
            Environment.Exit (1);

        }
        internal static  void WelcomeCustomer(string FullName)
        {
            Console.WriteLine($"Welcome back {FullName}");
        }
        // display the application menu
        internal  static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("----------My Bank Application Menu--------");
            Console.WriteLine(":                                         :");
            Console.WriteLine("1.Account Balance                         :");
            Console.WriteLine("2.Deposit                                 :");
            Console.WriteLine("3.Withdrawal                              :");
            Console.WriteLine("4.Transfer                                :");
            Console.WriteLine("5.View Transaction                        :");
            Console.WriteLine("6.logout                                  :");

        }
        // Display Login and Signup menu
        internal static void DisplayLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("----------My Bank Application Menu--------");
            Console.WriteLine(":                                        :");
            Console.WriteLine("1.Creat Account:                                :");
            Console.WriteLine("2.Login:                                 :");
            // selecting the value by pressing
            int options = Validator.convert<int>("Option: ");
            switch (options)
            {
                case 1:
                    Utility.PrintMessage("Creating new user account......... ", true);
                    break;
                case 2:
                    // calling the method that display the welcome message

                    BankApp bankApp = new BankApp();
                    bankApp.IntializeData();
                    bankApp.Run();
                    //long accountNumber = Validator.convert<long>("Your Account Number:");
                    //Console.WriteLine($" Your Account Number is :{accountNumber}");
                    Utility.PressEnterToContinue();
                    break;
                default:
                    Console.WriteLine("Invalid entry.Try again");
                    break;
            }

        }
        public static void LogOutProgress()
        {
            Console.WriteLine("Thank you for using My Bank App");
            Utility.PrintDotAnimation();
            Console.Clear();
        }
        internal static int SelectedAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.{0}500       5.{0}10000",cur );
            Console.WriteLine(":2.{0}1000      6.{0}15000", cur);
            Console.WriteLine(":3.{0}2000      7.{0}20000", cur);
            Console.WriteLine(":4.{0}5000      8.{0}40000", cur);
            Console.WriteLine(":0.other");
            Console.WriteLine();

            int selectedAmount = Validator.convert<int>("option:");
            switch (selectedAmount)
            {
                case 1:
                    return 500;
                    break;
                case 2:
                    return 1000;
                    break;
                case 3:
                    return 2000;
                    break;
                case 4:
                    return 50000;
                    break;
                case 5:
                    return 10000;
                    break;
                case 6:
                    return 15000;
                    break;
                case 7:
                    return 20000;
                    break;
                case 8:
                    return 4000;
                    break;
                case 0:
                    return 0;
                default:
                    Console.WriteLine("Invalid Input, Try again");
                    
                    return -1;


                    
            }
        }
        internal static InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.RecipientBankAccountNumber = Validator.convert<long>("Recipient Bank account number");
            internalTransfer.TransferAmount = Validator.convert<decimal>($"amount{cur }");
            internalTransfer.RecipientBankAccountName = Utility.GetUserInput("Recipient's name:");
            return internalTransfer;
        }
    }
}

