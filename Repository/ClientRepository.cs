using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            var Clients = await _dbContext.Clients.AsNoTracking().ToListAsync();
            return Clients;
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            var Client = await _dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            return Client;
        }

        public async Task<Client?> AddClientAsync(Client Client)
        {
            try {
                await _dbContext.Clients.AddAsync(Client);
                await _dbContext.SaveChangesAsync();
                return Client;
            } catch (System.Exception) {
                throw new Exception("Falha ao cadstrar usuario");
            }
        }

        public async Task<Client?> DeleteClientAsync(int id)
        {
            var Client = await GetClientByIdAsync(id);
            
            if (Client == null) {
                return Client;
            }

            _dbContext.Clients.Remove(Client);
            await _dbContext.SaveChangesAsync();

            return Client;
        }

        public async Task<Client?> UpdateClientAsync(int id, Client Client)
        {
            var ClientQuery = await GetClientByIdAsync(id);
            
            if (ClientQuery == null) {
                return ClientQuery;
            }

            _dbContext.Entry(ClientQuery).CurrentValues.SetValues(Client);
            await _dbContext.SaveChangesAsync();

            return ClientQuery;
        }

        public async Task<Client?> UpdateClientPatchAsync(int id, JsonPatchDocument ClientDocument)
        {
            var ClientQuery = await GetClientByIdAsync(id);
            
            if (ClientQuery == null) {
                return ClientQuery;
            }
            
            ClientDocument.ApplyTo(ClientQuery);
            await _dbContext.SaveChangesAsync();

            return ClientQuery;
        }
    }
}