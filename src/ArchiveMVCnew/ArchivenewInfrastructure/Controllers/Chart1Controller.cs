using ArchivenewDomain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArchivenewInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Chart1Controller : ControllerBase
    {
        private readonly DbfacultyArchivenewContext _context;

        public Chart1Controller(DbfacultyArchivenewContext context)
        {
            _context = context;
        }

        [HttpGet("DepartmentData")]
        public async Task<IActionResult> GetDepartmentData()
        {
            var departmentData = await _context.Dates
                                .AsNoTracking()
                                .Where(d => d.Department != null)
                                .GroupBy(d => d.Department)
                                .Select(group => new object[] { group.Key, group.Count() })
                                .ToListAsync();

            return Ok(departmentData);
        }
    }
}
