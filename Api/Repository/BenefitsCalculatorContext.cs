using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class BenefitsCalculatorContext : DbContext
{
    public BenefitsCalculatorContext(DbContextOptions<BenefitsCalculatorContext> options) : base(options)
    {
        
    }
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Dependent> Dependents => Set<Dependent>();
}