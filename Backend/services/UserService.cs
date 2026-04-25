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

    public string LogUser(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        return "User not found";

        if (user.Password != password)
            return "Wrong password";

    return "Login success";
    }
}