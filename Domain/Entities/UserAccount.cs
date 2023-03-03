using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBankApp.Domain.Enum;

namespace MyBankApp.Domain.Entities
{
    public class UserAccount
    {
        public int Id { get; set; }
        public int Password { get; set; }
        public long AccountNumber { get; set; }
        public String FullName { get; set; }
        public decimal  AccountBalance { get; set; }
        public string EmailAddress { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }
        public string AccountType { get; set; }

        public DateTime Created { get; set; }

        public UserAccount()
        {

        }
        public UserAccount (int id, int password, long accountNumber, string fullName, long accountBalance, string emailAddress, int totalLogin, bool isLocked, string accountType, DateTime created)
        {
            Id = id;
            Password = password;
            AccountNumber = accountNumber;
            FullName = fullName;
            AccountBalance = accountBalance;
            EmailAddress = emailAddress;
           TotalLogin = totalLogin;
            IsLocked = isLocked;
            AccountType = accountType;
            Created = created;
        }
    }
    
}
