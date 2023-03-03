using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBankApp.Display;

namespace MyBankApp.App
{
    public class Entry
    {
        static void Main(string[] args)
        {
            // calling the method that display the welcome message
           
            BankApp bankApp = new BankApp();
            bankApp.IntializeData();
            bankApp.Run();
            //long accountNumber = Validator.convert<long>("Your Account Number:");
            //Console.WriteLine($" Your Account Number is :{accountNumber}");
            Utility.PressEnterToContinue();


        }
    }
}
