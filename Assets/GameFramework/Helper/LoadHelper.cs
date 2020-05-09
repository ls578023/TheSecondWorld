
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using UObject = UnityEngine.Object;

namespace GameFramework
{
    public class LoadHelper
    {
        static int LoadId=0;

        static int GetLoadId() 
        {
            LoadId++;
            if (LoadId >= int.MaxValue)
                LoadId = 0;
            return LoadId;
        }
        /// <summary>
        /// 加载Texture
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static async Task<Texture> LoadTexture(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            assetBundleName = "Textures/" + assetBundleName + ".png";
            return await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<Texture>(assetBundleName, assetName);
        }

        /// <summary>
        /// 同步加载Texture
        /// </summary>
        /// <param name="assetBundleName">相对于Textures目录的资源包名称</param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static async void LoadTexture(string assetBundleName, Action<int, Texture> CallBack, Action<int> LoadTakId = null)
        {

            int loadID = GetLoadId();
            LoadTakId?.Invoke(loadID);
            Texture t = await LoadTexture(assetBundleName);
            CallBack?.Invoke(loadID, t);
        }

        /// <summary>
        /// 加载Sprite Textures目录下的图片  目前只能加载png图片
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static async Task<Sprite> LoadSprite(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            Sprite sprite = GameFrameEntry.GetModule<AssetbundleModule>().GetSprite(assetName);
            if (sprite != null)
                return sprite;
            assetBundleName = "Textures/" + assetBundleName + ".png";
            Texture texture=  await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<Texture>(assetBundleName, assetName);
            sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            sprite.name = texture.name;
            GameFrameEntry.GetModule<AssetbundleModule>().AddSprite(assetName, sprite);
            return sprite;
        }
        /// <summary>
        /// 加载图集中的图片 转换成Texture2D放回
        /// </summary>
        /// <param name="uiAtlas"></param>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        public static async Task<Texture2D> LoadTexture2DForAtlas(string uiAtlas, string spriteName)
        {
            Texture2D texture2D = GameFrameEntry.GetModule<AssetbundleModule>().GetTexture2D(spriteName);
            if (texture2D != null)
                return texture2D;
            SpriteAtlas atlas = await GameFrameEntry.GetModule<AssetbundleModule>().LoadSpriteAtlas(uiAtlas);
            if (atlas == null) return null;
            var loadSprite = atlas.GetSprite(spriteName);
            texture2D  = new Texture2D((int)loadSprite.rect.width, (int)loadSprite.rect.height);
            var pixels = loadSprite.texture.GetPixels(
                (int)loadSprite.textureRect.x,
                (int)loadSprite.textureRect.y,
                (int)loadSprite.textureRect.width,
                (int)loadSprite.textureRect.height);
            texture2D.SetPixels(pixels);
            texture2D.Apply();
            GameFrameEntry.GetModule<AssetbundleModule>().AddGetTexture2D(spriteName, texture2D);
            return texture2D;

        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="isAdditive"></param>
        /// <param name="cbProgress"></param>
        /// <returns></returns>
        public static async Task LoadScene(string sceneName, bool isAdditive = false, Action<float> cbProgress = null)
        {
            await GameFrameEntry.GetModule<AssetbundleModule>().LoadScene(sceneName, isAdditive, cbProgress);
            //GuideTrigger.LoadScene(sceneName);
        }
        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="CallBack"></param>
        /// <param name="LoadTakId"></param>
        /// <param name="isAdditive"></param>
        /// <param name="cbProgress"></param>
        /// <returns></returns>
        public static async void LoadScene(string sceneName, Action<int> CallBack, Action<int> LoadTakId = null, bool isAdditive = false, Action<float> cbProgress = null)
        {
            int loadID = GetLoadId();
            LoadTakId?.Invoke(loadID);
            await LoadScene(sceneName, isAdditive, cbProgress);
            CallBack?.Invoke(loadID);
            //GuideTrigger.LoadScene(sceneName);
        }
        /// <summary>
        /// 加载预设文件夹下的预设
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static async Task<GameObject> LoadPrefab(string PrefabsName) 
        {
            PrefabsName = "Prefabs/" + PrefabsName;
            return await GameFrameEntry.GetModule<AssetbundleModule>().LoadPrefab(PrefabsName);
        }

        /// <summary>
        /// 同步加载预设文件夹下的预设
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static async void LoadPrefab(string PrefabsName, Action<int,GameObject> CallBack, Action<int> LoadTakId = null)
        {

            int loadID = GetLoadId();
            LoadTakId?.Invoke(loadID);
            GameObject t = await LoadPrefab(PrefabsName);
            CallBack?.Invoke(loadID,t);
        }



        /// <summary>
        /// 异步加载Model文件夹下的预设
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static async Task<GameObject> LoadModelPrefab(string PrefabsName)
        {
            PrefabsName = "Model/" + PrefabsName;
            return await GameFrameEntry.GetModule<AssetbundleModule>().LoadPrefab(PrefabsName);
        }

        /// <summary>
        /// 同步加载Model文件夹下的预设
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static async void LoadModelPrefab(string PrefabsName, Action<int,GameObject> CallBack, Action<int> LoadTakId = null)
        {
            int loadID = GetLoadId();
            LoadTakId?.Invoke(loadID);
            GameObject t = await LoadModelPrefab(PrefabsName);
            CallBack?.Invoke(loadID,t);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetBundleName">完整资源包名,注：带文件扩展名</param>
        /// <param name="assetName">资源名</param>
        public static async Task<T> LoadAsset<T>(string assetBundleName, string assetName=null) where T : UObject
        {
            //assetBundleName = assetBundleName.ToLower();
            //assetBundleName += AppSetting.ExtName;
            if (string.IsNullOrEmpty(assetName))
            {
                string assetFullName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
                assetName = assetFullName.Split('.')[0];
            }
            return await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<T>(assetBundleName, assetName);
        }


        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="assetBundleName">完整资源包名,注：带文件扩展名</param>
        /// <param name="assetName">资源名</param>
        public static async void LoadAsset<T>(string assetBundleName, Action<int,T> CallBack, Action<int> LoadTakId=null, string assetName = null) where T : UObject
        {
            int loadID = GetLoadId();
            LoadTakId?.Invoke(loadID);
            T t = await LoadAsset<T>(assetBundleName, assetName);
            CallBack?.Invoke(loadID,t);
        }



        /// <summary>
        /// 加载声音文件
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
        public static async Task<AudioClip> LoadSound(string assetBundleName)
        {
            string assetNameByHZ = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1);
            string[] AudioNameS = assetNameByHZ.Split('.');//去掉文件后缀
            string assetName = AudioNameS[0];
            return await GameFrameEntry.GetModule<AssetbundleModule>().LoadAsset<AudioClip>(assetBundleName, assetName);
        }
        
    }
}
