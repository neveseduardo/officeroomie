using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationRepository(ApplicationDbContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            var Reservations = await _dbContext.Reservations.AsNoTracking().ToListAsync();
            return Reservations;
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            var Reservation = await _dbContext.Reservations.AsNoTracking().FirstOrDefaultAsync(x => x.id == id);
            return Reservation;
        }

        public async Task<Reservation?> AddReservationAsync(Reservation Reservation)
        {
            try {
                await _dbContext.Reservations.AddAsync(Reservation);
                await _dbContext.SaveChangesAsync();
                return Reservation;
            } catch (System.Exception) {
                throw new Exception("Falha ao cadstrar usuario");
            }
        }

        public async Task<Reservation?> DeleteReservationAsync(int id)
        {
            var Reservation = await GetReservationByIdAsync(id);
            
            if (Reservation == null) {
                return Reservation;
            }

            _dbContext.Reservations.Remove(Reservation);
            await _dbContext.SaveChangesAsync();

            return Reservation;
        }

        public async Task<Reservation?> UpdateReservationAsync(int id, Reservation Reservation)
        {
            var ReservationQuery = await GetReservationByIdAsync(id);
            
            if (ReservationQuery == null) {
                return ReservationQuery;
            }

            _dbContext.Entry(ReservationQuery).CurrentValues.SetValues(Reservation);
            await _dbContext.SaveChangesAsync();

            return ReservationQuery;
        }

        public async Task<Reservation?> UpdateReservationPatchAsync(int id, JsonPatchDocument ReservationDocument)
        {
            var ReservationQuery = await GetReservationByIdAsync(id);
            
            if (ReservationQuery == null) {
                return ReservationQuery;
            }
            
            ReservationDocument.ApplyTo(ReservationQuery);
            await _dbContext.SaveChangesAsync();

            return ReservationQuery;
        }
    }
}