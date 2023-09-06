using Api.Models;

namespace Api.Services;

public class BenefitsCalculationService
{
    private readonly IBenefitsCalculationStrategy _calculationStrategy;
    private decimal PaycheksYearly = 26m;

    public BenefitsCalculationService(IBenefitsCalculationStrategy calculationStrategy)
    {
        _calculationStrategy = calculationStrategy;
    }
    
    public decimal Calculate(Employee employee)
    {
        var benefitsDeduction = _calculationStrategy.Calculate(employee);
        
        var monthlyBenefitsDeduction = benefitsDeduction / PaycheksYearly;
        var monthlySalary = employee.Salary / PaycheksYearly;
        return monthlySalary - monthlyBenefitsDeduction;
    }
}