namespace EmployeeDaysOff.Core.Interfaces
{
	public interface IUnitOfWork
	{
    	IEmployeeRepository Employees { get; }
    	ILeaveTypeRepository LeaveTypes { get; }
    	ILeaveRequestRepository LeaveRequests { get; }
    	ILeaveAllocationRepository LeaveAllocations { get;}
    	Task<int> SaveAsync();
	}
}