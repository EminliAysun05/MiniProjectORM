using ORMMiniProject.Models.Common;
using ORMMiniProject.Models.Enums;

namespace ORMMiniProject.Models;

public class Order:BaseEntity
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    //enum atacaqsan bura Status:
    //OrderStatus Enumı tipində
    //olacaq (Bunun haqqında məlumat aşağıdadır)
    //- Sifarişin statusu
    public OrderStatusEnum OrderStatus { get; set; }

    public ICollection<OrderDetail> Details { get; set; } = null!;
}
