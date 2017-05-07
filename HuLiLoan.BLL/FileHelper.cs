using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HuLiLoan.BLL
{
    public class FileHelper
    {
        /// <summary>
        /// 上传贷款文件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="fi"></param>
        /// <returns></returns>
        public static bool UpLoadApplyLoanFile(string username, HttpPostedFileBase fi)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\FileUpLoad\\" + username))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\FileUpLoad\\"+username);
                    directoryInfo.Create();
                }
                fi.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "\\FileUpLoad\\" + username + "\\" +fi.FileName);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool UpLoadSummerNoteFile(string username, HttpPostedFileBase fi)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\SummerNoteFileUpLoad\\" + username))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\SummerNoteFileUpLoad\\" + username);
                    directoryInfo.Create();
                }
                fi.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "\\SummerNoteFileUpLoad\\" + username + "\\" + fi.FileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
