using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuLiLoan.BLL
{
    public class BBSProcess
    {
        /// <summary>
        /// 发布问题
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="title"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static bool PublishArticle(string userName, string title, string details)
        {
            try
            {
                var db = new HuLiLoanDBEntities();
                var dt = DateTime.Now;
                var article = new Article
                {
                    UserName = userName,
                    Title = title,
                    Details = details,
                    PubTime = dt
                };
                db.Article.Add(article);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }

        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static bool PublishComment(string userName, string details)
        {
            try
            {
                var db = new HuLiLoanDBEntities();
                var dt = DateTime.Now;
                var comment = new Comment
                {
                    UserName = userName,
                    Details = details,
                    PubTime = dt
                };
                db.Comment.Add(comment);
                db.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                throw;
                return false;
            }
        }


        /// <summary>
        /// 通过页数page获取文章
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetArticleJsonByPage(int page,int count)
        {
            using (var db = new HuLiLoanDBEntities())
            {
                var lstArticle = new List<Article>();
                var query = (from item in db.Article
                                orderby item.Aid
                                select new
                                {
                                    Aid = item.Aid,
                                    UserName = item.UserName,
                                    Title = item.Title,
                                    Details = item.Details,
                                    PubTime = item.PubTime
                                }).Skip(count * (page - 1)).Take(count).ToList();

                //var jsonLoan = Newtonsoft.Json.JsonConvert.SerializeObject(lstLoan);
                foreach (var item in query)
                {
                    lstArticle.Add(new Article
                    {
                        Aid = item.Aid,
                        UserName = item.UserName,
                        Title = item.Title,
                        Details = item.Details,
                        PubTime = item.PubTime
                    });
                }
                var jsonArticle = new JObject(
                         new JProperty("total", lstArticle.Count),
                         new JProperty("rows",
                                 new JArray(
                                     from p in lstArticle
                                     select new JObject(
                                         new JProperty("Aid", p.Aid),
                                         new JProperty("UserName", p.UserName),
                                         new JProperty("Title", p.Title),
                                         new JProperty("Details", p.Details),
                                         new JProperty("PubTime", p.PubTime)
                                         )))).ToString();
                return jsonArticle;
            }
        }

        /// <summary>
        /// 获取文章总条数
        /// </summary>
        /// <returns></returns>
        public static int GetAllArticleCount()
        {
            using (var db = new HuLiLoanDBEntities())
            {
                return db.Article.Count();
            }
        }
           



    }
}
