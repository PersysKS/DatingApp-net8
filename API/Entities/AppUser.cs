using System.Globalization;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required String Username {get;set;}

    public required byte [] PasswordHash { get; set; }

    public required byte [] Passwordsalt { get; set; }
}
