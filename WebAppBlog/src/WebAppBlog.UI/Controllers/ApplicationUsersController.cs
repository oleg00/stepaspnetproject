using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppBlog.UI.Data;
using WebAppBlog.UI.Models;

namespace WebAppBlog.UI.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.ToListAsync());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }



        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email, UserName ")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.ApplicationUser.SingleOrDefault(m => m.Id == id); ;
                    user.Email = applicationUser.Email;
                    user.UserName = applicationUser.UserName;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ToBanTheUser/5
        public async Task<IActionResult> ToBanTheUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST : ToBanTheUser/5
        [HttpPost]
        public ActionResult ToBanTheUser(string id, [ Bind("_IsBlocked") ] ApplicationUser applicationUser)
        {
            var user = _context.ApplicationUser.SingleOrDefault(m => m.Id == id); ;
            user._OldUserName = applicationUser.UserName;
            user.UserName = "[banned]";
            user._IsBlocked = true;
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET : SetRating/5
        public ActionResult SetRating(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = _context.ApplicationUser.SingleOrDefault(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST : SetRating/5
        [HttpPost]
        public ActionResult SetRating(string id, [Bind("_Rate")] ApplicationUser applicationUser)
        {
            var user = _context.ApplicationUser.SingleOrDefault(m => m.Id == id); ;
            user._Rate = applicationUser._Rate;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
