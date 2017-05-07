using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuLiLoan.BLL
{
    public class LoanProcess
    {

        /// <summary>
        /// 添加一条贷款
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="IdCard"></param>
        /// <param name="cellPhone"></param>
        /// <param name="address"></param>
        /// <param name="idImgPlus"></param>
        /// <param name="idImgMinus"></param>
        /// <param name="idImgHand"></param>
        /// <param name="applyTable"></param>
        /// <returns></returns>
        public static bool AddLoan(string userName, string name,
            string IdCard, string cellPhone, string address, string idImgPlus,
            string idImgMinus, string idImgHand, string applyTable)
        {
            try
            {
                var db = new HuLiLoanDBEntities();
                var loan = new Loan
                {
                    UserName = userName,
                    Name = name,
                    IdCard = IdCard,
                    CellPhone = cellPhone,
                    Address = address,
                    IdImgPlus = idImgPlus,
                    IdImgMinus = idImgMinus,
                    IdImgHand = idImgHand,
                    ApplyTable = applyTable,
                    Status = false
                };
                db.Loan.Add(loan);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 获取所有贷款Json格式
        /// </summary>
        /// <returns></returns>
        public static string GetLoanJson()
        {
            using (var db = new HuLiLoanDBEntities())
            {
                List<Loan> lstLoan = new List<Loan>();
                var query = from item in db.Loan
                            select new
                            {
                                Lid = item.Lid,
                                UserName = item.UserName,
                                Name = item.Name,
                                IdCard = item.IdCard,
                                CellPhone = item.CellPhone,
                                Address = item.Address,
                                IdImgPlus = item.IdImgPlus,
                                IdImgMinus = item.IdImgMinus,
                                IdImgHand = item.IdImgHand,
                                ApplyTable = item.ApplyTable,
                                Status = item.Status
                            };

                //var jsonLoan = Newtonsoft.Json.JsonConvert.SerializeObject(lstLoan);
                foreach (var item in query)
                {
                    lstLoan.Add(new Loan
                    {
                        Lid = item.Lid,
                        UserName = item.UserName,
                        Name = item.Name,
                        IdCard = item.IdCard,
                        CellPhone = item.CellPhone,
                        Address = item.Address,
                        IdImgPlus = item.IdImgPlus,
                        IdImgMinus = item.IdImgMinus,
                        IdImgHand = item.IdImgHand,
                        ApplyTable = item.ApplyTable,
                        Status = item.Status
                    });
                }
                var jsonLoan = new JObject(
                         new JProperty("total", lstLoan.Count),
                         new JProperty("rows",
                                 new JArray(
                                     from p in lstLoan
                                     select new JObject(
                                         new JProperty("Lid", p.Lid),
                                         new JProperty("Name", p.Name),
                                         new JProperty("IdCard", p.IdCard),
                                         new JProperty("CellPhone", p.CellPhone),
                                         new JProperty("Address", p.Address),
                                         new JProperty("IdImgPlus", p.IdImgPlus),
                                         new JProperty("IdImgMinus", p.IdImgMinus),
                                         new JProperty("IdImgHand", p.IdImgHand),
                                         new JProperty("ApplyTable", p.ApplyTable),
                                         new JProperty("Status", p.Status == true ? "通过" : "未审核"),
                                         new JProperty("Details", "<a href=\"javascript: void(0)\" class=\"easyui-linkbutton\" onclick=\"getDetails()\">详情</a>")
                                         )))).ToString();
                return jsonLoan;
            }
        }


        /// <summary>
        /// 获取所有贷款Json格式
        /// </summary>
        /// <returns></returns>
        public static string GetUnCheckedLoanJson()
        {
            using (var db = new HuLiLoanDBEntities())
            {
                List<Loan> lstLoan = new List<Loan>();
                var query = from item in db.Loan
                            where item.Status == false
                            select new
                            {
                                Lid = item.Lid,
                                UserName = item.UserName,
                                Name = item.Name,
                                IdCard = item.IdCard,
                                CellPhone = item.CellPhone,
                                Address = item.Address,
                                IdImgPlus = item.IdImgPlus,
                                IdImgMinus = item.IdImgMinus,
                                IdImgHand = item.IdImgHand,
                                ApplyTable = item.ApplyTable,
                                Status = item.Status
                            };

                //var jsonLoan = Newtonsoft.Json.JsonConvert.SerializeObject(lstLoan);
                foreach (var item in query)
                {
                    lstLoan.Add(new Loan
                    {
                        Lid = item.Lid,
                        UserName = item.UserName,
                        Name = item.Name,
                        IdCard = item.IdCard,
                        CellPhone = item.CellPhone,
                        Address = item.Address,
                        IdImgPlus = item.IdImgPlus,
                        IdImgMinus = item.IdImgMinus,
                        IdImgHand = item.IdImgHand,
                        ApplyTable = item.ApplyTable,
                        Status = item.Status
                    });
                }
                var jsonLoan = new JObject(
                         new JProperty("total", lstLoan.Count),
                         new JProperty("rows",
                                 new JArray(
                                     from p in lstLoan
                                     select new JObject(
                                         new JProperty("Lid", p.Lid),
                                         new JProperty("UserName", p.UserName),
                                         new JProperty("Name", p.Name),
                                         new JProperty("IdCard", p.IdCard),
                                         new JProperty("CellPhone", p.CellPhone),
                                         new JProperty("Address", p.Address),
                                         new JProperty("IdImgPlus", p.IdImgPlus),
                                         new JProperty("IdImgMinus", p.IdImgMinus),
                                         new JProperty("IdImgHand", p.IdImgHand),
                                         new JProperty("ApplyTable", p.ApplyTable),
                                         new JProperty("Status", p.Status == true ? "通过" : "未审核"),
                                         new JProperty("Details", "<a href=\"javascript: void(0)\" class=\"easyui-linkbutton\" onclick=\"getDetails()\">详情</a>")
                                         )))).ToString();
                return jsonLoan;
            }
        }


        /// <summary>
        /// 批准贷款申请，通过项目的Lid获取
        /// </summary>
        /// <param name="lid"></param>
        /// <returns></returns>
        public static bool ApproveLoanApply(short lid)
        {
            try
            {
                var db = new HuLiLoanDBEntities();
                var loan = db.Loan.Where(p => p.Lid == lid).FirstOrDefault();
                if (loan != null)
                {
                    loan.Status = true;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


    }
}
