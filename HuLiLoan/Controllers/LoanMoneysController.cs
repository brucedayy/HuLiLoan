using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HuLiLoan.BLL;

namespace HuLiLoan.Controllers
{
    public class LoanMoneysController : Controller
    {
        private HuLiLoanDBEntities db = new HuLiLoanDBEntities();

        // GET: LoanMoneys
        public async Task<ActionResult> Index()
        {
            var loanMoney = db.LoanMoney.Include(l => l.Loan);
            return View(await loanMoney.ToListAsync());
        }

        // GET: LoanMoneys/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanMoney loanMoney = await db.LoanMoney.FindAsync(id);
            if (loanMoney == null)
            {
                return HttpNotFound();
            }
            return View(loanMoney);
        }

        // GET: LoanMoneys/Create
        public ActionResult Create()
        {
            var lstLid = (from p in db.Loan
                          where p.Status == true
                          select p.Lid).ToList();
            ViewBag.Lid = new SelectList(lstLid, "Lid");
            return View();
        }

        // POST: LoanMoneys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lid,Money,Approvetime,LimitTime,LoanTime,Interest,Status")] LoanMoney loanMoney)
        {
            if (ModelState.IsValid)
            {
                db.LoanMoney.Add(loanMoney);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Lid = new SelectList(db.Loan, "Lid", "UserName", loanMoney.Lid);
            return View(loanMoney);
        }

        // GET: LoanMoneys/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanMoney loanMoney = await db.LoanMoney.FindAsync(id);
            if (loanMoney == null)
            {
                return HttpNotFound();
            }
            ViewBag.Lid = new SelectList(db.Loan, "Lid", "UserName", loanMoney.Lid);
            return View(loanMoney);
        }

        // POST: LoanMoneys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lid,Money,Approvetime,LimitTime,LoanTime,Interest,Status")] LoanMoney loanMoney)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanMoney).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Lid = new SelectList(db.Loan, "Lid", "UserName", loanMoney.Lid);
            return View(loanMoney);
        }

        // GET: LoanMoneys/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanMoney loanMoney = await db.LoanMoney.FindAsync(id);
            if (loanMoney == null)
            {
                return HttpNotFound();
            }
            return View(loanMoney);
        }

        // POST: LoanMoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            LoanMoney loanMoney = await db.LoanMoney.FindAsync(id);
            db.LoanMoney.Remove(loanMoney);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
