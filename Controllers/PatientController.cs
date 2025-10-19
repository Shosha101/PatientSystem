
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllPatientDto>>> GetAll()
    {
        var patients = await _patientService.GetAllPatients();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAllPatientDto>> GetById(int id)
    {
        var patient = await _patientService.GetPatientById(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<GetAllPatientDto>> Create(GetAllPatientDto patientDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _patientService.AddPatient(patientDto);
        return CreatedAtAction(nameof(GetById), new { id = patientDto.Id }, patientDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GetAllPatientDto patientDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _patientService.UpdatePatient(id, patientDto);
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
            await _patientService.DeletePatient(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
