using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Application.Interfaces
{
	public interface ILeaveAllocationService
	{
    	Task AllocateLeaveAsync(string employeeId, int leaveTypeId, int numberOfDays, int period);
    	LeaveAllocation GetLeaveAllocationAsync(int leaveAllocationId);
    	Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsForEmployeeAsync(string employeeId);
    	Task AccrueAnnualLeaveAsync();
    	Task ResetLeaveAllocationsAsync();
	}
}
