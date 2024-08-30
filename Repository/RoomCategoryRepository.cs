using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repository
{
    public class RoomCategoryRepository : IRoomCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoomCategoryRepository(ApplicationDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<RoomCategory>> GetRoomCategorysAsync()
        {
            var RoomCategorys = await _dbContext.RoomCategories.AsNoTracking().ToListAsync();
            return RoomCategorys;
        }

        public async Task<RoomCategory?> GetRoomCategoryByIdAsync(int id)
        {
            var RoomCategory = await _dbContext.RoomCategories.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            return RoomCategory;
        }

        public async Task<RoomCategory?> AddRoomCategoryAsync(RoomCategory RoomCategory)
        {
            try {
                await _dbContext.RoomCategories.AddAsync(RoomCategory);
                await _dbContext.SaveChangesAsync();
                return RoomCategory;
            } catch (System.Exception) {
                throw new Exception("Falha ao cadstrar usuario");
            }
        }

        public async Task<RoomCategory?> DeleteRoomCategoryAsync(int id)
        {
            var RoomCategory = await GetRoomCategoryByIdAsync(id);
            
            if (RoomCategory == null) {
                return RoomCategory;
            }

            _dbContext.RoomCategories.Remove(RoomCategory);
            await _dbContext.SaveChangesAsync();

            return RoomCategory;
        }

        public async Task<RoomCategory?> UpdateRoomCategoryAsync(int id, RoomCategory RoomCategory)
        {
            var RoomCategoryQuery = await GetRoomCategoryByIdAsync(id);
            
            if (RoomCategoryQuery == null) {
                return RoomCategoryQuery;
            }

            _dbContext.Entry(RoomCategoryQuery).CurrentValues.SetValues(RoomCategory);
            await _dbContext.SaveChangesAsync();

            return RoomCategoryQuery;
        }

        public async Task<RoomCategory?> UpdateRoomCategoryPatchAsync(int id, JsonPatchDocument RoomCategoryDocument)
        {
            var RoomCategoryQuery = await GetRoomCategoryByIdAsync(id);
            
            if (RoomCategoryQuery == null) {
                return RoomCategoryQuery;
            }
            
            RoomCategoryDocument.ApplyTo(RoomCategoryQuery);
            await _dbContext.SaveChangesAsync();

            return RoomCategoryQuery;
        }
    }
}