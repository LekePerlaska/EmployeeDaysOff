using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Application.Interfaces
{
	public interface ILeaveTypeService
	{
    	Task<LeaveType> CreateLeaveTypeAsync(LeaveType leaveType);
    	Task<LeaveType> UpdateLeaveTypeAsync(LeaveType leaveType);
    	Task<LeaveType> GetLeaveTypeByIdAsync(int id);
    	Task<bool> DeleteLeaveTypeAsync(int id);
}
}
