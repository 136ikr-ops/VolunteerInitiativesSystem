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

        // ==============================
        // INDEX
        // ==============================
        public async Task<IActionResult> Index()
        {
            var initiatives = await _context.Initiatives
                .Include(i => i.Coordinator)
                .ToListAsync();

            return View(initiatives);
        }

        // ==============================
        // DETAILS
        // ==============================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var initiative = await _context.Initiatives
                .Include(i => i.Coordinator)
                .Include(i => i.Registrations)
                    .ThenInclude(r => r.Participant)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (initiative == null)
                return NotFound();

            return View(initiative);
        }

        // ==============================
        // CREATE
        // ==============================
        public IActionResult Create()
        {
            ViewBag.Coordinators = _context.Coordinators.ToList();
            return View();
        }

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

        // ==============================
        // EDIT
        // ==============================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var initiative = await _context.Initiatives.FindAsync(id);
            if (initiative == null)
                return NotFound();

            ViewBag.Coordinators = _context.Coordinators.ToList();
            return View(initiative);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Initiative initiative)
        {
            if (id != initiative.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(initiative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Coordinators = _context.Coordinators.ToList();
            return View(initiative);
        }

        // ==============================
        // DELETE
        // ==============================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var initiative = await _context.Initiatives
                .Include(i => i.Coordinator)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (initiative == null)
                return NotFound();

            return View(initiative);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var initiative = await _context.Initiatives.FindAsync(id);

            if (initiative != null)
            {
                _context.Initiatives.Remove(initiative);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ==============================
        // REGISTER PARTICIPANT
        // ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int initiativeId, int participantId)
        {
            var initiative = await _context.Initiatives
                .Include(i => i.Registrations)
                .FirstOrDefaultAsync(i => i.Id == initiativeId);

            if (initiative == null)
                return NotFound();

            if (initiative.Date < DateTime.Now)
            {
                TempData["Error"] = "Инициативата вече е минала.";
                return RedirectToAction("Details", new { id = initiativeId });
            }

            bool alreadyRegistered = await _context.InitiativeParticipants
                .AnyAsync(ip => ip.InitiativeId == initiativeId &&
                                ip.ParticipantId == participantId);

            if (alreadyRegistered)
            {
                TempData["Error"] = "Вече сте записан за тази инициатива.";
                return RedirectToAction("Details", new { id = initiativeId });
            }

            if (initiative.MaxParticipants > 0 &&
                initiative.Registrations.Count >= initiative.MaxParticipants)
            {
                TempData["Error"] = "Няма свободни места.";
                return RedirectToAction("Details", new { id = initiativeId });
            }

            var registration = new InitiativeParticipant
            {
                InitiativeId = initiativeId,
                ParticipantId = participantId
            };

            _context.InitiativeParticipants.Add(registration);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Успешно се записахте!";
            return RedirectToAction("Details", new { id = initiativeId });
        }
    }
}