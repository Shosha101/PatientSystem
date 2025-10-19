
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllEmployeeDto>>> GetAll()
    {
        var employees = await _employeeService.GetAllEmployees();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAllEmployeeDto>> GetById(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<GetAllEmployeeDto>> Create(GetAllEmployeeDto employeeDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _employeeService.AddEmployee(employeeDto);
        return CreatedAtAction(nameof(GetById), new { id = employeeDto.Id }, employeeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GetAllEmployeeDto employeeDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _employeeService.UpdateEmployee(id, employeeDto);
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
            await _employeeService.DeleteEmployee(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}