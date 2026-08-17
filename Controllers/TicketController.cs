using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketingSystem.Api.DTOs;
using TicketingSystem.Api.Interfaces;

namespace TicketingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }


    // ========================================
    // Create Ticket
    // ========================================

    [HttpPost]
    public async Task<IActionResult> CreateTicket(
        CreateTicketDto dto)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var ticket = await _ticketService.CreateTicketAsync(
            dto,
            userId
        );

        return Ok(ticket);
    }


    // ========================================
    // Get My Tickets
    // ========================================

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTickets()
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var tickets = await _ticketService.GetMyTicketsAsync(
            userId
        );

        return Ok(tickets);
    }


    // ========================================
    // Get Ticket By Id
    // ========================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicketById(
        int id)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var ticket = await _ticketService.GetTicketByIdAsync(
            id,
            userId
        );

        if (ticket == null)
        {
            return NotFound(
                new
                {
                    message = "Ticket not found."
                }
            );
        }

        return Ok(ticket);
    }


    // ========================================
    // Update Ticket
    // ========================================

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTicket(
        int id,
        UpdateTicketDto dto)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var isAdmin = User.IsInRole("Admin");

        var ticket = await _ticketService.UpdateTicketAsync(
            id,
            dto,
            userId,
            isAdmin
        );

        if (ticket == null)
        {
            return NotFound(
                new
                {
                    message =
                        "Ticket not found or you do not have permission."
                }
            );
        }

        return Ok(ticket);
    }


    // ========================================
    // Delete Ticket
    // ========================================

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(
        int id)
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var isAdmin = User.IsInRole("Admin");

        var deleted = await _ticketService.DeleteTicketAsync(
            id,
            userId,
            isAdmin
        );

        if (!deleted)
        {
            return NotFound(
                new
                {
                    message =
                        "Ticket not found or you do not have permission."
                }
            );
        }

        return Ok(
            new
            {
                message = "Ticket deleted successfully."
            }
        );
    }
}