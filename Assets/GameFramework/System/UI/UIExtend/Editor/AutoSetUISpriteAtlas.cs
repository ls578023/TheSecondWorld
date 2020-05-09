
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.U2D;

namespace GameFramework
{

    /// <summary>
    /// 自动设置UI引用的SpriteAtlsas
    /// </summary>
    public class AutoSetUISpriteAtlas : UnityEditor.AssetModificationProcessor
    {
        //UI目录
        private static string UI_DIR = AppSetting.BundleResDir + "UI/";
        //UIAtlas目录
        private static string UI_ATLAS_DIR = AppSetting.BundleResDir + "UIAtlas/";

        [InitializeOnLoadMethod]
        static void StartInitializeOnLoadMethod()
        {
            //注册Apply时的回调
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject instance)
            {
                if (instance)
                    SaveUIPrefab(instance);
            };
        }
        /// <summary>
        /// 保存UI预制
        /// 自动添加引用图集依赖
        /// </summary>
        /// <param name="instance"></param>
        static void SaveUIPrefab(GameObject instance)
        {
            string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(instance));
            if (!IsUIPrefab(prefabPath))
                return;
            GameObject go = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(instance) as GameObject;
            Image[] imgs = go.GetComponentsInChildren<Image>(true);
            Dictionary<string, SpriteAtlas> saDict = new Dictionary<string, SpriteAtlas>();
            List<SpriteAtlas> saList = new List<SpriteAtlas>();
            string imgPath;
            string spriteAtlasPath;
            SpriteAtlas sa;
            foreach (Image img in imgs)
            {
                imgPath = AssetDatabase.GetAssetPath(img.sprite);
                if (imgPath.IndexOf("/UIAtlas/") == -1) continue;
                imgPath = imgPath.Substring(0, imgPath.LastIndexOf("/"));
                spriteAtlasPath = imgPath.Replace("/ArtRes/UIAtlas/", "/BundleRes/UIAtlas/") + ".spriteatlas";
                if (!saDict.TryGetValue(spriteAtlasPath, out sa))
                {
                    sa = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(spriteAtlasPath);
                    if (sa != null)
                    {
                        saDict.Add(spriteAtlasPath, sa);
                        saList.Add(sa);
                    }
                    else
                    {
                        ToolsHelper.Warning("SpriteAtlas未找到:" + spriteAtlasPath);
                    }
                }
            }
            SpriteAtlasList compAtlas = go.GetComponent<SpriteAtlasList>();
            if (saList.Count > 0)
            {
                if (compAtlas == null)
                    compAtlas = go.AddComponent<SpriteAtlasList>();
                compAtlas.AtlasList = saList.ToArray();
            }
            else
            {
                if (compAtlas != null)
                    Component.DestroyImmediate(compAtlas, true);
            }
            PrefabUtility.ResetToPrefabState(instance);
        }
        /// <summary>
        /// 是否为UI预制
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static bool IsUIPrefab(string path)
        {
            if (path.Contains(UI_DIR) && Path.GetExtension(path) == ".prefab")
                return true;
            return false;
        }

    }
}