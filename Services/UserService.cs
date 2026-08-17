using TicketingSystem.Api.Data;
using TicketingSystem.Api.Entities;
using TicketingSystem.Api.Interfaces;

namespace TicketingSystem.Api.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;


    public UserService(AppDbContext context)
    {
        _context = context;
    }



public User Create(User user)
{
    if (string.IsNullOrEmpty(user.Id) || user.Id == "string")
    {
        user.Id = Guid.NewGuid().ToString();
    }


    if (_context.Users.Any(x => x.Id == user.Id))
    {
        user.Id = Guid.NewGuid().ToString();
    }


    _context.Users.Add(user);

    _context.SaveChanges();

    return user;
}

    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }



    public User? GetById(string id)
    {
        return _context.Users
            .FirstOrDefault(x => x.Id == id);
    }



    public bool Update(string id, User user)
    {
        var existingUser = _context.Users
            .FirstOrDefault(x => x.Id == id);


        if (existingUser == null)
            return false;


        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.Phonenumber = user.Phonenumber;
        existingUser.Birthdate = user.Birthdate;
        existingUser.IsMarried = user.IsMarried;


        _context.SaveChanges();


        return true;
    }



    public bool Delete(string id)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.Id == id);


        if (user == null)
            return false;


        _context.Users.Remove(user);

        _context.SaveChanges();


        return true;
    }
}