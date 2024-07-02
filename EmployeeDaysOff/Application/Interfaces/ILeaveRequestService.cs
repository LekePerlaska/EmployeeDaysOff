using EmployeeDaysOff.Application.DTOs;
using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Application.Interfaces
{
	public interface ILeaveRequestService
	{
    	Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequestDto leaveRequestDto);
		LeaveRequest GetLeaveRequestByIdAsync(int leaveRequestId);
    	Task<bool> ApproveLeaveRequestAsync(int leaveRequestId, string approverId);
    	Task<bool> CancelLeaveRequestAsync(int leaveRequestId);
	}
}