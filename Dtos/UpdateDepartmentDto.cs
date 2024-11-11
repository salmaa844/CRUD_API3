using crudAPI.Data.Models;

namespace crudAPI.Dtos
{
    public class UpdateDepartmentDto
    {
       
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
