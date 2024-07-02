using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDaysOff.Infrastructure.Data.Repositories
{
	public class LeaveRequestRepository : ILeaveRequestRepository
	{
    	private readonly ApplicationDbContext _context;

    	public LeaveRequestRepository(ApplicationDbContext context)
    	{
        	_context = context;
    	}

		public void Add(LeaveRequest leaveRequest)
		{
			_context.Set<LeaveRequest>().Add(leaveRequest);
		}

		public void Update(LeaveRequest existingLeaveRequest)
		{
			_context.Set<LeaveRequest>().Update(existingLeaveRequest);
		}

    	public IEnumerable<LeaveRequest> GetLeaveRequestsById(int leaveRequestId)
    	{
        	return _context.Set<LeaveRequest>().Where(lR => lR.Id == leaveRequestId);
    	}
	}
}