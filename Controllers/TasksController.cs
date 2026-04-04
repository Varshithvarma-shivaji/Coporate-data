using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using varma.Models;

// ✅ Alias to avoid conflict
using TaskModel = varma.Models.Task;

namespace varma.Controllers
{
    public class TasksController : Controller
    {
        private readonly WorkPulseDbContext _context;

        public TasksController(WorkPulseDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var workPulseDbContext = _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project);

            return View(await workPulseDbContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId");
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,ProjectId,EmployeeId,TaskTitle,Status,Priority,DueDate")] TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", task.ProjectId);

            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", task.ProjectId);

            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,ProjectId,EmployeeId,TaskTitle,Status,Priority,DueDate")] TaskModel task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", task.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", task.ProjectId);

            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}