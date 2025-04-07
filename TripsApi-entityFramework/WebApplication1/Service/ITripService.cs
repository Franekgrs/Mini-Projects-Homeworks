using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;

namespace WebApplication1.Service;

public interface ITripService
{
    Task AssignClientToTrip(ClientToAddDto clientToAddDto);
    Task<List<GetTripsDto>> GetTripsAsync();
}