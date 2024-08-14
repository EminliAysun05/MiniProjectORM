using ORMMiniProject.Dtos.UserDtos;
using ORMMiniProject.Exceptions;
using ORMMiniProject.Models;
using ORMMiniProject.Repostories.Implementations;
using ORMMiniProject.Repostories.Interfaces;
using ORMMiniProject.Services.Interfaces;

namespace ORMMiniProject.Services.Implementations;

public class UserService : IUserService
{


    private readonly IUserReposity _repository;


    public UserService()
    {
        _repository = new UserReposity();
    }

    public async Task<List<OrderDtoForExcel>> GetAllOrdersAsync(int userId)
    {
        var user = await _repository.GetSingleAsync(x => x.Id == userId, "Orders");

        if (user is null)
            throw new NotFoundException("User is not found");

        List<OrderDtoForExcel> dtos = new();

        foreach (var order in user.Orders)
        {
            OrderDtoForExcel dto = new()
            {

                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate
            };

            dtos.Add(dto);
        }

        return dtos;

    }

    public async Task<User> LoginUserAsync(LoginUserDto loginUser)
    {
        var user = await _repository.GetSingleAsync(x => x.Email.ToLower() == loginUser.Email.ToLower(), "Orders");

        if (user is null)
            throw new InvalidLoginException("User is not true");

        if (user.Password != loginUser.Password)
            throw new InvalidLoginException("Login is not valid");

        return user;


    }

    public async Task RegisterUserAsync(RegisterUserDto registeredUser)
    {
        if (registeredUser == null) //isvalidregistered

        {
            throw new InvalidUserInformationException("The information of user is not true");
        }

        var isExist = await _repository.IsExistAsync(x => x.Email == registeredUser.Email);

        if (isExist)
            throw new Exception("This user is already exist");



        User user = new()
        {

            FullName = registeredUser.FullName,
            Email = registeredUser.Email,
            Password = registeredUser.Password,
            Address = registeredUser.Address
        };

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateUser(UpdateUserDto updateUser)
    {
        var user = await _repository.GetSingleAsync(x => x.Id == updateUser.Id);
        if (user is null)
            throw new InvalidLoginException("User is not found");
        user.Id = updateUser.Id;
        user.FullName = updateUser.FullName;
        user.Password = updateUser.Password;
        user.Address = updateUser.Address;
        user.Email = updateUser.Email;

        _repository.Update(user);
        await _repository.SaveChangesAsync();



    }



    //public async void GetByName(string search)
    //{

    //    var results=await _repository.GetFilterAsync(x=>x.FullName.Contains(search));

    //}
}

