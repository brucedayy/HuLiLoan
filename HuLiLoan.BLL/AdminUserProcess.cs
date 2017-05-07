using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuLiLoan.BLL
{
    public class AdminUserProcess
    {
        public static bool Login(string userName, string password)
        {
            try
            {
                HuLiLoanDBEntities db = new HuLiLoanDBEntities();
                var set = db.Set<AdminUser>();
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
