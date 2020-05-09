using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using GameFramework;


namespace GameFramework
{
    public class ConfigRead
    {
        private static int loadCount = 0; //加载资源数
        private static int loadedCount = 0; //已经加载资源数

        /// <summary>
        /// 配置表资源文件
        /// </summary>
        public static string configAssetbundle = AppSetting.ConfigBundleDir.TrimEnd('/');
        /// <summary>
        /// 读取配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static async void readConfig<T>(Dictionary<object, T> source) where T : BaseConfig
        {
            loadCount += 1;
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                for (int i = 0; i < list.Count; i++)
                {
                    if (source.ContainsKey(list[i].UniqueID))
                        CLog.Error($"表[{fileName}]中有相同键({list[i].UniqueID})");
                    else
                        source.Add(list[i].UniqueID, list[i]);
                }
            }
            else
            {
                CLog.Error($"配置文件不存在{fileName}");
            }
            loadedCount += 1;
        }

        /// <summary>
        /// 重新加载配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static async Task ReloadConfig<T>(Dictionary<object, T> source) where T : BaseConfig
        {
            string fileName = typeof(T).Name;
            source.Clear();
            UnityEngine.Object configObj = await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                for (int i = 0; i < list.Count; i++)
                {
                    if (source.ContainsKey(list[i].UniqueID))
                        CLog.Error($"表[{fileName}]中有相同键({list[i].UniqueID})");
                    else
                        source.Add(list[i].UniqueID, list[i]);
                }
            }
            else
            {
                CLog.Error($"配置文件不存在{fileName}");
            }
        }



        /// <summary>
        /// 读取竖表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static async Task<T> readConfigV<T>() where T : BaseConfig
        {
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<UnityEngine.Object>(configAssetbundle, fileName);
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                if (list.Count > 0)
                    return list[0];
            }
            else
            {
                CLog.Error($"配置文件不存在{fileName}");
            }
            return null;
        }

        public static async Task waitLoadComplate()
        {
            await new WaitUntil(() => { return loadCount == loadedCount; });
        }
    }
}

