using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArchivenewDomain.Model;
using ArchivenewInfrastructure;

namespace ArchivenewInfrastructure.Controllers
{
    public class ReferencesController : Controller
    {
        private readonly DbfacultyArchivenewContext _context;

        public ReferencesController(DbfacultyArchivenewContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> ReferenceIndex(string searchTerm, string searchBy)
        {
            IQueryable<Date> dates = _context.Dates;

            if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(searchBy))
            {
                switch (searchBy)
                {
                    case "FullName":
                        dates = dates.Where(d => d.FullName.Contains(searchTerm));
                        break;
                    case "Title":
                        dates = dates.Where(d => d.Title.Contains(searchTerm));
                        break;
                    case "Faculty":
                        dates = dates.Where(d => d.Faculty.Contains(searchTerm));
                        break;
                    default:
                        break;
                }

                var dateList = await dates.ToListAsync();
                return View(dateList);
            }

            
            return View(new List<Date>());
        }



        private bool ReferenceExists(int id)
        {
            return _context.References.Any(e => e.ReferenceId == id);
        }
    }
}