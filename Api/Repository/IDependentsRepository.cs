using Api.Models;

namespace Api.Repository;

public interface IDependentsRepository
{
    IEnumerable<Dependent> GetAll();
    Dependent GetById(int id);
}