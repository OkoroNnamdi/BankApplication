using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankApp.Domain.Interface
{
    public  interface IUserAccountActions
    {
        void CheckBalanc();
        void PlaceDeposit();
        void MakeWithDrawal();

    }
}
