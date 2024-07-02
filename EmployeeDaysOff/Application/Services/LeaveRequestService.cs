using EmployeeDaysOff.Core.Interfaces;
using EmployeeDaysOff.Application.DTOs;
using EmployeeDaysOff.Core.Entities;
using EmployeeDaysOff.Application.Interfaces;

namespace EmployeeDaysOff.Application.Services
{
	public class LeaveRequestService : ILeaveRequestService
	{
    	private readonly IUnitOfWork _unitOfWork;

    	public LeaveRequestService(IUnitOfWork unitOfWork)
    	{
        	_unitOfWork = unitOfWork;
    	}

		public async Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequestDto leaveRequestDto)
    	{
        	var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(leaveRequestDto.RequestingEmployeeId);
        	if (employee == null)
        	{
            	throw new Exception("Employee not found");
        	}

        	var leaveType = await _unitOfWork.LeaveTypes.GetByIdAsync(leaveRequestDto.LeaveTypeId);
        	if (leaveType == null)
        	{
            	throw new Exception("Leave type not found");
        	}

        	// Validate if employee has enough leave days
        	var leaveAllocation = (LeaveAllocation)await _unitOfWork.LeaveAllocations
            	.FindAsync(la => la.EmployeeId == leaveRequestDto.RequestingEmployeeId && la.LeaveTypeId == leaveRequestDto.LeaveTypeId);

        	if (leaveAllocation == null || leaveAllocation.NumberOfDays < (leaveRequestDto.EndDate - leaveRequestDto.StartDate).Days)
        	{
            	throw new Exception("Not enough leave days");
        	}

        	var leaveRequest = new LeaveRequest
        	{
            	RequestingEmployeeId = leaveRequestDto.RequestingEmployeeId,
            	LeaveTypeId = leaveRequestDto.LeaveTypeId,
            	StartDate = leaveRequestDto.StartDate,
            	EndDate = leaveRequestDto.EndDate,
            	DateRequested = DateTime.Now,
            	RequestComments = leaveRequestDto.RequestComments,
            	Approved = null,
            	Cancelled = false
        	};

        	_unitOfWork.LeaveRequests.Add(leaveRequest);
        	await _unitOfWork.SaveAsync();

        	return leaveRequest;
    	}

		public LeaveRequest GetLeaveRequestByIdAsync(int leaveRequestId)
		{
			return (LeaveRequest)_unitOfWork.LeaveRequests.GetLeaveRequestsById(leaveRequestId);
		}

    	public async Task<bool> ApproveLeaveRequestAsync(int leaveRequestId, string approverId)
    	{
        	LeaveRequest leaveRequest = (LeaveRequest)_unitOfWork.LeaveRequests.GetLeaveRequestsById(leaveRequestId);
        	if (leaveRequest == null)
        	{
            	throw new Exception("Leave request not found");
        	}

        	var approver = await _unitOfWork.Employees.GetEmployeeByIdAsync(approverId);
        	if (approver == null)
        	{
            	throw new Exception("Approver not found");
        	}

        	leaveRequest.Approved = true;
        	leaveRequest.ApprovedById = approverId;
        	leaveRequest.DateActioned = DateTime.Now;

        	// Subtract leave days from allocation
        	var leaveAllocation = (LeaveAllocation)await _unitOfWork.LeaveAllocations
            	.FindAsync(la => la.EmployeeId == leaveRequest.RequestingEmployeeId && la.LeaveTypeId == leaveRequest.LeaveTypeId);

        	if (leaveAllocation == null)
        	{
            	throw new Exception("Leave allocation not found");
        	}

        	leaveAllocation.NumberOfDays -= (leaveRequest.EndDate - leaveRequest.StartDate).Days;

        	_unitOfWork.LeaveRequests.Update(leaveRequest);
        	_unitOfWork.LeaveAllocations.Update(leaveAllocation);
        	await _unitOfWork.SaveAsync();

        	return true;
    	}

    	public async Task<bool> CancelLeaveRequestAsync(int leaveRequestId)
    	{
        	var leaveRequest = (LeaveRequest)_unitOfWork.LeaveRequests.GetLeaveRequestsById(leaveRequestId);
        	if (leaveRequest == null)
        	{
            	throw new Exception("Leave request not found");
        	}

        	leaveRequest.Cancelled = true;
        	_unitOfWork.LeaveRequests.Update(leaveRequest);
        	await _unitOfWork.SaveAsync();

        	return true;
    	}

	}
}