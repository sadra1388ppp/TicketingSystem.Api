namespace TicketingSystem.Api.DTOs;

public class RegisterDto
{
    public string? Id { get; set; }


    public string FullName { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;


    public string Password { get; set; } = string.Empty;


    public string Phonenumber { get; set; } = string.Empty;


    public DateTime? Birthdate { get; set; }


    public bool IsMarried { get; set; }



}