using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    public class RandomHelper
    {
        /// <summary>全局随机数</summary>
        private static readonly Random random = new Random(getRandomSeed());

        private static int getRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// 根据权重对象随机
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">随机对象列表-对象包含权重值和id</param>
        /// <param name="count">获取随机列表中的几个元素</param>
        /// <returns></returns>
        public static List<T> GetRandomWeight<T>(List<T> list, int count) where T : RandomObject
        {
            if (list == null || list.Count < count || count <= 0)
            {
                return null;
            }
            if (list.Count == count)
                return list;

            //计算权重总和
            int totalWeights = 0;
            for (int i = 0; i < list.Count; i++)
            {
                totalWeights += list[i].Weight + 1;  //权重+1，防止为0情况。
            }

            //随机赋值权重
            List<KeyValuePair<int, int>> wlist = new List<KeyValuePair<int, int>>();    //第一个int为list下标索引、第一个int为权重排序值
            for (int i = 0; i < list.Count; i++)
            {
                int w = (list[i].Weight + 1) + random.Next(0, totalWeights);   // （权重+1） + 从0到（总权重-1）的随机数
                wlist.Add(new KeyValuePair<int, int>(i, w));
            }

            //Debug.Log(wlist.Count);
            //排序
            wlist.Sort(
              delegate (KeyValuePair<int, int> kvp1, KeyValuePair<int, int> kvp2)
              {
                  return kvp2.Value - kvp1.Value;
              });
            //Debug.Log(1);
            //根据实际情况取排在最前面的几个
            List<T> newList = new List<T>();
            for (int i = 0; i < count; i++)
            {
                T entiy = list[wlist[i].Key];
                newList.Add(entiy);
            }
            //随机法则
            return newList;
        }


        /// <summary>
        /// 返回一个范围内的随机数
        /// </summary>
        /// <param name="minValue">最小值(含)</param>
        /// <param name="maxValue">最大值(含)</param>
        /// <returns></returns>
        public static int Random(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue + 1);
        }

        public static T RandomEnum<T>()where T:Enum
        {
            Array array = Enum.GetValues(typeof(T));
            int id = random.Next(array.Length);
            return (T)array.GetValue(id);
        } 

        /// <summary>
        /// 在一个范围内，随机N个不重复的整数 包含min 不包含Max
        /// </summary>
        /// <returns></returns>
        public static int[] Randoms(int Count,int minNum,int MaxNum)
        {
            if ((MaxNum - minNum) < Count)
            {
                CLog.Log($"随机次数[{Count}]大于区间值[{MaxNum - minNum}]");
                return null;
            }
            if (MaxNum < minNum)
            {
                CLog.Log($"随机区间值错误[{MaxNum}]小于[{minNum}]");
                return null;
            }

            List<int> randomList = new List<int>();
            for (int i = minNum; i < MaxNum; i++)
            {
                randomList.Add(i);
            }
            List<int> result = new List<int>();
            for (int i = 0; i < Count; i++)
            {
                int MaxIndex = randomList.Count;
                int index = Random(0, MaxIndex - 1);
                result.Add(randomList[index]);
                randomList.RemoveAt(index);
            }
            return result.ToArray();
        }

        /// <summary>
        /// 返回一个范围内的随机数
        /// </summary>
        /// <param name="range">随机范围[最小值，最大值]</param>
        /// <returns></returns>
        public static int Random(int[] range)
        {
            if (range.Length < 2)
                return range[0];
            return Random(range[0], range[1]);
        }

        /// <summary>
        /// 判断百分比随机是否命中
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool RandomPercent(int val)
        {
            return random.Next(0, 100) < val;
        }

       
        public static void Shuffle<T>(List<T> list)
        {
            int length = list.Count-1;

            while (length>1)
            {
                int index = Random(0, length - 1);
                T temp = list[length];
                list[length] = list[index];
                list[index] = temp;
                length--;
            }

        }


        /// <summary>
        /// 从列表中随机获得一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Randm<T>(List<T> list)
        {
            int randomVal = random.Next(list.Count);
            return list[randomVal];
        }

        /// <summary>
        /// 从数组中随机获得一个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Randm<T>(T[] list)
        {
            int randomVal = random.Next(list.Length);
            return list[randomVal];
        }

        /// <summary>
        /// 从数组中随机取几个不重复的
        /// 注：会修改源数据中的数据顺序，如果不想改变源数据，自行复制数据再传进来
        /// </summary>
        /// <param name="list">源数据</param>
        /// <param name="num">最多获取数量</param>
        /// <returns></returns>
        public static List<T> RandomGetNum<T>(List<T> _list, int maxNum, bool isClone = false)
        {
            List<T> list;
            if (isClone)
            {
                list = new List<T>();
                foreach (T t in _list)
                    list.Add(t);
            }
            else
                list = _list;

            List<T> rtnList = new List<T>();
            int index;
            int arrLen = list.Count;
            for (int i = 0; i < maxNum; i++)
            {
                if (arrLen == 0)
                    break;
                index = RandomHelper.Random(0, arrLen - 1);
                rtnList.Add(list[index]);
                list[index] = list[arrLen - 1];
                arrLen -= 1;
            }
            return rtnList;
        }


    }
}
