using EmployeeDaysOff.Application.Interfaces;
using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Core.Entities;

namespace EmployeeDaysOff.Application.Services
{	
	public class LeaveTypeService : ILeaveTypeService
	{
		private readonly IUnitOfWork _unitOfWork;

    	public LeaveTypeService(IUnitOfWork unitOfWork)
    	{
        	_unitOfWork = unitOfWork;
    	}

    	public async Task<LeaveType> CreateLeaveTypeAsync(LeaveType leaveType)
    	{
        	_unitOfWork.LeaveTypes.Add(leaveType);
        	await _unitOfWork.SaveAsync();
        	return leaveType;
    	}

    	public async Task<LeaveType> UpdateLeaveTypeAsync(LeaveType leaveType)
    	{
        	var existingLeaveType = await _unitOfWork.LeaveTypes.GetByIdAsync(leaveType.Id);
        	if (existingLeaveType == null)
        	{
            	throw new Exception("Leave type not found");
        	}

        	existingLeaveType.Name = leaveType.Name;
        	existingLeaveType.DefaultDays = leaveType.DefaultDays;

        	_unitOfWork.LeaveTypes.Update(existingLeaveType);
        	await _unitOfWork.SaveAsync();
        	return existingLeaveType;
    	}

    	public async Task<LeaveType> GetLeaveTypeByIdAsync(int id)
    	{
        	return await _unitOfWork.LeaveTypes.GetByIdAsync(id);
    	}

    	public async Task<bool> DeleteLeaveTypeAsync(int id)
    	{
        	var leaveType = await _unitOfWork.LeaveTypes.GetByIdAsync(id);
        	if (leaveType == null)
        	{
            	throw new Exception("Leave type not found");
        	}

        	_unitOfWork.LeaveTypes.Remove(leaveType);
        	await _unitOfWork.SaveAsync();
        	return true;
    	}
	}
}