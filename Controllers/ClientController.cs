using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;
using WebApi.ModelViewModels;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Authorize]
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
        public async Task<IEnumerable<ClientViewModel>> GetClients()
        {
            var clients = await _clientRepository.GetClientsAsync();

            var viewModel = clients.Select(u => new ClientViewModel {
                id = u.id,
                name = u.name,
                email = u.email,
            });

            return viewModel;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            
            if (client == null) {
                return NotFound();
            }

            var viewModel = new ClientViewModel {
                id = client.id,
                name = client.name,
                email = client.email,
            };
            
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StoreClient([FromBody] ClientDto dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var client = new Client {
                name = dto.name,
                email = dto.email,
            };
            
            await _clientRepository.AddClientAsync(client);

            return CreatedAtAction("GetClient", new { id = client.id }, client);
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
        public async Task<IActionResult> UpdateClient([FromRoute] int id, [FromBody] ClientDto dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var client = new Client {
                name = dto.name,
                email = dto.email,
            };

            var updatedClient = await _clientRepository.UpdateClientAsync(id, client);
            
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