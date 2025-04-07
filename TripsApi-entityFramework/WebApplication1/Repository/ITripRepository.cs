using WebApplication1.DTOs;

namespace WebApplication1.Repository;

public interface ITripRepository
{
    Task<bool> TripExists(ClientToAddDto clientToAddDto);
    Task<bool> ClientExists(ClientToAddDto clientToAddDto); // sprawdzam klienta o peselu
    Task<bool> IsClientAssignedToTrip(ClientToAddDto clientToAddDto);
    Task AddClientToTrip(ClientToAddDto clientToAddDto);

    Task<List<GetTripsDto>> GetTripsAsync();

}