using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTables;
using MyBankApp.Display;
using MyBankApp.Domain.Entities;
using MyBankApp.Domain.Enum;
using MyBankApp.Domain.Interface;
using MyBankApp.Interface;

namespace MyBankApp
{
    public class BankApp:IUserLogin ,IUserAccountActions,ITransaction
    {
        private List<UserAccount> userAccounList;
        private UserAccount selectedUserAccount;
        private List<Transaction> _ListofTransactiions;
        private const decimal  minimumKeptAmount = 500;
       // private readonly  AppDisplay appDisplay;
         
        public void Run()
        {
            AppDisplay.Welcome();
            ChecKAccountNumberAndPassword();
            AppDisplay.WelcomeCustomer(selectedUserAccount.FullName );
            while (true) { 
            AppDisplay.DisplayAppMenu();
            processMenuOption();
            }
        }
        public void IntializeData( )
        {
            userAccounList = new List<UserAccount>()

            {

                new UserAccount{Id=1,  AccountBalance=9000, AccountNumber= 47899098,  FullName="Okoro Nnamdi",Password =12345678, IsLocked =false},
                new UserAccount{Id =2, AccountBalance = 8000, AccountNumber=57899, FullName ="Okeh Emmanuel", IsLocked =false},
                new UserAccount{Id =3, AccountBalance = 10000, AccountNumber=67899098, FullName ="Emmanual Isreal", IsLocked =false},

            };
            _ListofTransactiions = new List<Transaction>();

        }
    private void processMenuOption()
        {
           switch (Validator.convert<int>("an option:"))
            {
                case(int)AppMenu.CheckBalance:
                    CheckBalanc();
                    break;
                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;
                case (int)AppMenu.MakeWithdrawal:
                   MakeWithDrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = AppDisplay.InternalTransferForm();
                    processInternalTransfer(internalTransfer);
                    break;
                case (int)AppMenu.ViewTransaction:
                    ViewTransaction();
                    break;
                case (int)AppMenu.Logout :
                    AppDisplay.LoginProgress();
                    Utility.PrintMessage("Your have sucessfully Logged out, Thank for banking with us");
                    Run();
                    break;
                    default:
                    Utility.PrintMessage("Invalid option",false);
                    break;
            }
        }
        public void ChecKAccountNumberAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccount accountInput = AppDisplay.UserLoginForm();
                AppDisplay.LoginProgress();
                foreach (UserAccount account in userAccounList)
                {
                    selectedUserAccount = account;
                    
                    if (accountInput.AccountNumber.Equals(selectedUserAccount.AccountNumber))
                    {
                        selectedUserAccount.TotalLogin++;
                        if (accountInput.Password .Equals (selectedUserAccount.Password))
                        {
                            selectedUserAccount = account;
                            if(selectedUserAccount.IsLocked || selectedUserAccount.TotalLogin > 3)
                            {
                                // print the user is logged out
                                AppDisplay.PrintLockScreen();
                            }
                            else
                            {
                                selectedUserAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }
                    if (isCorrectLogin == false)
                    {
                        Utility.PrintMessage("\nInvalid Account or Password", false);
                        selectedUserAccount.IsLocked = selectedUserAccount.TotalLogin == 3;
                        if (selectedUserAccount.IsLocked)
                        {
                            AppDisplay.PrintLockScreen();
                        }
                    }
                    Console.Clear();

                }



            }
            
            
        }

        public void CheckBalanc()
        {
            Utility.PrintMessage($"your account balance{Utility.FormatAmount (selectedUserAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("Enter the amount in the multiple of 500");
            var transaction_amt = Validator.convert<int>($"amoun{AppDisplay.cur}");
            // simulatate counting of notes
            Utility.PrintDotAnimation();
            Console.WriteLine("");
            // gard clause
            if (transaction_amt <=0)
               
            {
                Utility.PrintMessage("The transaction amount should be greater than zero. Try again",false);
                return;
            }
            if (transaction_amt %500 != 0)
            {
                Utility.PrintMessage("Enter amount in the multiple of 500 or 1000. Try again",false);
                return ;
            }
            if (PreviewBankNotesCount (transaction_amt) == false)
            {
                Utility.PrintMessage("Your have cnacelled your action");
                return ;
            }
            // bind the transaction detail to the transaction object
            InsertTransaction(selectedUserAccount.Id, TransactionType.Deposit, transaction_amt, "");

            //update the account balance
            selectedUserAccount.AccountBalance =+ transaction_amt;
            // print sucessful message
            Utility.PrintMessage($"Your deposit of {Utility.FormatAmount (transaction_amt )} was sucessful");

        }

        public void MakeWithDrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppDisplay.SelectedAmount();
            if (selectedAmount == -1)
            {
                MakeWithDrawal();
                return;

            }else if (selectedAmount != 0)
            {
                transaction_amt = selectedAmount;
            }
            else
            {
                transaction_amt = Validator.convert<int>($"amount{AppDisplay.cur}");

            }
            // input validation
            if (transaction_amt <= 0)
            {
                Console.WriteLine("Amount should be greater than zero");
                return;
            }
            if(transaction_amt % 500 != 0)
            {
                Console.WriteLine("Amount must be in the multiples of 500");
                return;
            }
            // business logic 
            if(transaction_amt > selectedUserAccount.AccountBalance)
            {
                Utility.PrintMessage($"Transaction failed. Your balance is low to withdraw " +
                    $"{Utility.FormatAmount(transaction_amt)}",false );

                return;
            }
            if((selectedUserAccount.AccountBalance -transaction_amt)<minimumKeptAmount)
            {
                Utility.PrintMessage($"Your account is to have {Utility.FormatAmount(minimumKeptAmount )}",false );
                return;
            }
            // bind transaction details to transaction object
            InsertTransaction(selectedUserAccount.Id, TransactionType.WithDrawal, -transaction_amt,"");
            // update account balance
            selectedUserAccount.AccountBalance -= transaction_amt;
            // print sucessful message
            Utility.PrintMessage($"You have sucessfully withdraw " +
                $"{Utility.FormatAmount (transaction_amt )}",true);

        }
        private bool PreviewBankNotesCount(int amount)
        {
            int thousandnoteAmount = amount / 1000;
            int hundrednoteAmount= (amount % 1000)/500;
            Console.WriteLine("\n Summary");
            Console.WriteLine("______");
            Console.WriteLine($"{AppDisplay.cur}1000 x {thousandnoteAmount }={1000*thousandnoteAmount }");
            Console.WriteLine($"{AppDisplay.cur }500 X {hundrednoteAmount }={500*hundrednoteAmount }");
            Console.WriteLine($"Total Amoun: {Utility.FormatAmount (amount)}\n\n");
            int opt = Validator.convert<int>("1 to Confirm:\n");
            return opt.Equals(1);

        }
        private void processInternalTransfer(InternalTransfer internalTransfer)
        {
            if (internalTransfer.TransferAmount <= 0)
            {
                Utility.PrintMessage($"Amount should be more than zero. Try again",false);
                return;
            }
            // check the sender's account balance.
            if (internalTransfer.TransferAmount > selectedUserAccount.AccountBalance)
            {
                Utility.PrintMessage($"Transaction failed, You donot have sufficient amount." +
                    $"to transfer {Utility.FormatAmount(internalTransfer.TransferAmount )} Try again ", false);
                return;
            }
            // checking the minimum kept amount
            if((selectedUserAccount.AccountBalance -internalTransfer .TransferAmount) < minimumKeptAmount)
            {
                Utility.PrintMessage($"Transacttin failed. Your account need to have minimum {Utility.FormatAmount (minimumKeptAmount )}",false );
                return;
                
            }
            // checking the validity of receiver's account number
            var selectedBankAccountReciever = (from userAcc in userAccounList
                                             where userAcc.AccountNumber==internalTransfer.RecipientBankAccountNumber 
                                             select userAcc).FirstOrDefault();
            if(selectedBankAccountReciever== null)
            {
                Utility.PrintMessage("Transfer failes. Receiver's bank account number is invalid", false);
                return;
            }
            // check receiver's full name
            if (selectedBankAccountReciever.FullName != internalTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage("transfer fail, Receipient account name invalid", false);
                return;

            }
            // Add transaction to transaction record- sender
            InsertTransaction(selectedUserAccount.Id, TransactionType.Transfer, -internalTransfer.TransferAmount,
                $"Transfer to {selectedBankAccountReciever.AccountNumber}({selectedBankAccountReciever.FullName})");
           
            // update sender' account balance
            selectedUserAccount.AccountBalance -= internalTransfer.TransferAmount;
            // add transaction record for reciever
            InsertTransaction(selectedBankAccountReciever.Id,TransactionType.Transfer,internalTransfer.TransferAmount, 
                $"Transfer from {selectedUserAccount.AccountNumber}({selectedUserAccount.FullName})");
            // update receiver's account balance

            selectedBankAccountReciever.AccountBalance =+ internalTransfer.TransferAmount;
            // print sucessful message

            Utility.PrintMessage($"You have transfered {Utility.FormatAmount(internalTransfer.TransferAmount )}" +
                $" to {internalTransfer.RecipientBankAccountName  }",true);

           
        }
        public void InsertTransaction(long _userBankAccountid, TransactionType _transtype, decimal _transAmount, string _desc)
        {
            var transaction = new Transaction()
            {
                TransactionId =Utility.GetTransactionId(),
                UserBankAccountId = _userBankAccountid,
                TransactionDate = DateTime.Now,
                TransactionAmount = _transAmount,

                TransactionType = _transtype,
                Description = _desc

            };
            //add transaction object
            _ListofTransactiions.Add(transaction);
        }

        public void ViewTransaction()
        {
            var filteredListOfTransaction = _ListofTransactiions.
                Where(t => t.UserBankAccountId ==selectedUserAccount.Id).ToList();
            // check if there transaction
            if (filteredListOfTransaction.Count  <= 0) 
            {
                Console.WriteLine("You have no transaction yet");
            }
            else
            {
                var table = new ConsoleTable("Id","Transaction Date", "Type","Descriptions","Amount"+AppDisplay.cur );
                foreach (var transaction in filteredListOfTransaction)
                {
                    table.AddRow(transaction.TransactionId, transaction.TransactionDate,
                        transaction.TransactionType, transaction.Description, transaction.TransactionAmount);
                }
                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMessage($"You have {filteredListOfTransaction.Count }transaction(s)",true);
            }
        }
    }
}
