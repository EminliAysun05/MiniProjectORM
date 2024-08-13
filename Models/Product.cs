using ORMMiniProject.Models.Common;
using ORMMiniProject.Repostories.Interfaces.Generic;

namespace ORMMiniProject.Models;

public class Product:BaseEntity,INameable
{
   
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public ICollection<OrderDetail> Details { get; set; } = null!;
}
