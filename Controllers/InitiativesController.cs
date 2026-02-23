using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolunteerInitiativesSystem.Data;
using VolunteerInitiativesSystem.Models;

namespace VolunteerInitiativesSystem.Controllers
{
    public class InitiativesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InitiativesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var initiatives = await _context.Initiatives
                .Include(i => i.Coordinator)
                .ToListAsync();

            return View(initiatives);
        }

        // GET: Initiatives/Create
        public IActionResult Create()
        {
            ViewBag.Coordinators = _context.Coordinators.ToList();
            return View();
        }

        // POST: Initiatives/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Initiative initiative)
        {
            if (ModelState.IsValid)
            {
                _context.Add(initiative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Coordinators = _context.Coordinators.ToList();
            return View(initiative);
        }
    }
}