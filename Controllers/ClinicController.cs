
using Ecommerce.BusinessLogicLayer.Dtos.Product;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClinicController : ControllerBase
{
    private readonly IClinicService _clinicService;

    public ClinicController(IClinicService clinicService)
    {
        _clinicService = clinicService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllClinicDto>>> GetAll()
    {
        var clinics = await _clinicService.GetAllClinics();
        return Ok(clinics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAllClinicDto>> GetById(int id)
    {
        var clinic = await _clinicService.GetClinicById(id);
        if (clinic == null) return NotFound();
        return Ok(clinic);
    }

    [HttpPost]
    public async Task<ActionResult<GetAllClinicDto>> Create(GetAllClinicDto clinicDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _clinicService.AddClinic(clinicDto);
        return CreatedAtAction(nameof(GetById), new { id = clinicDto.Id }, clinicDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GetAllClinicDto clinicDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _clinicService.UpdateClinic(id, clinicDto);
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
            await _clinicService.DeleteClinic(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
