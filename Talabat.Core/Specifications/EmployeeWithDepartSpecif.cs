using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class EmployeeWithDepartSpecif:BaseSpecification<Employee>
    {
        public EmployeeWithDepartSpecif()
        {
            Include.Add(e => e.Department);
        }
    }
}
