using Api.Dtos.Dependent;
using Api.Models;
using AutoMapper;

namespace Api.Mapping;

public class DependentProfile : Profile
{
    public DependentProfile()
    {
        this.CreateMap<Dependent, GetDependentDto>();
    }
}