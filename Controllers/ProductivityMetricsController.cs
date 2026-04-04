using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using varma.Models;

namespace varma.Controllers
{
    public class ProductivityMetricsController : Controller
    {
        private readonly WorkPulseDbContext _context;

        public ProductivityMetricsController(WorkPulseDbContext context)
        {
            _context = context;
        }

        // GET: ProductivityMetrics
        public async Task<IActionResult> Index()
        {
            var workPulseDbContext = _context.ProductivityMetrics.Include(p => p.Employee);
            return View(await workPulseDbContext.ToListAsync());
        }

        // GET: ProductivityMetrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productivityMetric = await _context.ProductivityMetrics
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.MetricId == id);
            if (productivityMetric == null)
            {
                return NotFound();
            }

            return View(productivityMetric);
        }

        // GET: ProductivityMetrics/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: ProductivityMetrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetricId,EmployeeId,Date,TasksCompleted,WorkHours,ProductivityScore")] ProductivityMetric productivityMetric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productivityMetric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", productivityMetric.EmployeeId);
            return View(productivityMetric);
        }

        // GET: ProductivityMetrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productivityMetric = await _context.ProductivityMetrics.FindAsync(id);
            if (productivityMetric == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", productivityMetric.EmployeeId);
            return View(productivityMetric);
        }

        // POST: ProductivityMetrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetricId,EmployeeId,Date,TasksCompleted,WorkHours,ProductivityScore")] ProductivityMetric productivityMetric)
        {
            if (id != productivityMetric.MetricId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productivityMetric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductivityMetricExists(productivityMetric.MetricId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", productivityMetric.EmployeeId);
            return View(productivityMetric);
        }

        // GET: ProductivityMetrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productivityMetric = await _context.ProductivityMetrics
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.MetricId == id);
            if (productivityMetric == null)
            {
                return NotFound();
            }

            return View(productivityMetric);
        }

        // POST: ProductivityMetrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productivityMetric = await _context.ProductivityMetrics.FindAsync(id);
            if (productivityMetric != null)
            {
                _context.ProductivityMetrics.Remove(productivityMetric);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductivityMetricExists(int id)
        {
            return _context.ProductivityMetrics.Any(e => e.MetricId == id);
        }
    }
}
