using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/reservations")]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationRepository.GetReservationsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation([FromRoute] int id)
        {
            var Reservation = await _reservationRepository.GetReservationByIdAsync(id);
            
            if (Reservation == null) {
                return NotFound();
            }
            
            return Ok(Reservation);
        }

        [HttpPost]
        public async Task<IActionResult> StoreReservation([FromBody] Reservation Reservation)
        {
            await _reservationRepository.AddReservationAsync(Reservation);
            return CreatedAtAction("GetReservation", new { id = Reservation.id }, Reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            var Reservation = await _reservationRepository.DeleteReservationAsync(id);
            
            if (Reservation == null) {
                return NotFound();
            }
            
            return Ok(Reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation([FromRoute] int id, [FromBody] Reservation Reservation)
        {
            if (id != Reservation.id) {
                return BadRequest();
            }

            var updatedReservation = await _reservationRepository.UpdateReservationAsync(id, Reservation);
            
            if (updatedReservation == null) {
                return NotFound();
            }

            return Ok(updatedReservation);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchReservation([FromRoute] int id, [FromBody] JsonPatchDocument ReservationDocument)
        {
            var updatedReservation = await _reservationRepository.UpdateReservationPatchAsync(id, ReservationDocument);
            
            if (updatedReservation == null) {
                return NotFound();
            }
            
            return Ok(updatedReservation);
        }
    }
}