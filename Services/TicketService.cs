using Microsoft.EntityFrameworkCore;
using TicketingSystem.Api.Data;
using TicketingSystem.Api.DTOs;
using TicketingSystem.Api.Entities;
using TicketingSystem.Api.Interfaces;

namespace TicketingSystem.Api.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> CreateTicketAsync(
        CreateTicketDto dto,
        string userId)
    {
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            Status = "Open",
            UserId = userId
        };

        _context.Tickets.Add(ticket);

        await _context.SaveChangesAsync();

        return ticket;
    }

    public async Task<List<Ticket>> GetMyTicketsAsync(
        string userId)
    {
        return await _context.Tickets
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(
        int id,
        string userId)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(t =>
                t.Id == id &&
                t.UserId == userId);
    }

    public async Task<Ticket?> UpdateTicketAsync(
        int id,
        UpdateTicketDto dto,
        string userId,
        bool isAdmin)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return null;
        }

        if (!isAdmin && ticket.UserId != userId)
        {
            return null;
        }

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Priority = dto.Priority;
        ticket.Status = dto.Status;

        await _context.SaveChangesAsync();

        return ticket;
    }

    public async Task<bool> DeleteTicketAsync(
        int id,
        string userId,
        bool isAdmin)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            return false;
        }

        if (!isAdmin && ticket.UserId != userId)
        {
            return false;
        }

        _context.Tickets.Remove(ticket);

        await _context.SaveChangesAsync();

        return true;
    }
}