namespace TicketingSystem.Api.Entities;

public class Ticket
{
    public int Id { get; set; }
    

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Open";

    public string Priority { get; set; } = "Medium";

    public string UserId { get; set; } = string.Empty;

    public User User { get; set; } = null!;
}