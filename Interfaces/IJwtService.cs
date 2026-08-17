using TicketingSystem.Api.Entities;

namespace TicketingSystem.Api.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}