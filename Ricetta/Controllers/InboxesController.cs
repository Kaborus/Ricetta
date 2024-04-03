using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ricetta.Data;
using Ricetta.Data.Entities;

namespace Ricetta.Controllers
{
    public class InboxesController : Controller
    {
        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;
        private readonly RicettaDbContext _context;

        public InboxesController(SignInManager<Member> signInManager, UserManager<Member> userManager, RicettaDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Inboxes
        public async Task<IActionResult> Index()
        {
            var ricettaDbContext = _context.Inboxes.Where(i => i.UserId == _userManager.GetUserId(this.User) || i.ReceiverId == _userManager.GetUserId(this.User)).Include(i => i.User);
            return View(await ricettaDbContext.ToListAsync());
        }

        // GET: Inboxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbox = await _context.Inboxes
                .Include(i => i.User).Include(i => i.Receiver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inbox == null)
            {
                return NotFound();
            }

            return View(inbox);
        }

        // GET: Inboxes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Inboxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ReceiverId,Id")] Inbox inbox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inbox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", inbox.UserId);
            ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "Id", inbox.ReceiverId);
            return View(inbox);
        }

        // GET: Inboxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbox = await _context.Inboxes.FindAsync(id);
            if (inbox == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", inbox.UserId);
            return View(inbox);
        }

        // POST: Inboxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Id")] Inbox inbox)
        {
            if (id != inbox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inbox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InboxExists(inbox.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", inbox.UserId);
            return View(inbox);
        }

        // GET: Inboxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inbox = await _context.Inboxes
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inbox == null)
            {
                return NotFound();
            }

            return View(inbox);
        }

        // POST: Inboxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inbox = await _context.Inboxes.FindAsync(id);
            if (inbox != null)
            {
                _context.Inboxes.Remove(inbox);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InboxExists(int id)
        {
            return _context.Inboxes.Any(e => e.Id == id);
        }
    }
}
