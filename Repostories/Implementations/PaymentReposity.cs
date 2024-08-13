using ORMMiniProject.Models;
using ORMMiniProject.Repostories.Implementations.Generic;
using ORMMiniProject.Repostories.Interfaces;

namespace ORMMiniProject.Repostories.Implementations
{
    public class PaymentReposity:Reposity<Payment>,IPaymentReposity
    {
    }
}
