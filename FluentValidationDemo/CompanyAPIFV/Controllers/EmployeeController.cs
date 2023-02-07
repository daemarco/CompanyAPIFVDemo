using CompanyAPIFV.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPIFV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {

        }

        [HttpPost]
        public IActionResult RegisterEmplyee(RegisterEmployeeRequest request) 
        {
            return Ok("TODO-Marco: " + request.Name);
        }
    }
}
