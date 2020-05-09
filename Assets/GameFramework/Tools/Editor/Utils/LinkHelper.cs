using AssetBundles;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 硬链接目录工具。。。支持win+mac, 需要win 7以上才有mklink命令
    /// </summary>
    public class LinkHelper
    {
        /// <summary>
        /// 删除硬链接目录
        /// </summary>
        /// <param name="linkPath"></param>
        public static void DeleteLink(string linkPath)
        {
            var os = Environment.OSVersion;
            if (os.ToString().Contains("Windows"))
            {
                ToolsHelper.ExecuteCommand(String.Format("rmdir \"{0}\"", linkPath));
            }
            else if (os.ToString().Contains("Unix"))
            {
                ToolsHelper.ExecuteCommand(String.Format("rm -Rf \"{0}\"", linkPath));
            }
            else
            {
                ToolsHelper.Error(String.Format("[SymbolLinkFolder]Error on OS: {0}", os.ToString()));
            }
        }

        public static void SymbolLinkFolder(string srcFolderPath, string targetPath)
        {
            var os = Environment.OSVersion;
            if (os.ToString().Contains("Windows"))
            {
                ToolsHelper.ExecuteCommand(String.Format("mklink /J \"{0}\" \"{1}\"", targetPath, srcFolderPath));
            }
            else if (os.ToString().Contains("Unix"))
            {
                var fullPath = Path.GetFullPath(targetPath);
                if (fullPath.EndsWith("/"))
                {
                    fullPath = fullPath.Substring(0, fullPath.Length - 1);
                    fullPath = Path.GetDirectoryName(fullPath);
                }
                ToolsHelper.ExecuteCommand(String.Format("ln -s {0} {1}", Path.GetFullPath(srcFolderPath), fullPath));
            }
            else
            {
                ToolsHelper.Error(String.Format("[SymbolLinkFolder]Error on OS: {0}", os.ToString()));
            }
        }

        /// <summary>
        /// 删除指定目录所有硬链接
        /// </summary>
        /// <param name="assetBundlesLinkPath"></param>
        public static void DeleteAllLinks(string assetBundlesLinkPath)
        {
            if (Directory.Exists(assetBundlesLinkPath))
            {
                foreach (var dirPath in Directory.GetDirectories(assetBundlesLinkPath))
                {
                    DeleteLink(dirPath);
                }
            }

        }
        /// <summary>
        /// 创建StreamingAssets链接
        /// </summary>
        public static void MkLinkStreamingAssets()
        {
            string linkPath = Application.streamingAssetsPath + "/" + AppSetting.PlatformName;
            if (IsLinkStreamingAssets)
            {
                ToolsHelper.CreateDir(Application.streamingAssetsPath);
                var exportPath = AppSetting.ExportResBaseDir + AppSetting.PlatformName;
                SymbolLinkFolder(exportPath, linkPath);
            }
            else
            {
                DeleteLink(linkPath);
            }
            AssetDatabase.Refresh();
        }

        private static bool _IsLinkStreamingAssets = false;
        /// <summary>
        /// 是否连接资源StreamingAssets
        /// </summary>
        public static bool IsLinkStreamingAssets
        {
            get
            {
                _IsLinkStreamingAssets =  EditorPrefs.GetBool("IsLinkStreamingAssets", false);
                return _IsLinkStreamingAssets;
            }
            set
            {
                _IsLinkStreamingAssets = value;
                EditorPrefs.SetBool("IsLinkStreamingAssets", value);
            }
        }
    }
}