using Api.Models;

namespace Api.Repository;

public class DependentsRepository : IDependentsRepository
{
    private readonly BenefitsCalculatorContext _dbContext;

    public DependentsRepository(BenefitsCalculatorContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Dependent> GetAll()
        => _dbContext.Dependents;

    public Dependent GetById(int id)
        => _dbContext.Dependents.FirstOrDefault(d => d.Id == id);
}