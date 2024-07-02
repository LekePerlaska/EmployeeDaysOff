using Microsoft.AspNetCore.Mvc;
using EmployeeDaysOff.Application.Interfaces;
using EmployeeDaysOff.Application.DTOs;

namespace EmployeeDaysOff.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LeaveAllocationController : ControllerBase
	{
    	private readonly ILeaveAllocationService _leaveAllocationService;

    	public LeaveAllocationController(ILeaveAllocationService leaveAllocationService)
    	{
        	_leaveAllocationService = leaveAllocationService;
    	}

    	[HttpPost]
    	[Authorize(Roles = "Admin")]
    	public async Task<IActionResult> AllocateLeave([FromBody] LeaveAllocationDto allocateLeaveDto)
    	{
        	try
        	{
            	await _leaveAllocationService.AllocateLeaveAsync(allocateLeaveDto.EmployeeId, allocateLeaveDto.LeaveTypeId, allocateLeaveDto.NumberOfDays, allocateLeaveDto.Period);
            	return Ok();
        	}
        	catch (Exception ex)
        	{
            	return BadRequest(ex.Message);
        	}
    	}

    	[HttpGet("{id}")]
    	[Authorize(Roles = "Employee,Manager")]
    	public IActionResult GetLeaveAllocationById(int id)
    	{
        	var leaveAllocation = _leaveAllocationService.GetLeaveAllocationAsync(id);
        	if (leaveAllocation == null)
        	{
            	return NotFound();
        	}

        	return Ok(leaveAllocation);
    	}

    	[HttpGet("employee/{employeeId}")]
    	[Authorize(Roles = "Employee,Manager")]
    	public async Task<IActionResult> GetLeaveAllocationsForEmployee(string employeeId)
    	{
        	var leaveAllocations = await _leaveAllocationService.GetLeaveAllocationsForEmployeeAsync(employeeId);
        	return Ok(leaveAllocations);
    	}
	}
}