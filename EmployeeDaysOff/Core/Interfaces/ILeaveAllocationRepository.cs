using EmployeeDaysOff.Core.Entities;
using System.Linq.Expressions;

namespace EmployeeDaysOff.Core.Interfaces
{
	public interface ILeaveAllocationRepository
	{
		Task AddAsync(LeaveAllocation entity);
		Task<IEnumerable<LeaveAllocation>> GetAllAsync();
    	IEnumerable<LeaveAllocation> GetLeaveAllocationsByEmployeeId(string employeeId);
    	IEnumerable<LeaveAllocation> GetById(int leaveAllocationId);
		Task<IEnumerable<LeaveAllocation>> FindAsync(Expression<Func<LeaveAllocation, bool>> predicate);
		void Update(LeaveAllocation entity);
		void Remove(LeaveAllocation entity);
	}
}