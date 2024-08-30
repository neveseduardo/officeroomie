using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
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
        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await _roomRepository.GetRoomsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom([FromRoute] int id)
        {
            var Room = await _roomRepository.GetRoomByIdAsync(id);
            
            if (Room == null) {
                return NotFound();
            }
            
            return Ok(Room);
        }

        [HttpPost]
        public async Task<IActionResult> StoreRoom([FromBody] Room Room)
        {
            await _roomRepository.AddRoomAsync(Room);
            return CreatedAtAction("GetRoom", new { id = Room.id }, Room);
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
        public async Task<IActionResult> UpdateRoom([FromRoute] int id, [FromBody] Room Room)
        {
            if (id != Room.id) {
                return BadRequest();
            }

            var updatedRoom = await _roomRepository.UpdateRoomAsync(id, Room);
            
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