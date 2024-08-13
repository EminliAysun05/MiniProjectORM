using ORMMiniProject.Dtos.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Services.Interfaces
{
    public interface IPaymentServices
    {
        Task CreatePayment(MakePaymentDto paymentDto);
        Task<List<GetPaymentsDto>> GetAllPayments();
    }
}
