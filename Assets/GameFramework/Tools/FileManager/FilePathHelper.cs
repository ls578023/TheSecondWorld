using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace GameFramework.Tools
{
    /// <summary>
    /// 针对文件路径的处理工具
    /// </summary>
    public class FilePathHelper
    {
        /// <summary>
        /// 获得包名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string GetPackName(string str)
        {
            string rexStr = @"(?<=\\)[^\\\.]+$";
            return StringTools.GetFirstMatch(str, rexStr);
        }


        /// <summary>
        /// 获得表名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string GetTableName(string str)
        {
            string rexStr = @"(?<=\\)[^\\\.]+(?=\.)";
            return StringTools.GetFirstMatch(str, rexStr);
        }

        /// <summary>
        /// 获得文件名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string GetFileName(string str)
        {
            string rexStr = @"(?<=\\)[^\\]+$|(?<=/)[^/]+$";
            return StringTools.GetFirstMatch(str, rexStr);
        }

        /// <summary>
        /// 获得扩展名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string GetExName(string str)
        {
            //return System.IO.Path.GetExtension(str);
            string rexStr = @"(?<=\\[^\\]+.)[^\\.]+$|(?<=/[^/]+.)[^/.]+$";
            return StringTools.GetFirstMatch(str, rexStr);

        }

        /// <summary>
        /// 去除扩展名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string RemoveExName(string str)
        {
            string returnStr = str;
            string rexStr = @"[^\.]+(?=\.)";
            string xStr = StringTools.GetFirstMatch(str, rexStr);
            if (!string.IsNullOrEmpty(xStr))
            {
                returnStr = xStr;
            }
            return returnStr;

        }
    }
}
