using System;
using System.Collections.Generic;

namespace TestCVD
{
    public partial class Employee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int? ChiefId { get; set; }
        public string? Name { get; set; }
        public int? Salary { get; set; }

        public Department Department { get; set; }
    }
}
