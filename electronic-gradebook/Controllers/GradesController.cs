using electronic_gradebook.Data;
using electronic_gradebook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace electronic_gradebook.Controllers
{
    [Authorize]
    public class GradesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var grades = await _context.Grades
                .Include(grade => grade.Student)
                .OrderByDescending(grade => grade.DateGiven)
                .ToListAsync();
            return View(grades);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadStudentsAsync();
            return View(new Grade());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Grade grade)
        {
            bool studentExists = await _context.Users
                .AnyAsync(user => user.Id == grade.StudentId);
            if (!studentExists)
            {
                ModelState.AddModelError(
                    nameof(grade.StudentId),
                    "Избери съществуващ потребител.");
            }
            if (!ModelState.IsValid)
            {
                await LoadStudentsAsync(grade.StudentId);
                return View(grade);
            }
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task LoadStudentsAsync(string? selectedStudentId = null)
        {
            var users = await _context.Users
                .OrderBy(user => user.Email)
                .ToListAsync();
            ViewBag.StudentId = new SelectList(
                users,
                "Id",
                "Email",
                selectedStudentId);
        }
    }
}
