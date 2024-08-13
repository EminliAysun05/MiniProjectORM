using ORMMiniProject.Dtos.UserDtos;
using ORMMiniProject.Models;

namespace ORMMiniProject.Services.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserDto registeredUser);
    Task<User> LoginUserAsync(LoginUserDto loginUser);
    Task UpdateUser(UpdateUserDto updateUser);
    Task <List<OrderDtoForExcel>> GetAllOrdersAsync(int userId);
    //Task ExportUserOrdersToExcelAsync(int userId); //for excel 


}
