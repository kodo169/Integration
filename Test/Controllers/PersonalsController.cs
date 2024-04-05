using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Data;

namespace Test.Controllers
{
    public class PersonalsController : Controller
    {
        private readonly HrContext _context;

        public PersonalsController(HrContext context)
        {
            _context = context;
        }
        // GET: Personals
        public async Task<IActionResult> Index()
        {
            var hrContext = _context.Personals.Include(p => p.BenefitPlansNavigation);
            return View(await hrContext.ToListAsync());
        }

        // GET: Personals/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.Personals
                .Include(p => p.BenefitPlansNavigation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // GET: Personals/Create
        public IActionResult Create()
        {
            ViewData["BenefitPlans"] = new SelectList(_context.BenefitPlans, "BenefitPlanId", "BenefitPlanId");
            return View();
        }

        // POST: Personals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,MiddleInitial,Address1,Address2,City,State,Zip,Email,PhoneNumber,SocialSecurityNumber,DriversLicense,MaritalStatus,Gender,ShareholderStatus,BenefitPlans,Ethnicity")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            ViewData["BenefitPlans"] = new SelectList(_context.BenefitPlans, "BenefitPlanId", "BenefitPlanId", personal.BenefitPlans);
            return View(personal);

        }

        // GET: Personals/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            ViewData["BenefitPlans"] = new SelectList(_context.BenefitPlans, "BenefitPlanId", "BenefitPlanId", personal.BenefitPlans);
            return View(personal);
        }

        // POST: Personals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("EmployeeId,FirstName,LastName,MiddleInitial,Address1,Address2,City,State,Zip,Email,PhoneNumber,SocialSecurityNumber,DriversLicense,MaritalStatus,Gender,ShareholderStatus,BenefitPlans,Ethnicity")] Personal personal)
        {
            if (id != personal.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalExists(personal.EmployeeId))
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
            ViewData["BenefitPlans"] = new SelectList(_context.BenefitPlans, "BenefitPlanId", "BenefitPlanId", personal.BenefitPlans);
            return View(personal);
        }

        // GET: Personals/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personal = await _context.Personals
                .Include(p => p.BenefitPlansNavigation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        // POST: Personals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var personal = await _context.Personals.FindAsync(id);
            if (personal != null)
            {
                _context.Personals.Remove(personal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalExists(decimal id)
        {
            return _context.Personals.Any(e => e.EmployeeId == id);
        }
    }
}
