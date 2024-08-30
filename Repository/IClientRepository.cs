using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClientsAsync();

        Task<Client?> GetClientByIdAsync(int id);

        Task<Client?> AddClientAsync(Client Client);

        Task<Client?> DeleteClientAsync(int id);

        Task<Client?> UpdateClientAsync(int id, Client Client);

        Task<Client?> UpdateClientPatchAsync(int id, JsonPatchDocument Client);
    }
}