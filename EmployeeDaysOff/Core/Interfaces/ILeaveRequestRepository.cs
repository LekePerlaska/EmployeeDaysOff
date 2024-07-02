using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Core.Interfaces
{
	public interface ILeaveRequestRepository
	{
		void Add(LeaveRequest leaveRequest);
		void Update(LeaveRequest leaveRequest);
    	IEnumerable<LeaveRequest> GetLeaveRequestsById(int leaveRequestId);
	}
}