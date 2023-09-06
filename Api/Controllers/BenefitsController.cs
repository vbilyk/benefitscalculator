using Api.Dtos.Benefit;
using Api.Dtos.Employee;
using Api.Models;
using Api.Repository;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BenefitsController : ControllerBase
{
    private readonly BenefitsCalculationService _service;
    private readonly IEmployeeRepository _employeeRepository;

    public BenefitsController(
        BenefitsCalculationService service,
        IEmployeeRepository employeeRepository)
    {
        _service = service;
        _employeeRepository = employeeRepository;
    }
    
    [SwaggerOperation(Summary = "Calculate paycheck")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int id)
    {
        var employee = _employeeRepository.GetById(id);
        var paycheck = _service.Calculate(employee);
        return new ApiResponse<GetPaycheckDto>()
        {
            Data = new GetPaycheckDto(){ Monthly = paycheck },
            Success = true
        };
    }
}