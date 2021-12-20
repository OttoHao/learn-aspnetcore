using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Moives
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moive.ToListAsync());
        }

        // GET: Moives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moive = await _context.Moive
                .FirstOrDefaultAsync(m => m.ID == id);
            if (moive == null)
            {
                return NotFound();
            }

            return View(moive);
        }

        // GET: Moives/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moives/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Moive moive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moive);
        }

        // GET: Moives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moive = await _context.Moive.FindAsync(id);
            if (moive == null)
            {
                return NotFound();
            }
            return View(moive);
        }

        // POST: Moives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Moive moive)
        {
            if (id != moive.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoiveExists(moive.ID))
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
            return View(moive);
        }

        // GET: Moives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moive = await _context.Moive
                .FirstOrDefaultAsync(m => m.ID == id);
            if (moive == null)
            {
                return NotFound();
            }

            return View(moive);
        }

        // POST: Moives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moive = await _context.Moive.FindAsync(id);
            _context.Moive.Remove(moive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoiveExists(int id)
        {
            return _context.Moive.Any(e => e.ID == id);
        }
    }
}
