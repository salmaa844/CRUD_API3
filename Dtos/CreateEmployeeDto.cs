﻿namespace crudAPI.Dtos
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }
}
