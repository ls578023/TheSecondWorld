using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UObject = UnityEngine.Object;
using UnityEngine.U2D;
using AssetBundles;
using System.Threading.Tasks;

namespace GameFramework
{
    /// <summary>
    /// 资源包管理器
    /// 全部资源包加载都使用异步加载
    /// </summary>
    internal class AssetbundleModule : GameFrameModule
    {

        internal override int Priority => ModulePriority.AssetbundleModule;

        Dictionary<string, Sprite> dicSprites;//Texture转Sprite集合
        Dictionary<string, Texture2D> dicTexture2D;//Sprite转Texture集合
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initOK"></param>
        internal override async Task Initialize()
        {
            if (IsInitialize)
                return;
            AssetBundleManager.logMode = AssetBundleManager.LogMode.JustErrors;
            AssetBundleManager.Initialize();
            dicSprites = new Dictionary<string, Sprite>();
            dicTexture2D = new Dictionary<string, Texture2D>();
            //AssetBundleManager.BaseDownloadingURL = ResUtil.GetRelativePath();
            AssetBundleLoadManifestOperation request = AssetBundleManager.LoadManifest(AppSetting.MineGameName);
            if (request == null) 
                return;
            await request;
            IsInitialize = true;
            CLog.Log("AssetbundleModule初始化完成");
        }
        internal override void Start()
        {
        }
        #region Texture转Sprite

        public Sprite GetSprite(string Spname)
        {
            Sprite sprite;
            dicSprites.TryGetValue(Spname, out sprite);
            return sprite;
        }
        public void AddSprite(string Spname, Sprite sprite)
        {
            if (!dicSprites.ContainsKey(Spname))
            {
                dicSprites.Add(Spname, sprite);
                return;
            }
            dicSprites[Spname] = sprite;
        }
        #endregion

        #region Sprite转Texture

        public Texture2D GetTexture2D(string Spname)
        {
            Texture2D sprite;
            dicTexture2D.TryGetValue(Spname, out sprite);
            return sprite;
        }
        public void AddGetTexture2D(string Spname, Texture2D sprite)
        {
            if (!dicTexture2D.ContainsKey(Spname))
            {
                dicTexture2D.Add(Spname, sprite);
                return;
            }
            dicTexture2D[Spname] = sprite;
        }
        #endregion
        /// <summary>
        /// 加载游戏Manifest文件
        /// </summary>
        //public async Task InitializeMineGameAssetBundle() 
        //{
        //    AssetBundleLoadManifestOperation request = AssetBundleManager.LoadManifest(AppSetting.MineGameName);

        //    CLog.Log("加载游戏资源主文件完成");
        //}

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        internal override void Shutdown()
        {
            dicSprites.Clear();
            dicTexture2D.Clear();
            AssetBundleManager.Shutdown();
            IsInitialize = false;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetBundleName">完整资源包名,注：带文件扩展名</param>
        /// <param name="assetName">资源名</param>
        public async Task<T> LoadAsset<T>(string assetBundleName, string assetName) where T : UObject
        {
            assetBundleName = assetBundleName.ToLower();
            assetBundleName += AppSetting.ExtName;
            return await _LoadAsset<T>(assetBundleName, assetName);
        }

        /// <summary>
        /// 异步预设
        /// </summary>
        /// <param name="assetBundleName">资源包名</param>
        /// <param name="assetName">资源名,不填自动获取资源包最后的名字</param>
        public async Task<GameObject> LoadPrefab(string assetBundleName, string assetName = null)
        {
            if (string.IsNullOrEmpty(assetName))
                assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName += ".prefab";
            assetBundleName = assetBundleName.ToLower();
            assetBundleName += AppSetting.ExtName;
            GameObject obj = await _LoadAsset<GameObject>(assetBundleName, assetName, true);
            GameObject rtn = GameObject.Instantiate(obj) as GameObject;
            Utils.ResetShader(rtn);
            return rtn;
        }

        /// <summary>
        /// 异步加载图集
        /// </summary>
        /// <param name="assetBundleName"></param>
        public async Task<SpriteAtlas> LoadSpriteAtlas(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = AppSetting.UIAtlasDir + assetBundleName + ".spriteatlas";            
            return await LoadAsset<SpriteAtlas>(assetBundleName, assetName);
        }
        /// <summary>
        /// 异步加载材质球
        /// </summary>
        /// <param name="assetBundleName"></param>
        public async Task<Material> LoadMaterial(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = "Materials/" + assetBundleName + ".mat";
            return await LoadAsset<Material>(assetBundleName, assetName);
        }
        
        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <param name="isAdditive">是否叠加场景</param>
        /// <param name="cbProgress">进度回调</param>
        public async Task LoadScene(string sceneName, bool isAdditive = false, Action<float> cbProgress = null)
        {
            string sceen = sceneName.Substring(sceneName.LastIndexOf("/") + 1);
            string assetBundleName = "scene/" + sceneName.ToLower() + ".unity";          
            if (!assetBundleName.EndsWith(AppSetting.ExtName))
                assetBundleName += AppSetting.ExtName;
            await _LoadScene(assetBundleName, sceen, isAdditive, cbProgress);
        }

        #region 私有协同方法
        /// <summary>
        /// 异步加载资源
        /// </summary>
        private async Task<T> _LoadAsset<T>(string assetBundleName, string assetName, bool isWait = false) where T : UObject
        {
            // Load asset from assetBundle.
            AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(T));
            if (request == null) return default(T);
            await request;

#if UNITY_EDITOR
            if (AssetBundleManager.SimulateAssetBundleInEditor)
                await waitFrame;
#endif

            T obj = request.GetAsset<T>();
            if (obj == null)
                CLog.Error($"加载资源失败:{assetBundleName}  AssName:{assetName}");
            return obj;
        }

        private WaitForEndOfFrame waitFrame =  new WaitForEndOfFrame();
        /// <summary>
        /// 异步加载场景
        /// </summary>
        private async Task _LoadScene(string sceneAssetBundle, string levelName, bool isAdditive, Action<float> cbProgress)
        {
            float startTime = Time.realtimeSinceStartup;
            AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneAssetBundle, levelName, isAdditive);
            
            if (request != null) 
            {
                while (request.Progress() < 1f)
                {
                    await waitFrame;
                    cbProgress?.Invoke(request.Progress());
                    if (request.IsDone())
                        break;
                }
                cbProgress?.Invoke(1f);
            }

            float elapsedTime = Time.realtimeSinceStartup - startTime;
            Utils.ResetShader(null);
            Debug.Log("Finished loading scene " + levelName + " in " + elapsedTime + " seconds");
            Debug.Log("当前场景 " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        }


        #endregion
    }
}
