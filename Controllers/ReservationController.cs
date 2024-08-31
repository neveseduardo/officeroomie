using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Models;
using WebApi.ModelViewModels;
using WebApi.DTO;

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
        public async Task<IEnumerable<ReservationViewModel>> GetReservations()
        {
            var reservations = await _reservationRepository.GetReservationsAsync();

            var viewModel = reservations.Select(u => new ReservationViewModel {
                id = u.id,
                reservation_date = u.reservation_date,
                initial_hour = u.initial_hour,
                finish_hour = u.finish_hour,
                protocol = u.protocol,
                status = u.status,
            });

            return viewModel;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation([FromRoute] int id)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            
            if (reservation == null) {
                return NotFound();
            }

            var reservationViewModel = new ReservationViewModel {
                id = reservation.id,
                reservation_date = reservation.reservation_date,
                initial_hour = reservation.initial_hour,
                finish_hour = reservation.finish_hour,
                protocol = reservation.protocol,
                status = reservation.status,
            };
            
            return Ok(reservationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StoreReservation([FromBody] ReservationDto dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var reservation = new Reservation {
                client_id = dto.client_id,
                room_id = dto.room_id,
                reservation_date = dto.reservation_date,
                initial_hour = dto.initial_hour,
                finish_hour = dto.finish_hour,
                protocol = dto.protocol,
                status = dto.status,
            };

            await _reservationRepository.AddReservationAsync(reservation);

            return CreatedAtAction("GetReservation", new { id = reservation.id }, reservation);
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
        public async Task<IActionResult> UpdateReservation([FromRoute] int id, [FromBody] ReservationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservation = new Reservation {
                client_id = dto.client_id,
                room_id = dto.room_id,
                reservation_date = dto.reservation_date,
                initial_hour = dto.initial_hour,
                finish_hour = dto.finish_hour,
                protocol = dto.protocol,
                status = dto.status,
            };

            var updatedReservation = await _reservationRepository.UpdateReservationAsync(id, reservation);
            
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