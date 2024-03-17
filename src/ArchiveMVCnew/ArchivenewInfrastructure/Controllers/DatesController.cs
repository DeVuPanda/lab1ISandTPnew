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

                if (!string.IsNullOrWhiteSpace(date.ExtentOfMaterial))
                {
                    date.ExtentOfMaterial += " сторінок";
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

                if (!string.IsNullOrWhiteSpace(date.ExtentOfMaterial))
                {
                    date.ExtentOfMaterial += " сторінок";
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
    }
}
