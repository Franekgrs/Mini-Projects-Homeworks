using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Repository;

namespace WebApplication1.Service;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task AssignClientToTrip(ClientToAddDto clientToAddDto)
    {
        if (await _tripRepository.ClientExists(clientToAddDto))
        {
            throw new NotFoundException("Nie ma clienta o podanym peselu");
        }

        if (await _tripRepository.TripExists(clientToAddDto))
        {
            throw new NotFoundException("Wycieczka nie istnieje lub juz sie odbyla");
        }

        if (await _tripRepository.IsClientAssignedToTrip(clientToAddDto))
        {
            throw new NotFoundException("Klient o podanym numerze pesel jest juz zapisany na ta wycieczke");
        }

        await _tripRepository.AddClientToTrip(clientToAddDto);
    }

    public Task<List<GetTripsDto>> GetTripsAsync()
    {
        return _tripRepository.GetTripsAsync();
    }
}