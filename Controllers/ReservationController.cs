
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllReservationDto>>> GetAll()
    {
        var reservations = await _reservationService.GetAllReservations();
        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAllReservationDto>> GetById(int id)
    {
        var reservation = await _reservationService.GetReservationById(id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<ActionResult<GetAllReservationDto>> Create(GetAllReservationDto reservationDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _reservationService.AddReservation(reservationDto);
        return CreatedAtAction(nameof(GetById), new { id = reservationDto.Id }, reservationDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GetAllReservationDto reservationDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _reservationService.UpdateReservation(id, reservationDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _reservationService.DeleteReservation(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("doctor/{doctorId}")]
    public async Task<ActionResult<IEnumerable<GetAllReservationDto>>> GetByDoctor(int doctorId)
    {
        var reservations = await _reservationService.GetReservationsByDoctor(doctorId);
        return Ok(reservations);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<ActionResult<IEnumerable<GetAllReservationDto>>> GetByPatient(int patientId)
    {
        var reservations = await _reservationService.GetReservationsByPatient(patientId);
        return Ok(reservations);
    }
}
