using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_OOP_WPF_HOME_WORK.BackEnd
{
    class Department : Manager
    {
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public List<Department> depList { get; set; }

        public Department(string Name, int id)
        {
            this.DepartmentName = Name;
            this.DepartmentId = id;
        }

        public new Department this[int index]
        {
            get { return this.depList[index]; }
        }
        public Department() : this("", 0)
        {

        }
        public override string ToString()
        {
            return $"{DepartmentName,15} {DepartmentId,3}";
        }
        public List<Department> AddFakeDepartment(int countDepartment)
        {
            List<Department> departmentList = new List<Department>();

            for (int i = 1; i < countDepartment; i++)
            {
                var faker = new Faker();

                departmentList.Add(new Department($"Department {faker.Person.Company.Name}", i));
            }
            return departmentList;
        }
    }
}
