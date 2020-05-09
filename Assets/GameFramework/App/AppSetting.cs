using AssetBundles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// APP配置
    /// </summary>
    public class AppSetting
    {
       
        public const int GameFrameRate = 60;                          //(60)游戏帧频
        public const string ExtName = ".unity3d";                      //(.unity3d)素材扩展名
        public const string UIAtlasDir = "UIAtlas/";
        public const string ConfigBundleDir = "Data/Config/";       //配置文件目录(相对于BundleResDir)
        public const string ABFiles = "ABFiles.txt";                    //AB资源文件信息  资源路径|MD5值|大小
        public const string VersionFile = "Version.txt";                //版本信息文件
        public const string BusinessModuleDir = "Script/UI/";//UI文件导出位置
        /// <summary>
        /// 是否是竖版游戏
        /// </summary>
        public static bool IsPortrait;  
        /// <summary>
        /// 手机是否拥有刘海
        /// </summary>
        public static bool IsBangs;  
        /// <summary>
        /// 刘海像素是多少
        /// </summary>
        public static float BangsPixel;

        private static string mineGameName;
        public static string MineGameName {

            get
            {
                if (string.IsNullOrEmpty(mineGameName))
                {

                    string path = Application.dataPath;
                    string[] Chars = path.Split('/');
                    mineGameName = Chars[Chars.Length - 2];
                }
                return mineGameName;
            }
            set
            {
                mineGameName = value;
            }
        }
        public static MineGameModel mineGameModel = MineGameModel.None;


//        public static EPlatformType PlatformType
//        {
//            get
//            {
//#if HY_MC
//                return EPlatformType.HY_MC;
//#elif HY_GP
//                return EPlatformType.HY_GP;
//#elif HY_IOS
//                return EPlatformType.HY_IOS;
//#else
//                return EPlatformType.AccountPwd;
//#endif
//            }
        //}

        /// <summary>获取平台名称,后面可能会跟据不同渠道进行特殊处理</summary>
        public static string PlatformName
        {
            get { return AssetBundles.Utility.GetPlatformName(); }
        }

       

        /// <summary>
        /// 导出资源根目录
        /// </summary>
        public static string ExportResBaseDir
        {
            get { return Path.GetFullPath("ProtoConfig/Product/AssetBundles/").Replace("\\", "/"); }
            //get { return Path.GetFullPath("../Product/AssetBundles/").Replace("\\", "/"); }
        }

        /// <summary>
        /// 需要打包的资源目录
        /// </summary>
        public const string BundleResDir = "Assets/GameRes/BundleRes/";

        public const string BundleArtResDir = "Assets/GameRes/ArtRes/";

        public static string[] BundleArtResFolders = new string[] { "Textures", "Prefabs", "Materials" };

        /// <summary>
        /// persistentDataPath
        /// </summary>
        public static string PersistentDataPath
        {
            get { return Application.persistentDataPath + "/" + PlatformName + "/"; }
        }
        /// <summary>
        /// 工程路径
        /// </summary>
        public static string DataPath
        {
            get { return Application.dataPath + "/"; }
        }

        /// <summary>
        /// persistentDataPath
        /// </summary>
        public static string PersistentDataURL
        {
            get { return "file:///" + PersistentDataPath; }
        }

        /// <summary>
        /// StreamingAssetsURL
        /// </summary>
        public static string StreamingAssetsURL {

            get { return StreamingAssetsPath; }

        }

        /// <summary>
        /// streamingAssetsPath
        /// </summary>
        public static string StreamingAssetsPath
        {            
            get {
#if UNITY_IOS
                return "file://"+ Application.streamingAssetsPath + "/" + PlatformName + "/"+ MineGameName+"/"; 
#else
                return Application.streamingAssetsPath + "/" + PlatformName + "/" + MineGameName + "/";
#endif
            }
        }
     }
}
