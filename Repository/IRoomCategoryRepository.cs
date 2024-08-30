using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IRoomCategoryRepository
    {
        Task<IEnumerable<RoomCategory>> GetRoomCategorysAsync();

        Task<RoomCategory?> GetRoomCategoryByIdAsync(int id);

        Task<RoomCategory?> AddRoomCategoryAsync(RoomCategory RoomCategory);

        Task<RoomCategory?> DeleteRoomCategoryAsync(int id);

        Task<RoomCategory?> UpdateRoomCategoryAsync(int id, RoomCategory RoomCategory);

        Task<RoomCategory?> UpdateRoomCategoryPatchAsync(int id, JsonPatchDocument RoomCategory);
    }
}