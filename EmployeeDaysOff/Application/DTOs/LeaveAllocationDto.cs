namespace EmployeeDaysOff.Application.DTOs
{
	public class LeaveAllocationDto
	{
    	public int Id { get; set; }
    	public int NumberOfDays { get; set; }
    	public DateTime DateCreated { get; set; }
    	public string EmployeeId { get; set; }
    	public EmployeeDto Employee { get; set; }
    	public int LeaveTypeId { get; set; }
    	public LeaveTypeDto LeaveType { get; set; }
    	public int Period { get; set; }
	}
}