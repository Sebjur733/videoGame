using Backend.Data;
using Backend.Models;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public string RegUser(string username, string password)
    {
        var user = new User
        {
            Username = username,
            Password = password
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return "User created";
    }
}