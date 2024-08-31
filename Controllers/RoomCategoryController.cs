using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;
using WebApi.ModelsViewModels;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Authorize]
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
        public async Task<IEnumerable<RoomCategoryViewModel>> GetRoomCategorys()
        {
            var roomcategories = await _roomCategoryRepository.GetRoomCategorysAsync();

            var viewModel = roomcategories.Select(u => new RoomCategoryViewModel {
                id = u.id,
                description = u.description,
            });

            return viewModel;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomCategory([FromRoute] int id)
        {
            var roomCategory = await _roomCategoryRepository.GetRoomCategoryByIdAsync(id);
            
            if (roomCategory == null) {
                return NotFound();
            }

            var viewModel = new RoomCategoryViewModel {
                id = roomCategory.id,
                description = roomCategory.description,
            };
            
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StoreRoomCategory([FromBody] RoomCategoryDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var roomCategory = new RoomCategory {
                description = dto.description,
            };

            await _roomCategoryRepository.AddRoomCategoryAsync(roomCategory);

            return CreatedAtAction("GetRoomCategory", new { id = roomCategory.id }, roomCategory);
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
        public async Task<IActionResult> UpdateRoomCategory([FromRoute] int id, [FromBody] RoomCategoryDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var roomCategory = new RoomCategory {
                description = dto.description,
            };

            var updatedRoomCategory = await _roomCategoryRepository.UpdateRoomCategoryAsync(id, roomCategory);
            
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