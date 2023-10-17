using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using _258_A2_Tom_Milne.Models;

namespace _258_A2_Tom_Milne.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly A2DbContext _context;

        public ProjectTaskController(A2DbContext context)
        {
            _context = context;
            

        }

        // GET: ProjectTask
        public async Task<IActionResult> Index()
        {
            var a2DbContext = _context.ProjectTask.Include(p => p.Project);
            return View(await a2DbContext.ToListAsync());
        }

        // GET: ProjectTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectTask == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public IActionResult Create(int projectId)
        {
            var projectTask = new ProjectTask
            {
                ProjectId = projectId
            };

            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Title", projectTask.ProjectId);
            return View();
        }

        // POST: ProjectTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Date,ProjectId")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Title", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectTask == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", projectTask.ProjectId);
            return View(projectTask);
        }

        // POST: ProjectTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Date,ProjectId")] ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectTask == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectTask == null)
            {
                return Problem("Entity set 'A2DbContext.ProjectTask'  is null.");
            }
            var projectTask = await _context.ProjectTask.FindAsync(id);
            if (projectTask != null)
            {
                _context.ProjectTask.Remove(projectTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
          return (_context.ProjectTask?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
