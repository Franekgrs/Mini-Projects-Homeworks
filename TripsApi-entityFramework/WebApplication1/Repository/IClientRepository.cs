using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repository;

public interface IClientRepository
{
    Task DeleteClientAsync(int id);
    Task<Client> GetClientByIdAsync(int id);
    Task<bool> ClientHasTripsAsync(int id);
}