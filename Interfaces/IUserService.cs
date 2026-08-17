using TicketingSystem.Api.Entities;

namespace TicketingSystem.Api.Interfaces;

public interface IUserService
{
    User Create(User user);


    List<User> GetAll();


    User? GetById(string id);


    bool Update(string id, User user);


    bool Delete(string id);
}