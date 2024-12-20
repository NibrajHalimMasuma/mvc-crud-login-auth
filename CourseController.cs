using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDBeAcademy.Data;
using IDBeAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IDBeAcademy.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Courses.Include(c => c.TSP).Include(c => c.Trainer);
            return View(await applicationDbContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.TSP)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

       
        public IActionResult Create()
        {
            ViewData["TspId"] = new SelectList(_context.TSPs, "TspId", "TspId");
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "TrainerId");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Title,Description,TspId,TrainerId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TspId"] = new SelectList(_context.TSPs, "TspId", "TspId", course.TspId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "TrainerId", course.TrainerId);
            return View(course);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.TSP)   // Ensure TSP is included to access TSP data
                .Include(c => c.Trainer) // Ensure Trainer is included to access Trainer data
                .FirstOrDefaultAsync(m => m.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            // Update SelectList to use Trainer Name and TSP Name
            ViewData["TspId"] = new SelectList(_context.TSPs, "TspId", "Name", course.TspId);  // Assuming 'Name' is the TSP name
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "Name", course.TrainerId);  // Assuming 'Name' is the Trainer name

            return View(course); // Pass the course model to the view
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Description,TspId,TrainerId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
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
            ViewData["TspId"] = new SelectList(_context.TSPs, "TspId", "TspId", course.TspId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "TrainerId", course.TrainerId);
            return View(course);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.TSP)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
