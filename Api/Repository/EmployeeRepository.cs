using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly BenefitsCalculatorContext _dbContext;

    public EmployeeRepository(BenefitsCalculatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Employee> GetAll()
        => _dbContext.Employees.Include(e => e.Dependents);

    public Employee GetById(int id)
    {
        return _dbContext.Employees
            .Include(e => e.Dependents)
            .FirstOrDefault(e => e.Id == id);
    }
}