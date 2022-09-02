namespace TestCVD
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            ShowSumSalary(false);

            ShowDepartmentWithMaxSalary();

            ShowChiefDepartmentSalaries();
        }

        static void ShowSumSalary(bool chiefInclude)
        {
            using var db = new test_cvdContext();

            var data = db.Employees.AsQueryable();

            if (!chiefInclude)
            {
                data = data.Where(x => x.ChiefId != null);
            }

            var groups = data
                .GroupBy(u => u.DepartmentId)
                .Select(g => new
                {
                    Dep = g.Key,
                    Sum = g.Sum(x => x.Salary)
                })
                .OrderBy(x => x.Dep);

            Console.WriteLine("Суммарная зарплата по департаментам:");
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Dep}\t{g.Sum}");
            }
        }

        static void ShowDepartmentWithMaxSalary()
        {
            using var db = new test_cvdContext();

            var empWithMaxSalary = db.Employees
                .OrderByDescending(x => x.Salary)
                .FirstOrDefault();

            Console.Write("Департамент с максимальной зарплатой сотрудника: ");
            Console.WriteLine(empWithMaxSalary?.DepartmentId);
        }

        static void ShowChiefDepartmentSalaries()
        {
            using var db = new test_cvdContext();

            var chiefs = db.Employees
                .Where(x => x.ChiefId == null)
                .OrderByDescending(x => x.Salary);

            Console.WriteLine("Зарплата руководителей департаментов:");
            foreach (var chief in chiefs)
            {
                Console.WriteLine($"{chief.DepartmentId}\t{chief.Salary}");
            }
        }
    }

}
