using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync();

        Task<Room?> GetRoomByIdAsync(int id);

        Task<Room?> AddRoomAsync(Room Room);

        Task<Room?> DeleteRoomAsync(int id);

        Task<Room?> UpdateRoomAsync(int id, Room Room);

        Task<Room?> UpdateRoomPatchAsync(int id, JsonPatchDocument Room);
    }
}