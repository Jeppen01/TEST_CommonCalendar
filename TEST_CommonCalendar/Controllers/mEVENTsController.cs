using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEST_CommonCalendar.CODE.COMMON;
using TEST_CommonCalendar.Data;
using TEST_CommonCalendar.Models;

namespace TEST_CommonCalendar.Controllers
{
    public class mEVENTsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public mEVENTsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: mEVENTs
        public async Task<IActionResult> Index()
        {
            return View(await _context.mEVENT.ToListAsync());
        }

        // GET: mEVENTs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEVENT = await _context.mEVENT
                .FirstOrDefaultAsync(m => m.k_EVENT_ID == id);
            if (mEVENT == null)
            {
                return NotFound();
            }

            return View(mEVENT);
        }

        // GET: mEVENTs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: mEVENTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("k_EVENT_ID,s_Title,s_Location,d_DateTimeFrom,d_DateTimeTo")] mEVENT mEVENT)
        {
            if (ModelState.IsValid)
            {
                mEVENT.k_EVENT_ID = Guid.NewGuid();
                _context.Add(mEVENT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mEVENT);
        }

        // POST: mEVENTs/getDailyEvents
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> getDailyEvents(DateTime d_InDate)
        {
            try
            {
                List<mEVENT> myEvents = await _context.mEVENT.Where(s => (s.d_DateTimeFrom.Date >= d_InDate.Date && s.d_DateTimeTo.Date <= d_InDate.Date) ||
                (s.d_DateTimeFrom.Date <= d_InDate.Date && s.d_DateTimeTo.Date >= d_InDate.Date)).Distinct().ToListAsync();

                List<mEVENT> returnlist = new List<mEVENT>();
                //process the dusplay dates
                foreach (mEVENT myevent in myEvents)
                {
                    mEVENT evtToReturn = new mEVENT();

                    if (myevent.d_DateTimeFrom.Date == myevent.d_DateTimeTo.Date)
                    {
                        evtToReturn.s_DailyCalHour = myevent.d_DateTimeFrom.ToString("HH:mm") + " - " + myevent.d_DateTimeTo.ToString("HH:mm");
                    }

                    if ((myevent.d_DateTimeFrom.Date != d_InDate.Date) && (myevent.d_DateTimeTo.Date == d_InDate.Date))
                    {
                        evtToReturn.s_DailyCalHour = "00:00 - " + myevent.d_DateTimeTo.ToString("HH:mm");
                    }

                    if ((myevent.d_DateTimeFrom.Date == d_InDate.Date) && (myevent.d_DateTimeTo.Date != d_InDate.Date))
                    {
                        evtToReturn.s_DailyCalHour = myevent.d_DateTimeFrom.ToString("HH:mm") + " - 00:00";
                    }

                    if ((myevent.d_DateTimeFrom.Date != d_InDate.Date) && (myevent.d_DateTimeTo.Date != d_InDate.Date))
                    {
                        evtToReturn.s_DailyCalHour = "00:00 - 00:00";
                    }

                    //display data
                    evtToReturn.k_EVENT_ID = myevent.k_EVENT_ID;
                    evtToReturn.s_Title = myevent.s_Title;
                    evtToReturn.s_Location = myevent.s_Location == null ? "" : myevent.s_Location;

                    //order by date
                    evtToReturn.d_DateTimeFrom = myevent.d_DateTimeFrom;

                    returnlist.Add(evtToReturn);
                }

                return Json(new { result = "success", l_DisplayEvents = returnlist.OrderBy(s => s.d_DateTimeFrom) });
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError(_context, this.GetType().Name, MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return Json(new { result = "failed" });
            }
        }

        // POST: mEVENTs/DeleteEvent
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(Guid gEventId)
        {
            try
            {
                mEVENT myrecord = await _context.mEVENT.FirstAsync(s => s.k_EVENT_ID == gEventId);

                _context.mEVENT.Remove(myrecord);
                await _context.SaveChangesAsync();

                return Json(new { result = "success" });
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError(_context, this.GetType().Name, MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return Json(new { result = "failed" });
            }
        }

        // POST: mEVENTs/EditEvent
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(Guid gEventId)
        {
            try
            {
                mEVENT myrecord = await _context.mEVENT.FirstAsync(s => s.k_EVENT_ID == gEventId);

                return Json(new
                {
                    result = "success",
                    stitle = myrecord.s_Title,
                    sLocation = myrecord.s_Location,
                    sEventid = myrecord.k_EVENT_ID.ToString(),
                    s_HourFrom = myrecord.d_DateTimeFrom.ToString("HH:mm"),
                    s_HourTo = myrecord.d_DateTimeTo.ToString("HH:mm"),
                    s_DateFrom = myrecord.d_DateTimeFrom.ToString("yyyy-MM-dd"),
                    s_DateTo = myrecord.d_DateTimeTo.ToString("yyyy-MM-dd")
                });
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError(_context, this.GetType().Name, MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return Json(new { result = "failed" });
            }
        }

        [HttpPost]
        // POST: mEVENTs/GetAllEvents
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAllEvents(string s_Startdate, string s_Enddate)
        {
            try
            {
                DateTime d_StartDate = COMMON_FUNCTIONS.DateTimeFromUnixTimestampSeconds(Convert.ToInt64(s_Startdate));
                DateTime d_EndDate = COMMON_FUNCTIONS.DateTimeFromUnixTimestampSeconds(Convert.ToInt64(s_Enddate));

                List<mEVENT> myevents = await _context.mEVENT.Where(s => (s.d_DateTimeFrom.Date >= d_StartDate.Date && s.d_DateTimeTo.Date <= d_EndDate.Date) ||
                (s.d_DateTimeFrom.Date >= d_StartDate.Date && s.d_DateTimeTo.Date >= d_EndDate.Date) ||
                (s.d_DateTimeFrom.Date <= d_StartDate.Date && s.d_DateTimeTo.Date <= d_EndDate.Date)).Distinct().ToListAsync();

                return Json(new { result = "success", l_Events = myevents });
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError(_context, this.GetType().Name, MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return Json(new { result = "failed" });
            }
        }

        // POST: mEVENTs/saveEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> saveEvent(DateTime d_StartDate, DateTime d_EndDate, string s_Title, string s_Location, string s_EventId)
        {
            try
            {
                mEVENT myrecord = new mEVENT();

                if (s_EventId == null)
                {
                    myrecord.k_EVENT_ID = new Guid();
                }
                else
                {
                    myrecord = await _context.mEVENT.FirstAsync(s => s.k_EVENT_ID == Guid.Parse(s_EventId));
                }

                //text data
                myrecord.s_Title = s_Title;
                myrecord.s_Location = s_Location;

                //date fields
                myrecord.d_DateTimeFrom = d_StartDate;
                myrecord.d_DateTimeTo = d_EndDate;


                if (s_EventId == null)
                {
                    await _context.mEVENT.AddAsync(myrecord);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.mEVENT.Update(myrecord);
                    await _context.SaveChangesAsync();
                }

                return Json(new { result = "success" });
            }
            catch (Exception ex)
            {
                COMMON_FUNCTIONS.storeError(_context, this.GetType().Name, MethodBase.GetCurrentMethod().ReflectedType.Name, ex.ToString());
                return Json(new { result = "failed" });
            }
        }

        // GET: mEVENTs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEVENT = await _context.mEVENT.FindAsync(id);
            if (mEVENT == null)
            {
                return NotFound();
            }
            return View(mEVENT);
        }

        // POST: mEVENTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("k_EVENT_ID,s_Title,s_Location,d_DateTimeFrom,d_DateTimeTo")] mEVENT mEVENT)
        {
            if (id != mEVENT.k_EVENT_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEVENT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mEVENTExists(mEVENT.k_EVENT_ID))
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
            return View(mEVENT);
        }

        // GET: mEVENTs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEVENT = await _context.mEVENT
                .FirstOrDefaultAsync(m => m.k_EVENT_ID == id);
            if (mEVENT == null)
            {
                return NotFound();
            }

            return View(mEVENT);
        }

        // POST: mEVENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mEVENT = await _context.mEVENT.FindAsync(id);
            _context.mEVENT.Remove(mEVENT);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mEVENTExists(Guid id)
        {
            return _context.mEVENT.Any(e => e.k_EVENT_ID == id);
        }
    }
}
