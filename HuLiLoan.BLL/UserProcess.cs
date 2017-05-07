using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuLiLoan.BLL
{
    public class UserProcess
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Register(string userName, string password)
        {
            try
            {
                var db = new HuLiLoanDBEntities();
                var user = new User
                {
                    UserName = userName,
                    Password = password,
                    Status = false
                };
                db.User.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Login(string userName, string password)
        {
            try
            {
                HuLiLoanDBEntities db = new HuLiLoanDBEntities();
                var set = db.Set<User>();
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
