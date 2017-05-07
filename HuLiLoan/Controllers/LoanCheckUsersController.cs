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
    public class LoanCheckUsersController : Controller
    {
        private HuLiLoanDBEntities db = new HuLiLoanDBEntities();

        // GET: LoanCheckUsers
        public async Task<ActionResult> Index()
        {
            return View(await db.LoanCheckUser.ToListAsync());
        }

        // GET: LoanCheckUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanCheckUser loanCheckUser = await db.LoanCheckUser.FindAsync(id);
            if (loanCheckUser == null)
            {
                return HttpNotFound();
            }
            return View(loanCheckUser);
        }

        // GET: LoanCheckUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanCheckUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserName,Password,Status")] LoanCheckUser loanCheckUser)
        {
            if (ModelState.IsValid)
            {
                db.LoanCheckUser.Add(loanCheckUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loanCheckUser);
        }

        // GET: LoanCheckUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanCheckUser loanCheckUser = await db.LoanCheckUser.FindAsync(id);
            if (loanCheckUser == null)
            {
                return HttpNotFound();
            }
            return View(loanCheckUser);
        }

        // POST: LoanCheckUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserName,Password,Status")] LoanCheckUser loanCheckUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanCheckUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loanCheckUser);
        }

        // GET: LoanCheckUsers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanCheckUser loanCheckUser = await db.LoanCheckUser.FindAsync(id);
            if (loanCheckUser == null)
            {
                return HttpNotFound();
            }
            return View(loanCheckUser);
        }

        // POST: LoanCheckUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            LoanCheckUser loanCheckUser = await db.LoanCheckUser.FindAsync(id);
            db.LoanCheckUser.Remove(loanCheckUser);
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
