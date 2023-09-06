using Api.Models;

namespace Api.Services;

public interface IBenefitsCalculationStrategy
{
    decimal Calculate(Employee employee);
}

public abstract class BaseBenefitsCalculationStrategy : IBenefitsCalculationStrategy
{
    private IBenefitsCalculationStrategy _nextStrategy;

    protected BaseBenefitsCalculationStrategy(IBenefitsCalculationStrategy nextStrategy)
    {
        _nextStrategy = nextStrategy;
    }

    public decimal Calculate(Employee employee)
    {
        decimal result = PerformCalculation(employee);

        if (_nextStrategy != null)
        {
            result += _nextStrategy.Calculate(employee);
        }

        return result;
    }

    protected abstract decimal PerformCalculation(Employee employee);
}

public class BaseCostCalculationStrategy : BaseBenefitsCalculationStrategy
{
    public BaseCostCalculationStrategy(DependentCostCalculationStrategy next)
        :base(next){}
    protected override decimal PerformCalculation(Employee employee)
    {
        return 1000m;
    }
}

public class DependentCostCalculationStrategy : BaseBenefitsCalculationStrategy
{
    public DependentCostCalculationStrategy(HighEarningEmployeeCostCalculationStrategy next)
        :base(next) {}
    protected override decimal PerformCalculation(Employee employee)
    {
        return employee.Dependents.Count * 600m;
    }
}

public class HighEarningEmployeeCostCalculationStrategy : BaseBenefitsCalculationStrategy
{
    public HighEarningEmployeeCostCalculationStrategy(DependentOver50CostCalculationStrategy next) 
        :base(next) {}
    protected override decimal PerformCalculation(Employee employee)
    {
        return employee.Salary > 80000m ? (employee.Salary * 0.02m) : 0m;
    }
}

public class DependentOver50CostCalculationStrategy : BaseBenefitsCalculationStrategy
{
    public DependentOver50CostCalculationStrategy() :base(null){}
    protected override decimal PerformCalculation(Employee employee)
    {
        return employee.Dependents.Count(d => d.DateOfBirth.Year < (DateTime.Now.Year - 50)) * 200m;
    }
}