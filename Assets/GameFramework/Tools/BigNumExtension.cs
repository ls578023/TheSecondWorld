using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GameFramework
{
    /// <summary>
    /// 大数据扩展
    /// </summary>
    public static class BigNumExtension
    {
        static List<string> bigNumSymbol;

        public static void SetBigNumSymbol(Dictionary<int, string> symbol)
        {
            bigNumSymbol = symbol.Values.ToList();
        }
        /// <summary>
        /// 根据字符串创建大数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static BigInteger GetPowByString(this string str, bool AutoBy100 = true)
        {
            BigInteger bigInteger = BigInteger.One;
            BigInteger.TryParse(str, out bigInteger);
            if (AutoBy100)
            {
                return bigInteger * 100;//默认扩大100 倍
            }
            else
            {
                return bigInteger;
            }
        }
        /// <summary>
        /// 获取大数据位数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetDigit(this BigInteger input)
        {
            string str = (input / 100).ToString();
            return str.Length - 1;
        }

        /// <summary>
        /// 根据字符串创建大数据 去除小数点
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <param name="AutoBy100"></param>
        /// <returns></returns>
        public static BigInteger GetPowByString(this string str, bool split, bool AutoBy100 = true)
        {
            if (split)
            {
                if (string.IsNullOrEmpty(str))
                    return 0;
                str = str.Split('.')[0];
            }
            BigInteger bigInteger = BigInteger.One;
            BigInteger.TryParse(str, out bigInteger);
            if (AutoBy100)
            {
                return bigInteger * 100;//默认扩大100 倍
            }
            else
            {
                return bigInteger;
            }
        }

        static string E = "E";
        static string one = "1";
        /// <summary>
        /// 大数据乘单浮点数
        /// </summary>
        /// <param name="big"></param>
        /// <param name="coe"></param>
        /// <returns></returns>
        public static BigInteger MulityWithFloat(this BigInteger big, float coe)
        {
            if (float.IsInfinity(coe))
            {
                coe = float.MaxValue;
            }
            int sign = System.Math.Sign(coe);
            string coeString = coe.ToString();
            string corStr = coe.ToString();
            if (corStr.Contains(E))
            {
                corStr = coe.ToString("F");
            }
            int splitIndex;
            if (corStr.Contains(slpit))
            {
                splitIndex = corStr.IndexOf('.');
            }
            else
            {
                splitIndex = corStr.Length;
            }
            corStr = corStr.Replace(slpit, "").Replace("-", "");
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(one);
            for (int i = splitIndex; i < coeString.Length - 1; i++)
            {
                builder.Append(zero);
            }

            BigInteger tempInt = sign * BigInteger.Parse(corStr);
            big = big * tempInt;
            big /= BigInteger.Parse(builder.ToString());
            if (big == 0)
                return BigInteger.Zero;
            return big;
        }
        public static BigInteger MulityWithFloat(this BigInteger big, double coe)
        {
            if (double.IsInfinity(coe))
            {
                coe = double.MaxValue;
            }
            int sign = System.Math.Sign(coe);
            string coeString = coe.ToString();
            string corStr = coe.ToString();
            if (corStr.Contains(E))
            {
                corStr = coe.ToString("F");
            }
            int splitIndex;
            int coeLength;
            if (corStr.Contains(slpit))
            {

                coeLength = corStr.Length - 1 - corStr.IndexOf('.');
            }
            else
            {
                coeLength = 0;
            }
            corStr = corStr.Replace(slpit, "").Replace("-", "");
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(one);
            for (int i = 0; i < coeLength; i++)
            {
                builder.Append(zero);
            }
            BigInteger tempInt = sign * BigInteger.Parse(corStr);
            big = big * tempInt;
            big /= BigInteger.Parse(builder.ToString());
            if (big == 0)
                return BigInteger.Zero;
            return big;
        }
        public static BigInteger MulityWithTest(this BigInteger big, float coe)
        {
            long tempInt = (long)(coe * 10000000);
            big = big * tempInt;
            big /= 10000000;
            if (big == 0)
                return BigInteger.One;
            return big;
        }

        /// <summary>
        /// 大数除以大数，得到一个float
        /// </summary>
        /// <param name="first">分子</param>
        /// <param name="second">分母</param>
        /// <returns></returns>
        public static float Division(this BigInteger first, BigInteger second)
        {
            if (first == 0 || second == 0)
            {
                return 0;
            }
            BigInteger big = BigInteger.Divide(second * 100, first);
            string bigNum = big.ToString();
            int length = 7;
            length = bigNum.Length < length ? bigNum.Length : length;
            string bigStr = big.ToString().Substring(0, length);
            float res = 100.0f / System.Convert.ToSingle(bigStr);
            return res;
        }

        /// <summary>
        /// 大数除以单浮点数
        /// </summary>
        /// <param name="big"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static BigInteger Division(this BigInteger coe, float num)
        {
            BigInteger coeBig = coe * 100;
            BigInteger numBig = (int)(num * 100);
            BigInteger bigNum = BigInteger.Divide(coeBig, numBig);
            return bigNum;
        }

        static string slpit = ".";
        static string zero = "0";
        /// <summary>
        /// 大数据的小数次幂
        /// </summary>
        /// <param name="big"></param>
        /// <param name="pow"></param>
        /// <returns></returns>
        public static BigInteger Pow(this BigInteger big, float pow)
        {
            big /= 100;
            string bigInterStr = big.ToString();
            //系数字符串
            System.Text.StringBuilder coeStr = new System.Text.StringBuilder();

            coeStr.Append(bigInterStr[0]);
            coeStr.Append(slpit);
            for (int i = 1; i < 3; i++)
            {
                if (bigInterStr.Length <= i + 1)
                    coeStr.Append(zero);
                else
                    coeStr.Append(bigInterStr[i]);
            }
            //系数
            float coe = 1f;
            bool parse = float.TryParse(coeStr.ToString(), out coe);
            if (parse)
            {
                coe = System.Convert.ToSingle(System.Math.Pow(coe, pow));
            }
            System.Text.StringBuilder rateStr = new System.Text.StringBuilder();
            rateStr.Append(one);
            if (bigInterStr.Length > 1)
            {
                rateStr.Append(bigInterStr.Substring(1, bigInterStr.Length - 1));
            }
            BigInteger rate;
            bool bigParse = BigInteger.TryParse(rateStr.ToString(), out rate);
            double log = 1;
            if (bigParse)
            {
                log = BigInteger.Log10(rate);
                log = System.Math.Floor(log);
            }
            log *= pow;
            int logInt = (int)log;
            BigInteger BigInt = BigInteger.Pow(10 * BigInteger.One, logInt);
            double BigFloat = System.Math.Pow(10, (float)((log - logInt)));
            //(a^c) *(10^N)*(10^f)
            BigInteger ouput = BigInt.MulityWithFloat(coe * (float)BigFloat);
            ouput *= 100;
            return ouput;
        }

        /// <summary>
        /// 大数转string
        /// </summary>
        /// <returns></returns>
        public static string BigToString(this BigInteger big)
        {

            //数据是被放大了100的
            string bigStr = big.ToString();
            //Debug.Log("大数据" + bigStr);
            string result = "";
            string Fh = "";
            if (big == 0)
                return "0";
            //数据的长度
            int Length = bigStr.Length;
            if (Length < 6)
            {
                //再倒数第2位插入点号
                //如果不足2位，补位成2位
                bigStr = bigStr.PadLeft(2, '0');

                bigStr = bigStr.Insert(bigStr.Length - 2, ".");
                //Debug.Log("bigStr" + bigStr);
                //截取前4位 算上。
                //result = bigStr.PadLeft(4, '0');
                if (Length >= 4)
                {
                    result = bigStr.Substring(0, 4);
                }
                else
                {
                    result = bigStr.PadLeft(4, '0'); ;
                }
                //Debug.Log("result" + result);
                //Debug.Log("result"+ result);
            }
            else
            {
                //把扩大的倍数还原
                bigStr = (big / 100).ToString();
                Length = bigStr.Length;
                int a = Length / 3;
                int b = Length % 3;
                if (b == 0)
                {
                    a -= 1;
                }
                //由商确定符号
                int fhIndex = a - 1 >= bigNumSymbol.Count ? bigNumSymbol.Count - 1 : a - 1;
                Fh = bigNumSymbol[fhIndex];

                bigStr = bigStr.Insert(bigStr.Length - 3 * a, ".");
                result = bigStr.Substring(0, 4);

            }
            int dotIndex = result.IndexOf(".") + 1;
            bool removeZero = false;
            if (big.ToString().Length < 6)
            {
                for (int i = dotIndex; i < result.Length; i++)
                {
                    if (result[i].Equals('0'))
                    {
                        removeZero = true;
                    }
                    else
                    {
                        removeZero = false;
                    }
                }
            }
            if (removeZero)
            {
                result = result.Substring(0, dotIndex - 1);
            }
            if (result.EndsWith("."))
                result = result.Substring(0, result.Length - 1);

            return result + "" + Fh;
        }

        /// <summary>
        /// 计算离线收益
        /// </summary>
        /// <param name="starTime">离线开始时间戳 s</param>
        /// <param name="endTime">离线结束时间戳 s</param>
        /// <param name="Speed">产能速度 /s</param>
        /// <param name="offlineAddList">各类加成区间</param>
        /// <returns></returns>
        public static BigInteger OfflineGain(int starTime, int endTime, BigInteger Speed, params OfflineAdd[] offlineAddList)
        {
            //计算出离线总收益
            BigInteger GainSum = (endTime - starTime) * Speed;
            //增益区间 与离线区间做交集
            List<int> times = new List<int>();//时间区间
            List<int> multiples = new List<int>();//时间区间对应的加成
            for (int i = 0; i < offlineAddList.Length; i++)
            {
                int time = 0;
                int tempStarTime, tempEndTime = 0;
                //排除无交集的情况
                if (offlineAddList[i].endTime <= starTime || offlineAddList[i].starTime >= endTime)
                    continue;
                //排除数据错误的情况
                if (offlineAddList[i].endTime <= offlineAddList[i].starTime)
                {
                    UnityEngine.Debug.LogWarning("离线增量收益结束时间不能小于开始时间");
                    continue;
                }
                //确定起点
                if (offlineAddList[i].starTime <= starTime)
                    tempStarTime = starTime;
                else
                    tempStarTime = offlineAddList[i].starTime;
                //确定终点
                if (offlineAddList[i].endTime > endTime)
                    tempEndTime = endTime;
                else
                    tempEndTime = offlineAddList[i].endTime;
                time = tempEndTime - tempStarTime;

                times.Add(time);
                multiples.Add(offlineAddList[i].multiple);

            }

            BigInteger AddGain = BigInteger.Zero;
            //根据时间交集获得增加的收益
            for (int i = 0; i < times.Count; i++)
            {
                AddGain += times[i] * Speed * multiples[i];
            }
            //由于加成百分比是扩大了10000倍 收益缩小10000倍
            AddGain = BigInteger.Divide(AddGain, 10000);
            return GainSum + AddGain;
        }
        /// <summary>
        /// double 相乘法
        /// </summary>
        /// <returns></returns>
        public static double MultiplyFloat(this double value, double LessValue)
        {
            return (double)(new decimal(value) * new decimal(LessValue));
        }
        /// <summary>
        /// double 减法
        /// </summary>
        /// <returns></returns>
        public static double ReductDouble(this double value, double LessValue)
        {
            return (double)(new decimal(value) - new decimal(LessValue));
        }
    }

    public class OfflineAdd
    {
        /// <summary>
        /// 开始时间戳 以秒为单位
        /// </summary>
        public int starTime;
        /// <summary>
        /// 结束时间戳 以秒为单位
        /// </summary>
        public int endTime;
        /// <summary>
        /// 加成比例 万分比  比如5% 就填 500 
        /// </summary>
        public int multiple;
    }


}