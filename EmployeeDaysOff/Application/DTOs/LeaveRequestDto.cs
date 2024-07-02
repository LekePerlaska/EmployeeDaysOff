namespace EmployeeDaysOff.Application.DTOs
{
	public class LeaveRequestDto
	{
    	public string RequestingEmployeeId { get; set; }
    	public DateTime StartDate { get; set; }
    	public DateTime EndDate { get; set; }
    	public int LeaveTypeId { get; set; }
    	public string RequestComments { get; set; }
	}
}
