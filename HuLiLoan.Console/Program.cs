using HuLiLoan.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuLiLoan.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试分页功能
            var query = BLL.BBSProcess.GetArticleJsonByPage(10, 9);
            System.Console.WriteLine(query);
            System.Console.Read();
        }
    }
}
