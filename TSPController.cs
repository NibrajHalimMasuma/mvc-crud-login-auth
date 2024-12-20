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
    public class TSPController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TSPController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.TSPs.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSP = await _context.TSPs
                .FirstOrDefaultAsync(m => m.TspId == id);
            if (tSP == null)
            {
                return NotFound();
            }

            return View(tSP);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TspId,Name,Location,Phone")] TSP tSP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tSP);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSP = await _context.TSPs.FindAsync(id);
            if (tSP == null)
            {
                return NotFound();
            }
            return View(tSP);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TspId,Name,Location,Phone")] TSP tSP)
        {
            if (id != tSP.TspId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSPExists(tSP.TspId))
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
            return View(tSP);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSP = await _context.TSPs
                .FirstOrDefaultAsync(m => m.TspId == id);
            if (tSP == null)
            {
                return NotFound();
            }

            return View(tSP);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tSP = await _context.TSPs.FindAsync(id);
            if (tSP != null)
            {
                _context.TSPs.Remove(tSP);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSPExists(int id)
        {
            return _context.TSPs.Any(e => e.TspId == id);
        }
    }
}
