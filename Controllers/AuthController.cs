using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Data;
using TicketingSystem.Api.DTOs;
using TicketingSystem.Api.Entities;
using TicketingSystem.Api.Interfaces;


namespace TicketingSystem.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly AppDbContext _context;

    private readonly IJwtService _jwtService;



    public AuthController(
        AppDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }




    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {

        if(string.IsNullOrWhiteSpace(dto.FullName))
        {
            return BadRequest(new
            {
                message = "نام الزامی است"
            });
        }



        if(string.IsNullOrWhiteSpace(dto.Email))
        {
            return BadRequest(new
            {
                message = "ایمیل الزامی است"
            });
        }



        if(string.IsNullOrWhiteSpace(dto.Password) ||
           dto.Password.Length < 6)
        {
            return BadRequest(new
            {
                message = "رمز عبور باید حداقل ۶ کاراکتر باشد"
            });
        }




        var exists = _context.Users
            .FirstOrDefault(x => x.Email == dto.Email);



        if(exists != null)
        {
            return BadRequest(new
            {
                message = "این ایمیل قبلا ثبت شده است"
            });
        }





        var user = new User
        {

            Id = string.IsNullOrEmpty(dto.Id) || dto.Id == "string"
            ? Guid.NewGuid().ToString()
            : dto.Id,



            FullName = dto.FullName,


            Email = dto.Email,


            Phonenumber = dto.Phonenumber,


            Birthdate = dto.Birthdate,


            IsMarried = dto.IsMarried,


            Password = dto.Password

        };



        _context.Users.Add(user);

        _context.SaveChanges();



        return Ok(new
        {
            message = "ثبت نام با موفقیت انجام شد",
            userId = user.Id
        });

    }







    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {


        var user = _context.Users
            .FirstOrDefault(x => x.Email == dto.Email);




        if(user == null || user.Password != dto.Password)
        {

            return Unauthorized(new
            {
                message = "ایمیل یا رمز عبور اشتباه است"
            });

        }




        var token = _jwtService.GenerateToken(user);




        return Ok(new
        {

            message = "ورود موفق بود",


            user = new
            {
                user.Id,
                user.FullName,
                user.Email
            },


            token = token

        });

    }

}