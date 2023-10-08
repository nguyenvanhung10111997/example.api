using example.service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace example.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync()
        {
            var result = await _departmentService.CreateAsync();
            return Ok(result);
        }
    }
}
