using electronic_gradebook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace electronic_gradebook.Controllers
{
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
    }
}
