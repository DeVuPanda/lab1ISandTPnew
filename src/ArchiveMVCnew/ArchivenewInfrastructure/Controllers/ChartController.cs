using ArchivenewDomain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArchivenewInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DbfacultyArchivenewContext _context; 

        public ChartController(DbfacultyArchivenewContext context)
        {
            _context = context;
        }

        [HttpGet("FacultyData")]
        public async Task<IActionResult> GetFacultyData()
        {
            var facultyData = await _context.Dates
                                .AsNoTracking()
                                .GroupBy(d => d.Faculty)
                                .Select(group => new object[] { group.Key, group.Count() })
                                .ToListAsync();

            return Ok(facultyData);
        }
    }
}
