using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyBankApp.Domain.Enum;

namespace MyBankApp.Domain.Interface
{
    public  interface ITransaction
    {
        void InsertTransaction(long _userBankAccountid, TransactionType _transtype, decimal _transAmount, string _desc);
        void ViewTransaction();

    } 
}
