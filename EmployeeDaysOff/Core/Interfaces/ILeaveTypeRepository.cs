using EmployeeDaysOff.Core.Entities;
using System.Linq.Expressions;

namespace EmployeeDaysOff.Core.Interfaces
{
	public interface ILeaveTypeRepository
	{
		void Add(LeaveType leaveType);
		Task<IEnumerable<LeaveType>> GetAllAsync();
		void Update(LeaveType existingLeaveType);
		Task<LeaveType> GetByIdAsync(int id);
    	Task<LeaveType> GetByNameAsync(string name);
		Task<IEnumerable<LeaveType>> FindAsync(Expression<Func<LeaveType, bool>> predicate);
		void Remove(LeaveType leaveType);
	}
}