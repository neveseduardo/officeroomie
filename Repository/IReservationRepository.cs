using Microsoft.AspNetCore.JsonPatch;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task<Reservation?> GetReservationByIdAsync(int id);

        Task<Reservation?> AddReservationAsync(Reservation Reservation);

        Task<Reservation?> DeleteReservationAsync(int id);

        Task<Reservation?> UpdateReservationAsync(int id, Reservation Reservation);

        Task<Reservation?> UpdateReservationPatchAsync(int id, JsonPatchDocument Reservation);
    }
}