using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repository;

public class TripRepository : ITripRepository
{
    
    private readonly MyLocalDbContext _context;

    public TripRepository(MyLocalDbContext context)
    {
        _context = context;
    }
    public async Task<bool> TripExists(ClientToAddDto clientToAddDto)
    {
        return await _context.Trips.AnyAsync(t => t.IdTrip == clientToAddDto.IdTrip && t.DateFrom > DateTime.Now);
    }

    public async Task<bool> ClientExists(ClientToAddDto clientToAddDto)
    {
        return await _context.Clients.AnyAsync(c => c.Pesel == clientToAddDto.Pesel);
    }

    public async Task<bool> IsClientAssignedToTrip(ClientToAddDto clientToAddDto)
    {
        return await _context.ClientTrips.AnyAsync(ct =>
            ct.IdClient == clientToAddDto.IdClient && ct.IdTrip == clientToAddDto.IdTrip);
    }

    public Task AddClientToTrip(ClientToAddDto clientToAddDto)
    {
        var client = new Client
        {
            IdClient = clientToAddDto.IdClient,
            FirstName = clientToAddDto.FirstName,
            LastName = clientToAddDto.LastName,
            Email = clientToAddDto.Email,
            Telephone = clientToAddDto.Telephone,
            Pesel = clientToAddDto.Pesel
        };

        var clientTrip = new ClientTrip
        {
            IdClientNavigation = client,
            IdTrip = clientToAddDto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientToAddDto.PaymentDate
        };

        _context.Clients.AddAsync(client);
        _context.ClientTrips.AddAsync(clientTrip);
        return _context.SaveChangesAsync();
    }

    public async Task<List<GetTripsDto>> GetTripsAsync()
    {
        return await _context.Trips.Select(trip => new GetTripsDto
        {
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople,
            Countries = trip.IdCountries.Select(country => new CountryDetails
            {
                Name = country.Name
            }).ToList(),
            Clients = trip.ClientTrips.Select(ct => new ClientDetails
            {
                FirstName = ct.IdClientNavigation.FirstName,
                LastName = ct.IdClientNavigation.LastName
            }).ToList()
        }).OrderByDescending(t => t.DateFrom).ToListAsync();
    }
}