using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/roomcategories")]
    public class RoomCategoryController : Controller
    {
        private readonly IRoomCategoryRepository _roomCategoryRepository;

        public RoomCategoryController(IRoomCategoryRepository roomCategoryRepository)
        {
            _roomCategoryRepository = roomCategoryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RoomCategory>> GetRoomCategorys()
        {
            return await _roomCategoryRepository.GetRoomCategorysAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomCategory([FromRoute] int id)
        {
            var RoomCategory = await _roomCategoryRepository.GetRoomCategoryByIdAsync(id);
            
            if (RoomCategory == null) {
                return NotFound();
            }
            
            return Ok(RoomCategory);
        }

        [HttpPost]
        public async Task<IActionResult> StoreRoomCategory([FromBody] RoomCategory RoomCategory)
        {
            await _roomCategoryRepository.AddRoomCategoryAsync(RoomCategory);
            return CreatedAtAction("GetRoomCategory", new { id = RoomCategory.id }, RoomCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomCategory([FromRoute] int id)
        {
            var RoomCategory = await _roomCategoryRepository.DeleteRoomCategoryAsync(id);
            
            if (RoomCategory == null) {
                return NotFound();
            }
            
            return Ok(RoomCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomCategory([FromRoute] int id, [FromBody] RoomCategory RoomCategory)
        {
            if (id != RoomCategory.id) {
                return BadRequest();
            }

            var updatedRoomCategory = await _roomCategoryRepository.UpdateRoomCategoryAsync(id, RoomCategory);
            
            if (updatedRoomCategory == null) {
                return NotFound();
            }

            return Ok(updatedRoomCategory);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchRoomCategory([FromRoute] int id, [FromBody] JsonPatchDocument RoomCategoryDocument)
        {
            var updatedRoomCategory = await _roomCategoryRepository.UpdateRoomCategoryPatchAsync(id, RoomCategoryDocument);
            
            if (updatedRoomCategory == null) {
                return NotFound();
            }
            
            return Ok(updatedRoomCategory);
        }
    }
}