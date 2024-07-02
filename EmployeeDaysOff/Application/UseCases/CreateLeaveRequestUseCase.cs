using EmployeeDaysOff.Application.Services;
using EmployeeDaysOff.Application.DTOs;

namespace EmployeeDaysOff.Application.UseCases
{
	public class CreateLeaveRequestUseCase
	{
    	private readonly LeaveRequestService _leaveRequestService;

    	public CreateLeaveRequestUseCase(LeaveRequestService leaveRequestService)
    	{
        	_leaveRequestService = leaveRequestService;
    	}

    	public async Task<bool> Execute(LeaveRequestDto requestDto)
    	{
        	return await _leaveRequestService.CreateLeaveRequest(requestDto);
    	}
	}
}