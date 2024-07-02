using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeDaysOff.Infrastructure.Data.Repositories
{
	public class LeaveTypeRepository : ILeaveTypeRepository
	{
    	private readonly ApplicationDbContext _context;

    	public LeaveTypeRepository(ApplicationDbContext context)
    	{
        	_context = context;
    	}

		public void Add(LeaveType leaveType)
		{
			_context.Set<LeaveType>().Add(leaveType);
		}

		public void Update(LeaveType existingLeaveType)
		{
			_context.Set<LeaveType>().Update(existingLeaveType);
		}
		public async Task<IEnumerable<LeaveType>> GetAllAsync()
    	{
        	return await _context.Set<LeaveType>().ToListAsync();
    	}
		public async Task<LeaveType> GetByIdAsync(int leaveTypeId)
		{
			return await _context.LeaveTypes
				.Include(lT => lT.Id)
				.FirstOrDefaultAsync(lT => lT.Id == leaveTypeId);
		}

    	public async Task<LeaveType> GetByNameAsync(string leaveTypeName)
    	{
        	return await _context.LeaveTypes
            	.Include(lT => lT.Name)
            	.FirstOrDefaultAsync(lT => lT.Name == leaveTypeName);
    	}

		public async Task<IEnumerable<LeaveType>> FindAsync(Expression<Func<LeaveType, bool>> predicate)
		{
			return await _context.Set<LeaveType>().Where(predicate).ToListAsync();
		}

		public void Remove(LeaveType leaveType)
		{
			_context.Set<LeaveType>().Remove(leaveType);
		}
	}
}