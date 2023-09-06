using Api.Models;
using Api.Repository;
using Api.Services;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void AddBenefitsCalculationServices(this IServiceCollection services)
    {
        services.AddScoped<IBenefitsCalculationStrategy, BaseCostCalculationStrategy>();
        services.AddScoped<DependentCostCalculationStrategy>();
        services.AddScoped<HighEarningEmployeeCostCalculationStrategy>();
        services.AddScoped<DependentOver50CostCalculationStrategy>();
        services.AddScoped<BenefitsCalculationService>();
    }
    
    public static void InitBenefitsTestData(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<BenefitsCalculatorContext>();
        
        var employee1 = new Employee()
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };

        var employee2Id = 2;
        var employee2Dependent1 = new Dependent()
        {
            Id = 1,
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3),
            EmployeeId = employee2Id
        };
        
        var employee2Dependent2 = new Dependent()
        {
            Id = 2,
            FirstName = "Child1",
            LastName = "Morant",
            Relationship = Relationship.Child,
            DateOfBirth = new DateTime(2020, 6, 23),
            EmployeeId = employee2Id
        };
        
        var employee2Dependent3 = new Dependent()
        {
            Id = 3,
            FirstName = "Child2",
            LastName = "Morant",
            Relationship = Relationship.Child,
            DateOfBirth = new DateTime(2021, 5, 18),
            EmployeeId = employee2Id
        };
        
        var employee2 = new Employee()
        {
            Id = employee2Id,
            FirstName = "Ja",
            LastName = "Morant",
            Salary = 92365.22m,
            DateOfBirth = new DateTime(1999, 8, 10),
            Dependents = new List<Dependent>()
            {
                employee2Dependent1,
                employee2Dependent2,
                employee2Dependent3
            }
        };

        var employee3Id = 3;
        
        var employee3Dependent1 = new Dependent()
        {
            Id = 4,
            FirstName = "DP",
            LastName = "Jordan",
            Relationship = Relationship.DomesticPartner,
            DateOfBirth = new DateTime(1974, 1, 2),
            EmployeeId = employee3Id
        };
        
        var employee3 = new Employee()
        {
            Id = employee3Id,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12m,
            DateOfBirth = new DateTime(1963, 2, 17),
            Dependents = new List<Dependent>()
            {
                employee3Dependent1
            }
        };
        
        appDbContext.Dependents.AddRange(employee2Dependent1,employee2Dependent2,employee2Dependent3, employee3Dependent1);
        appDbContext.Employees.AddRange(employee1, employee2, employee3);
        appDbContext.SaveChanges();
    }
}