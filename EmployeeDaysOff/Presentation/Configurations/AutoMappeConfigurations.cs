using EmployeeDaysOff.Application.DTOs;
using EmployeeDaysOff.Core.Entities;
using AutoMapper;

namespace EmployeeDaysOff.Presentation.Configuration
{
	public class AutoMapperConfigurations : Profile
	{
    	public AutoMapperConfigurations()
    	{
        	CreateMap<Employee, EmployeeDto>().ReverseMap();

        	CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();

	        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();

	        CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
    	}
	}
}