using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameFramework.Tools
{
    public class StringTools
    {
        static private StringTools _instance;

        public static StringTools Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StringTools();
                }
                return _instance;
            }
        }
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="c">截取字符</param>
        /// <param name="cIndex">从c出现的的次数索引开始截 -1不截取, 0第一次,1第二次截</param>
        /// <returns></returns>
        public static string SubstringIndexOf(string str, char c, int cIndex)
        {
            if (cIndex < 0)
                return str;
            int sum = 0;
            int lastIndex = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    if (cIndex == sum)
                        return str.Remove(i);
                    sum++;
                    lastIndex = i;
                }
            }
            if (lastIndex == -1)
                return str;
            return str.Remove(lastIndex);
        }
        /// <summary>
        /// 获取第一个匹配
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        static public string GetFirstMatch(string str, string regexStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(regexStr))
            {
                return null;
            }
            Match m = Regex.Match(str, regexStr);
            if (!string.IsNullOrEmpty(m.ToString()))
            {
                return m.ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取所有匹配,返回string[]
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        static public string[] GetAllMatchs(string str, string regexStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(regexStr))
            {
                return null;
            }
            MatchCollection mc = Regex.Matches(str, regexStr);
            if (mc.Count == 0)
            {
                return null;
            }
            string[] matchs = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                matchs[i] = mc[i].Value.ToString();
            }
            return matchs;
        }
        /// <summary>
        /// 获取所有匹配,返回List<string>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regexStr"></param>
        /// <returns></returns>
        static public List<string> GetAllMatchs2(string str, string regexStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(regexStr))
            {
                return null;
            }
            MatchCollection mc = Regex.Matches(str, regexStr);
            if (mc.Count == 0)
            {
                return null;
            }
            List<string> matchs = new List<string>();
            for (int i = 0; i < mc.Count; i++)
            {
                matchs.Add(mc[i].Value.ToString());
            }
            return matchs;
        }

        /// <summary>
        /// 格式化字符串，替代{数字}内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        static public string Format(string str, params object[] args)
        {
            string newStr = str;
            string rexStr = @"\{[0-9]+\}";
            MatchCollection mc = Regex.Matches(str, rexStr);
            if (mc.Count > 0)
            {
                for (int i = 0; i < mc.Count; i++)
                {
                    if (i < args.Length)
                    {
                        string subStr = "{" + i + "}";//mc[i].Value.ToString();
                        newStr = newStr.Replace(subStr, args[i].ToString());
                    }

                }


            }
            return newStr;
        }
        /// <summary>
        /// 切割字符串，返回string[]
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        static public string[] Split(string str, string splitStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(splitStr))
            {
                return null;
            }
            string[] newStrs = Regex.Split(str, splitStr);
            return newStrs;
        }
        /// <summary>
        /// 切割字符串，返回List<string>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        static public List<string> Split2(string str, string splitStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(splitStr))
            {
                return null;
            }
            string[] newStrs = Regex.Split(str, splitStr);
            List<string> strs = new List<string>();

            for (int i = 0; i < newStrs.Length; i++)
            {
                strs.Add(newStrs[i]);
            }
            return strs;
        }

        static public string[] SplitStr(string str, char splitStr)
        {
            if (string.IsNullOrEmpty(str) || splitStr == null)
            {
                return null;
            }
            return str.Split(splitStr);
        }


        /// <summary>
        /// 去掉换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string RemoveReturn(string str)
        {
            return StringTools.Replace(str, "\r\n|\r|\n", "");
        }

        /// <summary>
        /// 用正则表达式替换字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        static public string Replace(string str, string str1, string str2)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return null;
            }
            Regex rgx = new Regex(str1);
            string result = rgx.Replace(str, str2);
            return result;
        }



        /// <summary>
        /// 检查版本是否需要更新
        /// </summary>
        /// <param name="wwwVer"></param>
        /// <param name="localVer"></param>
        /// <returns></returns>
        static public bool isNeedUpdata(string wwwVer, string localVer)
        {
            if (string.IsNullOrEmpty(localVer))
            {
                return true;
            }
            string[] wwwVers = wwwVer.Split('.');
            string[] localVers = localVer.Split('.');
            int len = wwwVers.Length > localVers.Length ? wwwVers.Length : localVers.Length;
            bool needUpdata = false;
            int wwwValue;
            int localValue;
            for (int i = 0; i < len; i++)
            {
                wwwValue = GetValue(wwwVers, i);
                localValue = GetValue(localVers, i);
                if (wwwValue > localValue)
                {
                    needUpdata = true;
                    break;
                }
                if (localValue > wwwValue)
                {
                    break;
                }
            }
            return needUpdata;
        }

        static private int GetValue(string[] strs, int ind)
        {
            if (strs.Length > ind)
            {
                int val = System.Int32.Parse(strs[ind]);
                return val;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获得扩展名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string GetExName(string str)
        {
            string regexStr = @"(?<=\.)[^\.]+$";
            string strs = GetFirstMatch(str, regexStr);
            return strs;
        }

        /// <summary>
        /// 移除扩展名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public string RemoveExName(string str)
        {
            string regexStr = @".+(?=\.)";
            string strs = GetFirstMatch(str, regexStr);
            if (string.IsNullOrEmpty(strs))
            {
                strs = str;
            }
            return strs;
        }

        /// <summary>
        /// 从字符串获取unicode码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public int String2Unicode(string str)
        {
            System.Text.UnicodeEncoding unicodeEncodeing = new System.Text.UnicodeEncoding();
            byte[] bs = unicodeEncodeing.GetBytes(str);
            int ts = 0;
            for (int i = 0; i < bs.Length; i++)
            {
                int sub = bs[i];
                int addSub = (int)Math.Pow(256, i) * sub;
                ts += addSub;
            }
            return ts;
        }

        /// <summary>
        /// 把字符串拆分再分别获取unicode码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public int[] String2Unicodes(string str)
        {
            int[] res = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                string subStr = str.Substring(i, 1);
                res[i] = String2Unicode(subStr);
            }
            return res;
        }

        /// <summary>
        /// 获取是否为纯数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public bool IsNumber(string expression)
        {
            string getIsNumber = @"^\-?[0-9]+(\.[0-9]*)?$";
            MatchCollection mc = Regex.Matches(expression, getIsNumber);
            if (mc.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public string ChangePathFormat(string path)
        {
            string newPath = path.Replace('\\', '/');
            return newPath;
        }
    }
}

