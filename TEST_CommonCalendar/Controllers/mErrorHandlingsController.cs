using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEST_CommonCalendar.Data;
using TEST_CommonCalendar.Models;

namespace TEST_CommonCalendar.Controllers
{
    public class mErrorHandlingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public mErrorHandlingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: mErrorHandlings
        public async Task<IActionResult> Index()
        {
            return View(await _context.mErrorHandling.ToListAsync());
        }

        // GET: mErrorHandlings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mErrorHandling = await _context.mErrorHandling
                .FirstOrDefaultAsync(m => m.k_ErrorID == id);
            if (mErrorHandling == null)
            {
                return NotFound();
            }

            return View(mErrorHandling);
        }

        // GET: mErrorHandlings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: mErrorHandlings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("k_ErrorID,s_ClassName,s_FunctionName,s_ExceptionText,d_OccurTime")] mErrorHandling mErrorHandling)
        {
            if (ModelState.IsValid)
            {
                mErrorHandling.k_ErrorID = Guid.NewGuid();
                _context.Add(mErrorHandling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mErrorHandling);
        }

        // GET: mErrorHandlings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mErrorHandling = await _context.mErrorHandling.FindAsync(id);
            if (mErrorHandling == null)
            {
                return NotFound();
            }
            return View(mErrorHandling);
        }

        // POST: mErrorHandlings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("k_ErrorID,s_ClassName,s_FunctionName,s_ExceptionText,d_OccurTime")] mErrorHandling mErrorHandling)
        {
            if (id != mErrorHandling.k_ErrorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mErrorHandling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mErrorHandlingExists(mErrorHandling.k_ErrorID))
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
            return View(mErrorHandling);
        }

        // GET: mErrorHandlings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mErrorHandling = await _context.mErrorHandling
                .FirstOrDefaultAsync(m => m.k_ErrorID == id);
            if (mErrorHandling == null)
            {
                return NotFound();
            }

            return View(mErrorHandling);
        }

        // POST: mErrorHandlings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mErrorHandling = await _context.mErrorHandling.FindAsync(id);
            _context.mErrorHandling.Remove(mErrorHandling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mErrorHandlingExists(Guid id)
        {
            return _context.mErrorHandling.Any(e => e.k_ErrorID == id);
        }
    }
}
