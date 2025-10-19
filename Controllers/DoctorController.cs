using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace PatientSystem.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllDoctorDto>>> GetAll()
        {
            var doctors = await _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAllDoctorDto>> GetById(int id)
        {
            var doctor = await _doctorService.GetDoctorById(id);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<GetAllDoctorDto>> Create(GetAllDoctorDto doctorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _doctorService.AddDoctor(doctorDto);
            return CreatedAtAction(nameof(GetById), new { id = doctorDto.Id }, doctorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GetAllDoctorDto doctorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _doctorService.UpdateDoctor(id, doctorDto);
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
                await _doctorService.DeleteDoctor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
