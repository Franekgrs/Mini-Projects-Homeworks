using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repository;

public class ClientRepository : IClientRepository
{
    
    private readonly MyLocalDbContext _context;

    public ClientRepository(MyLocalDbContext context)
    {
        _context = context;
    }
    
    
    public async Task DeleteClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client!=null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<bool> ClientHasTripsAsync(int id)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == id);
    }


}