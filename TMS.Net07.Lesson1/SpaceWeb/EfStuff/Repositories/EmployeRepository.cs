using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.Human;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.EfStuff.Repositories
{
    public class EmployeRepository : BaseRepository<Employe>, IEmployeRepository
    {
        public EmployeRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext) { }

        public List<Employe> GetEmployesByDepartment(long idDepartment)=>
            _dbSet.Select(x => x)
            .Where(x => x.Department.Id == idDepartment && x.EmployeStatus == EmployeStatus.Accepted)
            .ToList();

        public List<Employe> GetEmployesByDepartment(Department department) => 
            GetEmployesByDepartment(department.Id);

        public List<Employe> GetRequestsToEmploy(long idDepartment)=>
            _dbSet.Select(x => x)
            .Where(x => x.Department.Id == idDepartment && x.EmployeStatus == EmployeStatus.Request)
            .ToList();

        public List<Employe> GetRequestsToEmploy(Department department) =>
            GetRequestsToEmploy(department.Id);
    }
}