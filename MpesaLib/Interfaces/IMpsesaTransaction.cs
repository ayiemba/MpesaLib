
using MpesaLib.Models;
using System.Threading.Tasks;

namespace MpesaLib.Interfaces
{
    public interface IMpesaTransaction
    {
        Task<string> BusinessToCustomer(BusinessToCustomer b2c);

        Task<string> LipaNaMpesaOnline(LipaNaMpesaOnline lipa);

        Task<string> BusinessToBusiness(BusinessToBusiness b2b);

        Task<string> CustomerToBusinessRegister(CustomerToBusinessRegister c2bregister);

        Task<string> CustomerToBusinessSimulate(CustomerToBusinessSimulate c2bsimulate);

        Task<string> LipaNaMpesaQuery(LipaNaMpesaQuery query);

        Task<string> Reversal(Reversal reversal);

        Task<string> TransactionStatus(TransactionStatus status);

        Task<string> AccountBalance(AccountBalance balance);

    }
}