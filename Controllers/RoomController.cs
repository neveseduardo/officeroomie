using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;
using WebApi.ViewModels;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomViewModel>> GetRooms()
        {
            var rooms = await _roomRepository.GetRoomsAsync();
            var viewModel = rooms.Select(u => new RoomViewModel {
                name = u.name,
                description = u.description,
                capacity = u.capacity,
            });
            
            return viewModel;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom([FromRoute] int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            
            if (room == null) {
                return NotFound();
            }

            var viewModel = new RoomViewModel
            {
                name = room.name,
                description = room.description,
                capacity = room.capacity,
            };
            
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StoreRoom([FromBody] RoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = new Room 
            {
                name = dto.name,
                description = dto.description,
                capacity = dto.capacity,
                category_id = dto.category_id,
            };

            await _roomRepository.AddRoomAsync(room);

            return CreatedAtAction("GetRoom", new { id = room.id }, room);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {
            var Room = await _roomRepository.DeleteRoomAsync(id);
            
            if (Room == null) {
                return NotFound();
            }
            
            return Ok(Room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom([FromRoute] int id, [FromBody] RoomDto dto)
        {
            var room = new Room 
            {
                name = dto.name,
                description = dto.description,
                capacity = dto.capacity,
                category_id = dto.category_id,
            };
            
            var updatedRoom = await _roomRepository.UpdateRoomAsync(id, room);
            
            if (updatedRoom == null) {
                return NotFound();
            }

            return Ok(updatedRoom);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchRoom([FromRoute] int id, [FromBody] JsonPatchDocument RoomDocument)
        {
            var updatedRoom = await _roomRepository.UpdateRoomPatchAsync(id, RoomDocument);
            
            if (updatedRoom == null) {
                return NotFound();
            }
            
            return Ok(updatedRoom);
        }
    }
}