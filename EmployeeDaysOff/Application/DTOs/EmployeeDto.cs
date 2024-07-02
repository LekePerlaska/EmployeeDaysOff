namespace EmployeeDaysOff.Application.DTOs
{
	public class EmployeeDto
	{
    	public string Id { get; set; }
    	public string Firstname { get; set; }
    	public string Lastname { get; set; }
    	public string Position { get; set; }
    	public string ReportsTo { get; set; }
    	public DateTime DateOfBirth { get; set; }
    	public DateTime DateJoined { get; set; }
	}
}