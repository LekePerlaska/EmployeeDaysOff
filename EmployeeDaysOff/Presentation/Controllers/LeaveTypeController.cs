using Microsoft.AspNetCore.Mvc;
using EmployeeDaysOff.Application.Interfaces;
using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LeaveTypeController : ControllerBase
	{
    	private readonly ILeaveTypeService _leaveTypeService;

    	public LeaveTypeController(ILeaveTypeService leaveTypeService)
    	{
        	_leaveTypeService = leaveTypeService;
    	}

    	[HttpPost]
    	public async Task<IActionResult> CreateLeaveType([FromBody] LeaveType leaveType)
    	{
        	var createdLeaveType = await _leaveTypeService.CreateLeaveTypeAsync(leaveType);
        	return CreatedAtAction(nameof(GetLeaveTypeById), new { id = createdLeaveType.Id }, createdLeaveType);
    	}

    	[HttpPut("{id}")]
    	public async Task<IActionResult> UpdateLeaveType(int id, [FromBody] LeaveType leaveType)
    	{
        	if (id != leaveType.Id)
        	{
            	return BadRequest();
        	}

        	try
        	{
            	var updatedLeaveType = await _leaveTypeService.UpdateLeaveTypeAsync(leaveType);
            	return Ok(updatedLeaveType);
        	}
        	catch (Exception ex)
        	{
            	return NotFound(ex.Message);
        	}
    	}

    	[HttpGet("{id}")]
    	public async Task<IActionResult> GetLeaveTypeById(int id)
    	{
        	var leaveType = await _leaveTypeService.GetLeaveTypeByIdAsync(id);
        	if (leaveType == null)
        	{
            	return NotFound();
        	}

        	return Ok(leaveType);
    	}

    	[HttpDelete("{id}")]
    	public async Task<IActionResult> DeleteLeaveType(int id)
    	{
        	try
        	{
            	var result = await _leaveTypeService.DeleteLeaveTypeAsync(id);
            	return Ok(result);
        	}
        	catch (Exception ex)
        	{
            	return NotFound(ex.Message);
        	}
    	}
	}	
}
