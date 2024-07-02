using EmployeeDaysOff.Application.Interfaces;
using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Application.Services
{
	public class LeaveAllocationService : ILeaveAllocationService
	{
    	private readonly IUnitOfWork _unitOfWork;

    	public LeaveAllocationService(IUnitOfWork unitOfWork)
    	{
        	_unitOfWork = unitOfWork;
    	}

    	public async Task AllocateLeaveAsync(string employeeId, int leaveTypeId, int numberOfDays, int period)
    {
        var leaveAllocation = new LeaveAllocation
        {
            EmployeeId = employeeId,
            LeaveTypeId = leaveTypeId,
            NumberOfDays = numberOfDays,
            DateCreated = DateTime.Now,
            Period = period
        };

        await _unitOfWork.LeaveAllocations.AddAsync(leaveAllocation);
        await _unitOfWork.SaveAsync();
    }

    public LeaveAllocation GetLeaveAllocationAsync(int leaveAllocationId)
    {
        return (LeaveAllocation)_unitOfWork.LeaveAllocations.GetById(leaveAllocationId);
    }

    public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsForEmployeeAsync(string employeeId)
    {
        return await _unitOfWork.LeaveAllocations.FindAsync(la => la.EmployeeId == employeeId);
    }

    public async Task AccrueAnnualLeaveAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        foreach (var employee in employees)
        {
            var annualLeaveType = (LeaveType)await _unitOfWork.LeaveTypes.FindAsync(lt => lt.Name == "Annual");
            if (annualLeaveType == null)
            {
                throw new Exception("Annual leave type not found");
            }

            var leaveAllocation = (LeaveAllocation)await _unitOfWork.LeaveAllocations
                .FindAsync(la => la.EmployeeId == employee.Id && la.LeaveTypeId == annualLeaveType.Id && la.Period == DateTime.Now.Year);

            if (leaveAllocation == null)
            {
                await AllocateLeaveAsync(employee.Id, annualLeaveType.Id, 2, DateTime.Now.Year);
            }
            else
            {
                leaveAllocation.NumberOfDays += 2;
                _unitOfWork.LeaveAllocations.Update(leaveAllocation);
            }
        }

        await _unitOfWork.SaveAsync();
    }

    public async Task ResetLeaveAllocationsAsync()
    {
        var leaveTypes = await _unitOfWork.LeaveTypes.GetAllAsync();
        foreach (var leaveType in leaveTypes)
        {
            if (leaveType.Name != "Annual")
            {
                var leaveAllocations = await _unitOfWork.LeaveAllocations
                    .FindAsync(la => la.LeaveTypeId == leaveType.Id);

                foreach (var leaveAllocation in leaveAllocations)
                {
                    leaveAllocation.NumberOfDays = leaveType.DefaultDays;
                    _unitOfWork.LeaveAllocations.Update(leaveAllocation);
                }
            }
        }

        var annualLeaveType = (LeaveType)await _unitOfWork.LeaveTypes.FindAsync(lt => lt.Name == "Annual");
        if (annualLeaveType != null)
        {
            var annualLeaveAllocations = await _unitOfWork.LeaveAllocations
                .FindAsync(la => la.LeaveTypeId == annualLeaveType.Id && la.Period == DateTime.Now.Year - 1);

            foreach (var annualLeaveAllocation in annualLeaveAllocations)
            {
                _unitOfWork.LeaveAllocations.Remove(annualLeaveAllocation);
            }
        }

        await _unitOfWork.SaveAsync();
    }
	}
}