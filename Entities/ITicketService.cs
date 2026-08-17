using TicketingSystem.Api.DTOs;
using TicketingSystem.Api.Entities;

namespace TicketingSystem.Api.Interfaces;

public interface ITicketService
{
    Task<Ticket> CreateTicketAsync(CreateTicketDto dto, string userId);

    Task<List<Ticket>> GetMyTicketsAsync(string userId);

    Task<Ticket?> GetTicketByIdAsync(int id, string userId);

    Task<Ticket?> UpdateTicketAsync(
        int id,
        UpdateTicketDto dto,
        string userId,
        bool isAdmin);

    Task<bool> DeleteTicketAsync(
        int id,
        string userId,
        bool isAdmin);
}