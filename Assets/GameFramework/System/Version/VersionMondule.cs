using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using AssetBundles;

namespace GameFramework
{
    internal class VersionMondule : GameFrameModule
    {
        /// <summary>PersistentDataPath AB资源版本信息</summary>
        private ABResInfo persistentABRes;
        /// <summary>StreamingAssets AB资源版本信息</summary>
        private ABResInfo streamingABRes;

        internal override int Priority => ModulePriority.VersionMondule;

        internal override async Task Initialize()
        {
            if (IsInitialize)
                return;
#if !UNITY_EDITOR
            string abFiles = AppSetting.PersistentDataURL + AppSetting.ABFiles;
            //Debug.Log("URL"+ abFiles);
            UnityWebRequest request = UnityWebRequest.Get(abFiles);
            await request.SendWebRequest();
            if (request.error == null)
            {
                string[] line = request.downloadHandler.text.Split('\n');
                persistentABRes = new ABResInfo(request.downloadHandler.text);
            }
            else
            {
                await loadStreamingABFiles();
                persistentABRes = streamingABRes;
            }
            if (persistentABRes == null)
                return;
            foreach (ABResFile file in persistentABRes.dicFileInfo.Values)
            {
                if (file.isStreaming)
                    AssetBundleManager.assetBundleURL.Add(file.File, AppSetting.StreamingAssetsURL + file.File);
                else
                    AssetBundleManager.assetBundleURL.Add(file.File, AppSetting.PersistentDataURL + file.File);
            }
#else

            await new WaitForEndOfFrame();
#endif
            IsInitialize = true;
            CLog.Log("初始化VersionMondule完成");
        }

        private async Task loadStreamingABFiles()
        {
            string abFiles = AppSetting.StreamingAssetsURL + AppSetting.ABFiles;
            Debug.Log("StreamingABFiles" + abFiles);
            UnityWebRequest request = UnityWebRequest.Get(abFiles);
            await request.SendWebRequest();
            if (request.error == null)
            {
                string[] line = request.downloadHandler.text.Split('\n');
                streamingABRes = new ABResInfo(request.downloadHandler.text, true);
            }
        }

        internal override void Shutdown()
        {
            IsInitialize = false;
        }

        internal override void Start()
        {
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }
    }
}
