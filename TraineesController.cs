using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDBeAcademy.Data;
using IDBeAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IDBeAcademy.Controllers
{
    public class TraineesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TraineesController(ApplicationDbContext context)
        {
            _context = context;
        }

       [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trainees.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TraineeId,FirstName,LastName,Email,Password,EnrollmentDate")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainee);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }
            return View(trainee);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TraineeId,FirstName,LastName,Email,Password,EnrollmentDate")] Trainee trainee)
        {
            if (id != trainee.TraineeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraineeExists(trainee.TraineeId))
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
            return View(trainee);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee != null)
            {
                _context.Trainees.Remove(trainee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TraineeExists(int id)
        {
            return _context.Trainees.Any(e => e.TraineeId == id);
        }
    }
}
