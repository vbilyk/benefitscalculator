using Api.Dtos.Dependent;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentsRepository _repository;
    private readonly IMapper _mapper;

    public DependentsController(IDependentsRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = _repository.GetById(id);
        var dependentDto = _mapper.Map<GetDependentDto>(dependent);
        return new ApiResponse<GetDependentDto>()
        {
            Data = dependentDto,
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = _repository.GetAll();
        var dependentsDto = _mapper.Map<List<GetDependentDto>>(dependents);
        return new ApiResponse<List<GetDependentDto>>()
        {
            Data = dependentsDto,
            Success = true
        };
    }
}
