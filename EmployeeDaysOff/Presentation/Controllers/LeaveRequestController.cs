using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeDaysOff.Application.UseCases;
using EmployeeDaysOff.Application.Interfaces;
using EmployeeDaysOff.Application.DTOs;

namespace EmployeeDaysOff.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LeaveRequestController : ControllerBase
	{
    	private readonly ILeaveRequestService _leaveRequestService;

    	public LeaveRequestController(ILeaveRequestService leaveRequestService)
    	{
        	_leaveRequestService = leaveRequestService;
    	}

    	[HttpPost]
    	[Authorize(Roles = "Employee")]
    	public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestDto leaveRequestDto)
    	{
        	try
        	{
            	var leaveRequest = await _leaveRequestService.CreateLeaveRequestAsync(leaveRequestDto);
            	return CreatedAtAction(nameof(GetLeaveRequestById), new { id = leaveRequest.Id }, leaveRequest);
        	}
        	catch (Exception ex)
        	{
            	return BadRequest(ex.Message);
        	}
    	}

    	[HttpPut("approve/{id}")]
    	[Authorize(Roles = "Manager")]
    	public async Task<IActionResult> ApproveLeaveRequest(int id, [FromQuery] string approverId)
    	{
        	try
        	{
            	var result = await _leaveRequestService.ApproveLeaveRequestAsync(id, approverId);
            	if (result)
            	{
                	return Ok();
            	}
            	return BadRequest("Unable to approve leave request");
        	}
        	catch (Exception ex)
        	{
            	return BadRequest(ex.Message);
        	}
    	}

    	[HttpPut("cancel/{id}")]
    	[Authorize(Roles = "Employee,Manager")]
    	public async Task<IActionResult> CancelLeaveRequest(int id)
    	{
        	try
        	{
            	var result = await _leaveRequestService.CancelLeaveRequestAsync(id);
            	if (result)
            	{
                	return Ok();
            	}
            	return BadRequest("Unable to cancel leave request");
        	}
        	catch (Exception ex)
        	{
            	return BadRequest(ex.Message);
        	}
    	}

    	[HttpGet("{id}")]
    	[Authorize(Roles = "Employee,Manager")]
    	public IActionResult GetLeaveRequestById(int id)
    	{
        	var leaveRequest = _leaveRequestService.GetLeaveRequestByIdAsync(id);
        	if (leaveRequest == null)
        	{
            	return NotFound();
        	}

        	return Ok(leaveRequest);
    	}
	}
}