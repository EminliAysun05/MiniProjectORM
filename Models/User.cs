using ORMMiniProject.Models.Common;
using ORMMiniProject.Repostories.Interfaces.Generic;

namespace ORMMiniProject.Models;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Address { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = null!;
}
