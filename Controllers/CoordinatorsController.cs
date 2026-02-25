using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerInitiativesSystem.Data;
using VolunteerInitiativesSystem.Models;

namespace VolunteerInitiativesSystem.Controllers
{
    public class CoordinatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordinatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coordinators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coordinators.ToListAsync());
        }

        // GET: Coordinators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coordinators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coordinator coordinator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordinator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordinator);
        }
    }
}