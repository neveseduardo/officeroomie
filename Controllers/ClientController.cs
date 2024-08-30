using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/clients")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _clientRepository.GetClientsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            var Client = await _clientRepository.GetClientByIdAsync(id);
            
            if (Client == null) {
                return NotFound();
            }
            
            return Ok(Client);
        }

        [HttpPost]
        public async Task<IActionResult> StoreClient([FromBody] Client Client)
        {
            await _clientRepository.AddClientAsync(Client);
            return CreatedAtAction("GetClient", new { id = Client.id }, Client);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            var Client = await _clientRepository.DeleteClientAsync(id);
            
            if (Client == null) {
                return NotFound();
            }
            
            return Ok(Client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] Client Client)
        {
            if (id != Client.id) {
                return BadRequest();
            }

            var updatedClient = await _clientRepository.UpdateClientAsync(id, Client);
            
            if (updatedClient == null) {
                return NotFound();
            }

            return Ok(updatedClient);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchClient([FromRoute] int id, [FromBody] JsonPatchDocument ClientDocument)
        {
            var updatedClient = await _clientRepository.UpdateClientPatchAsync(id, ClientDocument);
            
            if (updatedClient == null) {
                return NotFound();
            }
            
            return Ok(updatedClient);
        }
    }
}