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
    public class ViewLoanMoneysController : Controller
    {
        private HuLiLoanDBEntities db = new HuLiLoanDBEntities();

        // GET: ViewLoanMoneys
        public async Task<ActionResult> Index()
        {
            return View(await db.ViewLoanMoney.ToListAsync());
        }

        // GET: ViewLoanMoneys/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewLoanMoney viewLoanMoney = await db.ViewLoanMoney.FindAsync(id);
            if (viewLoanMoney == null)
            {
                return HttpNotFound();
            }
            return View(viewLoanMoney);
        }

        // GET: ViewLoanMoneys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewLoanMoneys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,UserName,IdCard,CellPhone,Address,Lid,Money,Approvetime,LimitTime,LoanTime,Interest,Status")] ViewLoanMoney viewLoanMoney)
        {
            if (ModelState.IsValid)
            {
                db.ViewLoanMoney.Add(viewLoanMoney);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viewLoanMoney);
        }

        // GET: ViewLoanMoneys/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewLoanMoney viewLoanMoney = await db.ViewLoanMoney.FindAsync(id);
            if (viewLoanMoney == null)
            {
                return HttpNotFound();
            }
            return View(viewLoanMoney);
        }

        // POST: ViewLoanMoneys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,UserName,IdCard,CellPhone,Address,Lid,Money,Approvetime,LimitTime,LoanTime,Interest,Status")] ViewLoanMoney viewLoanMoney)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewLoanMoney).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(viewLoanMoney);
        }

        // GET: ViewLoanMoneys/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewLoanMoney viewLoanMoney = await db.ViewLoanMoney.FindAsync(id);
            if (viewLoanMoney == null)
            {
                return HttpNotFound();
            }
            return View(viewLoanMoney);
        }

        // POST: ViewLoanMoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ViewLoanMoney viewLoanMoney = await db.ViewLoanMoney.FindAsync(id);
            db.ViewLoanMoney.Remove(viewLoanMoney);
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
