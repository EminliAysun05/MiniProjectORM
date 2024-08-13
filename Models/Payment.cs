using ORMMiniProject.Models.Common;

namespace ORMMiniProject.Models;

public class Payment:BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}
