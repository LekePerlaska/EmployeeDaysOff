using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDaysOff.Infrastructure.Data.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
    	private readonly ApplicationDbContext _context;

    	public EmployeeRepository(ApplicationDbContext context)
    	{
        	_context = context;
    	}

		public async Task<IEnumerable<Employee>> GetAllAsync()
    	{
        	return await _context.Set<Employee>().ToListAsync();
    	}

		public async Task<Employee> GetEmployeeByIdAsync(string employeeId)
		{
			return await _context.Employees
				.FirstOrDefaultAsync(e => e.Id == employeeId);
		}

    	public async Task<Employee> GetEmployeeWithReportsToAsync(string employeeId)
    	{
        	return await _context.Employees
            	.Include(e => e.ReportsTo)
            	.FirstOrDefaultAsync(e => e.Id == employeeId);
    	}
	}
}