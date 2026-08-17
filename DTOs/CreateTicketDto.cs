using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Api.DTOs;

public class CreateTicketDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(5)]
    [MaxLength(5000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Priority { get; set; } = "Medium";
}