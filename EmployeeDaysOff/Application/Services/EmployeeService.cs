using EmployeeDaysOff.Core.Interfaces;

namespace EmployeeDaysOff.Application.Services
{
	public class EmployeeService
	{
    	private readonly IUnitOfWork _unitOfWork;

    	public EmployeeService(IUnitOfWork unitOfWork)
    	{
        	_unitOfWork = unitOfWork;
    	}

    	// Other service methods
	}
}