using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using shift_management.Models;
using Microsoft.EntityFrameworkCore;

namespace shift_management.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var enrollments = _context.Enrollments.Include(e => e.Worker).Include(e => e.Shift);
            return View(await enrollments.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "Task");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerId,ShiftId,EnrollmentDate,Confirmation")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "Task", enrollment.ShiftId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "WorkerId", "Name", enrollment.WorkerId);
            return View(enrollment);
        }
    }

}
