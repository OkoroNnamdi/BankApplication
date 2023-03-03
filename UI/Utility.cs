using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyBankApp.Domain.Entities;
using MyBankApp.Domain.Enum;

namespace MyBankApp.Display
{
    public  class Utility

    {
        AccountType AccountType = new AccountType();
        private static long tranId;
        public static CultureInfo culture = new CultureInfo("IG-NG");


        public static long GetTransactionId()
        {
            return ++tranId;
        }
        // convert the captured input fro the keyboard to asterisk
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;
            string asterisk = "";
            StringBuilder input = new StringBuilder();
            while (true)
            {
                if(isPrompt )
                {
                    Console.WriteLine(prompt);
                }
                isPrompt = false;
                ConsoleKeyInfo inputKey= Console.ReadKey(true );
                if (inputKey.Key==ConsoleKey.Enter)
                {
                    // checking the length of the password
                    if (input.Length ==8)
                    {
                        break;
                    }
                    else
                    {
                        PrintMessage("\n Please Enter 8 digits  ", false );
                        isPrompt =true ;
                        input.Clear();
                        continue;

                    }

                }
                if(inputKey.Key ==ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove (input.Length - 1, 1);
                    
                }else if (inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterisk + "*");
                }
            }
            return input.ToString();
        }
        public static void PrintMessage(string msg, bool sucess = true)
        {
            if (sucess)
            {
                Console.BackgroundColor =ConsoleColor.Green;
            }else
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(msg);
            Console.ForegroundColor =ConsoleColor.White;
             PressEnterToContinue();
        }
        public static string GetUserInput(string prompt)
        {
            Console.Write($"Enter {prompt}");
            return Console.ReadLine();
        }
        public static void PrintDotAnimation(int timer =10)
        {
            
            for (int i = 0; i < timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.Clear();

        }
        public  static void PressEnterToContinue()
        {
            Console.WriteLine("\n\n Press any key to continue...\n");

            Console.ReadLine();
        }
       public static long GenerateRandomAccountNumber()
        {
            long rndAccount=0;
            Random random = new Random();
            for(int i=1; i < 11; i++)
            {
              rndAccount =  random.Next(0,9);
            }
            return rndAccount;
        }
        public static string FormatAmount(decimal amt)
        {
            return string.Format(culture, "{0:C2}", amt);
        }
        public static void collectUserAccountInfo()
        {
            UserAccount accountuser = new UserAccount();
            Console.WriteLine("Welcome to My bank, Please create your Account");
            while (true)
            {
                Utility.PrintMessage("Enter the Name:\n", true);
                string customerName = Console.ReadLine();
                accountuser.FullName = customerName;
                while (!Validator.ValidateUserName(customerName))
                {
                    Console.Clear();

                    Utility.PrintMessage("Enter the Name:\n",false);
                }
                Utility.PrintMessage("Enter Email Address:\n",true );
                // validate the emailaddress
                string emailAddress = Console.ReadLine();
                while (!Validator.IsValidEmail(emailAddress))
                {
                    Console.Clear();
                    Utility.PrintMessage(emailAddress,true);
                }
                accountuser.EmailAddress = emailAddress;
                // Generate the account number
                long accountNumber = GenerateRandomAccountNumber();
                Utility.PrintMessage("Enter you account type:\n");
                // accept the account type
                string accountType = Console.ReadLine();
                
                DateTime createdDate = DateTime.Now;
                accountuser.Created = createdDate;
                //get intital deposit
                Utility.PrintMessage("Enter you Initial deposit", true);
                decimal  iniDeposit =long.Parse(Console.ReadLine());
                accountuser.AccountBalance = iniDeposit;

               

                
               



            }
        }
    }
}
