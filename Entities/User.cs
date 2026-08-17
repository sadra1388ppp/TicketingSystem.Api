using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketingSystem.Api.Entities;

[Table("users")]
public class User
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [MinLength(10)]
    [MaxLength(12)]
    public string Phonenumber { get; set; } = string.Empty;

    public DateTime? Birthdate { get; set; }

    public bool IsMarried { get; set; } = false;

    [Required]
    public string Role { get; set; } = "User";
}