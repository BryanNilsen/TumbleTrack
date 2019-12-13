using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TumbleTrack.Data;
using TumbleTrack.Models;

namespace TumbleTrack.Controllers
{
    public class MeetsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MeetsController(ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Meets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Meets.Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Meets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meet = await _context.Meets
                .Include(m => m.Events)
                    .ThenInclude(t => t.GymnastEvents)
                    .ThenInclude(t => t.Gymnast)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MeetId == id);
            if (meet == null)
            {
                return NotFound();
            }

            return View(meet);
        }

        // GET: Meets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Meets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetId,Date,Name,City,State,UserId")] Meet meet)
        {
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                // Get the current user
                var user = await GetCurrentUserAsync();
                meet.User = user;
                _context.Add(meet);
                await _context.SaveChangesAsync();

                int meetId = meet.MeetId;

                // Create default events for this meet
                Event bar = new Event() { MeetId = meetId, Name = "Uneven Bars" };
                Event beam = new Event() { MeetId = meetId, Name = "Balance Beam" };
                Event floor = new Event() { MeetId = meetId, Name = "Floor Exercise" };
                Event vault = new Event() { MeetId = meetId, Name = "Vault" };

                _context.AddRange(bar, beam, floor, vault);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", meet.UserId);
            return View(meet);
        }

        // GET: Meets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meet = await _context.Meets.FindAsync(id);
            if (meet == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", meet.UserId);
            return View(meet);
        }

        // POST: Meets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetId,Date,Name,City,State,UserId")] Meet meet)
        {
            if (id != meet.MeetId)
            {
                return NotFound();
            }

            ModelState.Remove("User");
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                try
                {
                    // Get the current user
                    var user = await GetCurrentUserAsync();
                    meet.User = user;
                    _context.Update(meet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetExists(meet.MeetId))
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
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", meet.UserId);
            return View(meet);
        }

        // GET: Meets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meet = await _context.Meets
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MeetId == id);
            if (meet == null)
            {
                return NotFound();
            }

            return View(meet);
        }

        // POST: Meets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meet = await _context.Meets.FindAsync(id);
            _context.Meets.Remove(meet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetExists(int id)
        {
            return _context.Meets.Any(e => e.MeetId == id);
        }
    }
}
