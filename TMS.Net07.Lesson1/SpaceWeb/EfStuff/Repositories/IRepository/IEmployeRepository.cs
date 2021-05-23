using SpaceWeb.EfStuff.Model;
using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IEmployeRepository : IBaseRepository<Employe>
    {
        List<Employe> GetEmployesByDepartment(long idDepartment);
    }
}
