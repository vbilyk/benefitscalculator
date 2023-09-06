using Api.Models;

namespace Api.Repository;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll();
    Employee GetById(int id);
}