using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBPO.Domain.Data;
using WEBPO.Domain.Entities;

namespace WEBPO.Web.Controllers.Master
{
    public class VendorController : Controller
    {
        private readonly AppDbContext _context;

        public VendorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vendor
        public async Task<IActionResult> Index([FromQuery] string vendorCode, [FromQuery] string vendorName)
        {
            ViewBag.VendorList = new SelectList(await _context.MsVendor
                .Select(x=> new { vendorCode = x.IVsCd, vendorName = x.IVsDesc + " (" + x.IVsCd + ")" })
                .ToListAsync(), "vendorCode", "vendorName", "");

            var vnd = _context.MsVendor.AsQueryable();
            if (vendorCode + "" != "") vnd = vnd.Where(c => c.IVsCd.Contains(vendorCode));
            if (vendorName + "" != "") vnd = vnd.Where(c => c.IVsDesc.Contains(vendorName));

            return View(await vnd.ToListAsync());
        }

        public async Task OnGetAsync(string search)
        {
            await _context.MsVendor.Where(e=>e.IVsDesc.Contains(search)).ToListAsync();
        }

        // GET: Vendor/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mS_VS = await _context.MsVendor
                .FirstOrDefaultAsync(m => m.IVsCd == id);
            if (mS_VS == null)
            {
                return NotFound();
            }

            return View(mS_VS);
        }

        // GET: Vendor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IVsCd,IVsDesc,IEdiType")] MS_VS mS_VS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mS_VS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mS_VS);
        }

        // GET: Vendor/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mS_VS = await _context.MsVendor.FindAsync(id);
            if (mS_VS == null)
            {
                return NotFound();
            }
            return View(mS_VS);
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IVsCd,IVsDesc,IEdiType")] MS_VS mS_VS)
        {
            if (id != mS_VS.IVsCd)
            {
                //return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    

                    if (id != mS_VS.IVsCd)
                    {
                        var oldVendor = await _context.MsVendor.FindAsync(mS_VS.IVsCd);
                        if (oldVendor != null)
                        {
                            ViewBag.Error = "Vendor Already Exists";
                            return View(await _context.MsVendor.FindAsync(id));
                        } else {
                            _context.Add(mS_VS);
                        }
                           
 
                    }  else {
                        _context.Update(mS_VS);
                    }
                        
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MS_VSExists(mS_VS.IVsCd))
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
            return View(mS_VS);
        }

        // GET: Vendor/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mS_VS = await _context.MsVendor
                .FirstOrDefaultAsync(m => m.IVsCd == id);
            if (mS_VS == null)
            {
                return NotFound();
            }

            return View(mS_VS);
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mS_VS = await _context.MsVendor.FindAsync(id);
            _context.MsVendor.Remove(mS_VS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MS_VSExists(string id)
        {
            return _context.MsVendor.Any(e => e.IVsCd == id);
        }
    }
}
