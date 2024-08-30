using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoomRepository(ApplicationDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var Rooms = await _dbContext.Rooms.AsNoTracking().ToListAsync();
            return Rooms;
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            var Room = await _dbContext.Rooms.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            return Room;
        }

        public async Task<Room?> AddRoomAsync(Room Room)
        {
            try {
                await _dbContext.Rooms.AddAsync(Room);
                await _dbContext.SaveChangesAsync();
                return Room;
            } catch (System.Exception) {
                throw new Exception("Falha ao cadstrar usuario");
            }
        }

        public async Task<Room?> DeleteRoomAsync(int id)
        {
            var Room = await GetRoomByIdAsync(id);
            
            if (Room == null) {
                return Room;
            }

            _dbContext.Rooms.Remove(Room);
            await _dbContext.SaveChangesAsync();

            return Room;
        }

        public async Task<Room?> UpdateRoomAsync(int id, Room Room)
        {
            var RoomQuery = await GetRoomByIdAsync(id);
            
            if (RoomQuery == null) {
                return RoomQuery;
            }

            _dbContext.Entry(RoomQuery).CurrentValues.SetValues(Room);
            await _dbContext.SaveChangesAsync();

            return RoomQuery;
        }

        public async Task<Room?> UpdateRoomPatchAsync(int id, JsonPatchDocument RoomDocument)
        {
            var RoomQuery = await GetRoomByIdAsync(id);
            
            if (RoomQuery == null) {
                return RoomQuery;
            }
            
            RoomDocument.ApplyTo(RoomQuery);
            await _dbContext.SaveChangesAsync();

            return RoomQuery;
        }
    }
}