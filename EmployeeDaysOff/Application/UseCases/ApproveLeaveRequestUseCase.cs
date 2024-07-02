using EmployeeDaysOff.Application.Services;
using EmployeeDaysOff.Application.DTOs;

namespace EmployeeDaysOff.Application.UseCases
{
	public class ApproveLeaveRequestUseCase
	{
    	private readonly LeaveRequestService _leaveRequestService;

    	public ApproveLeaveRequestUseCase(LeaveRequestService leaveRequestService)
    	{
        	_leaveRequestService = leaveRequestService;
    	}

    	public async Task<bool> Execute(LeaveRequestDto requestDto)
    	{
        	return await _leaveRequestService.ApproveLeaveRequest(requestDto);
    	}
	}
}