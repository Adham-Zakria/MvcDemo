using Demo.DataAccess.Models.DepartmentModels;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //int Add(Department dept);
        //IEnumerable<Department> GetAll(bool IsTracking = false);
        //Department? GetById(int id);
        //int Remove(Department dept);
        //int Update(Department dept);
    }
}