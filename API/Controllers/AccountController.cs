using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
[HttpPost("register")] // account/register
public async  Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
{
    if (await UserExists (registerDto.Username)) return BadRequest("Username is taken");
     using var hmac = new HMACSHA512();

     var user = new AppUser
     {
        Username = registerDto.Username.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        Passwordsalt =hmac.Key
     };

     context.Users.Add(user);
     await context.SaveChangesAsync();
     return new UserDto
     {
        Username = user.Username,
        Token = tokenService.CreateToken(user)
     };
}

[HttpPost("login")]
public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
{
    var user = await context.Users.FirstOrDefaultAsync(x => 
        x.Username == loginDto.Username.ToLower()); 

        if(user == null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.Passwordsalt);

        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if(computeHash[i] != user.PasswordHash[i]) return Unauthorized ("Invalid passowrd");
        }
        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        };
}
private async Task<bool> UserExists (String username)
{
    return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
}

}
