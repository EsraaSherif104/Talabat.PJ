using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabt.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _emploRepo;

        public EmployeeController(IGenericRepository<Employee> EmploRepo)
        {
            _emploRepo = EmploRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartSpecif();
            var employee = await _emploRepo.GetAllWithSpecAsync(spec);
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>>GetEmployeeById(int id)
        {
            var spec = new EmployeeWithDepartSpecif(id);
            var Employee =await _emploRepo.GetEntityWithSpecAsync(spec);
            return Ok(Employee);

        }
    }
}
