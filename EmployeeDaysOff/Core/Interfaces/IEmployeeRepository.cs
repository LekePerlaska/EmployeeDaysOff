using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Core.Interfaces
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetAllAsync();
		Task<Employee> GetEmployeeByIdAsync(string employeeId);
    	Task<Employee> GetEmployeeWithReportsToAsync(string employeeId);
	}
}