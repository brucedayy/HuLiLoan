using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuLiLoan.BLL
{
    public static class CheckLoanUserProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string userName, string password)
        {
            try
            {
                HuLiLoanDBEntities db = new HuLiLoanDBEntities();
                var set = db.Set<LoanCheckUser>();
                var result = from u in set.ToList()
                             where u.UserName == userName && u.Password == password
                             select u;
                if (result.Count() == 0)
                    return false;
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
