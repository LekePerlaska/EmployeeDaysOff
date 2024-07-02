using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeDaysOff.Infrastructure.Data.Repositories
{
	public class LeaveAllocationRepository : ILeaveAllocationRepository
	{
    	private readonly ApplicationDbContext _context;

    	public LeaveAllocationRepository(ApplicationDbContext context)
    	{
        	_context = context;
    	}

		public async Task AddAsync(LeaveAllocation entity)
    	{
        	await _context.Set<LeaveAllocation>().AddAsync(entity);
    	}

		public async Task<IEnumerable<LeaveAllocation>> GetAllAsync()
    	{
        	return await _context.Set<LeaveAllocation>().ToListAsync();
    	}

    	public IEnumerable<LeaveAllocation> GetLeaveAllocationsByEmployeeId(string employeeId)
    	{
        	return _context.Set<LeaveAllocation>().Where(lA => lA.EmployeeId == employeeId);
    	}
		public IEnumerable<LeaveAllocation> GetById(int leaveAllocationId)
		{
			return _context.Set<LeaveAllocation>().Where(lA => lA.Id == leaveAllocationId);
		}
		public async Task<IEnumerable<LeaveAllocation>> FindAsync(Expression<Func<LeaveAllocation, bool>> predicate)
		{
			return await _context.Set<LeaveAllocation>().Where(predicate).ToListAsync();
		}
		public void Update(LeaveAllocation entity)
    	{
        	_context.Set<LeaveAllocation>().Update(entity);
    	}
		public void Remove(LeaveAllocation entity)
		{
			_context.Set<LeaveAllocation>().Remove(entity);
		}
	}
}