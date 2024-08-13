namespace ORMMiniProject.Dtos.UserDtos;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
