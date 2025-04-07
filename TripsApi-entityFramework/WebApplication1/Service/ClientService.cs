using WebApplication1.Exceptions;
using WebApplication1.Repository;

namespace WebApplication1.Service;

public class ClientService : iClientService
{
    private readonly ClientRepository _ClientRepository;

    public ClientService(ClientRepository clientRepository)
    {
        _ClientRepository = clientRepository;
    }

    public async Task<bool> DeleteCkuebtAsync(int id)
    {
        var client = await _ClientRepository.GetClientByIdAsync(id);
        if (client == null)
        {
            throw new NotFoundException("nie ma klienta o podanym id");
        }

        var hasTrips = await _ClientRepository.ClientHasTripsAsync(id);
        if (hasTrips)
        {
            throw new BadRequestException("Nie mozna usunac klienta przypisanego do wycieczki");
        }

        await _ClientRepository.DeleteClientAsync(id);
        return true;
    }
}