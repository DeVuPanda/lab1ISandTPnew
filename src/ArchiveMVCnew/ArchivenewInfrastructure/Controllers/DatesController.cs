using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArchivenewDomain.Model;
using ArchivenewInfrastructure;
using ClosedXML.Excel;

namespace ArchivenewInfrastructure.Controllers
{
    public class DatesController : Controller
    {
        private readonly DbfacultyArchivenewContext _context;

        public DatesController(DbfacultyArchivenewContext context)
        {
            _context = context;
        }

        // GET: Dates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dates.ToListAsync());
        }

        // GET: Dates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Title,Faculty,Department,Format,ExtentOfMaterial,Date1")] Date date)
        {
            if (ModelState.IsValid)
            {
                if (!IsFullNameValid(date.FullName))
                {
                    ModelState.AddModelError("FullName", "The full name should look like this \"Калениченко Денис Русланович\" and each word should be from capital letter and contains from 2 to 20 letters.");
                    return View(date);
                }

                _context.Add(date);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(date);
        }

        private bool IsFullNameValid(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            string[] words = fullName.Split(' ');
            if (words.Length != 3)
                return false;

            foreach (string word in words)
            {
                if (word.Length < 2 || word.Length > 30 || !char.IsUpper(word[0]))
                    return false;
            }

            return true;
        }



        // GET: Dates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date = await _context.Dates.FindAsync(id);
            if (date == null)
            {
                return NotFound();
            }
            return View(date);
        }

        // POST: Dates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DateId,FullName,Title,Faculty,Department,Format,ExtentOfMaterial,Date1")] Date date)
        {
            if (id != date.DateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!IsFullNameValid(date.FullName))
                {
                    ModelState.AddModelError("FullName", "The full name should look like this \"Калениченко Денис Русланович\" and each word should be from capital letter and contains from 2 to 20 letters.");
                    return View(date);
                }


                try
                {
                    _context.Update(date);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DateExists(date.DateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(date);
        }

        // GET: Dates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date = await _context.Dates
                .FirstOrDefaultAsync(m => m.DateId == id);
            if (date == null)
            {
                return NotFound();
            }

            return View(date);
        }

        // POST: Dates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var date = await _context.Dates.FindAsync(id);
            if (date != null)
            {
                _context.Dates.Remove(date);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool DateExists(int id)
        {
            return _context.Dates.Any(e => e.DateId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Export(IFormFile fileExcel)
        {
            if (fileExcel == null || fileExcel.Length == 0)
            {
                ModelState.AddModelError("", "Please select an Excel file.");
                return View();
            }

            if (!Path.GetExtension(fileExcel.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Please select an Excel file.");
                return View();
            }

            using (var stream = new MemoryStream())
            {
                await fileExcel.CopyToAsync(stream);
                using (var workBook = new XLWorkbook(stream))
                {
                    var worksheet = workBook.Worksheets.First();
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        var date = new Date
                        {
                            FullName = row.Cell(1).Value.ToString(),
                            Title = row.Cell(2).Value.ToString(),
                            Faculty = row.Cell(3).Value.ToString(),
                            Department = row.Cell(4).Value.ToString(),
                            Format = row.Cell(5).Value.ToString(),
                            ExtentOfMaterial = int.Parse(row.Cell(6).Value.ToString()),
                            Date1 = DateOnly.FromDateTime(row.Cell(7).GetDateTime())

                        };

                        _context.Dates.Add(date);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Import()
        {
            using (var workbook = new XLWorkbook())
            {
                var dates = await _context.Dates.ToListAsync();
                var worksheet = workbook.Worksheets.Add("Dates");

                worksheet.Cell("A1").Value = "Full Name";
                worksheet.Cell("B1").Value = "Title";
                worksheet.Cell("C1").Value = "Faculty";
                worksheet.Cell("D1").Value = "Department";
                worksheet.Cell("E1").Value = "Format";
                worksheet.Cell("F1").Value = "Extent of Material";
                worksheet.Cell("G1").Value = "Date";

                worksheet.Row(1).Style.Font.Bold = true;

                int row = 2;
                foreach (var date in dates)
                {
                    worksheet.Cell(row, 1).Value = date.FullName;
                    worksheet.Cell(row, 2).Value = date.Title;
                    worksheet.Cell(row, 3).Value = date.Faculty;
                    worksheet.Cell(row, 4).Value = date.Department;
                    worksheet.Cell(row, 5).Value = date.Format;
                    worksheet.Cell(row, 6).Value = date.ExtentOfMaterial;
                    worksheet.Cell(row, 7).Value = date.Date1?.ToString("yyyy-MM-dd");
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Dates.xlsx");
                }
            }
        }
    }
}
    
 
