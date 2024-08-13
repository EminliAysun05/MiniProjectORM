using ORMMiniProject.Dtos.PaymentDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Models;
using ORMMiniProject.Repostories.Implementations;
using ORMMiniProject.Repostories.Interfaces;
using ORMMiniProject.Services.Interfaces;

namespace ORMMiniProject.Services.Implementations;

public class PaymentService : IPaymentServices
{
    private readonly IPaymentReposity _paymentReposity;
    private readonly IOrderReposity _orderReposity;

    public PaymentService()
    {
        _paymentReposity = new PaymentReposity();
        _orderReposity = new OrderReposity();
    }
    public async Task CreatePayment(MakePaymentDto paymentDto)
    {
        if (paymentDto.Amount < 0)
        {
            throw new InvalidPaymentException("Amount shouldn't be lower than 0");
        }
        var order = await _orderReposity.GetSingleAsync(o => o.Id == paymentDto.OrderId);

        if (order == null)
        {
            throw new NotFoundException("Order is not found");
        }
        var payment = new Payment
        {
            OrderId = paymentDto.OrderId,
            Amount = paymentDto.Amount,
            PaymentDate = DateTime.UtcNow
        };
        await _paymentReposity.AddAsync(payment);
        await _paymentReposity.SaveChangesAsync();

    }

    public async Task<List<GetPaymentsDto>> GetAllPayments()
    {
        var payments = await _paymentReposity.GetAllAsync();
        return payments.Select(p => new GetPaymentsDto
        {
            OrderId = p.OrderId,
            Amount = p.Amount,
            PaymentDate = p.PaymentDate
        }).ToList();
    }
}
