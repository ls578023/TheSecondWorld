using AssetBundles;
using GameFramework.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    /// <summary>
    /// 资源打包工具
    /// </summary>
    public class ResBundleTools
    {
        /// <summary>
        /// 移除UI预设上的公有字体-黑体SIMHEI
        /// </summary>
        public static void RemoveUIPrefabTextFont()
        {
            List<string> FileName = FindUIPrefab();
            string BasePath = "Assets/GameRes/BundleRes/UI/";
            foreach (var file in FileName)
            {
                GameObject go = AssetDatabase.LoadAssetAtPath(BasePath + file, typeof(GameObject)) as GameObject;
                Text[] texts = go.GetComponentsInChildren<Text>(true);
                foreach (var item in texts)
                {
                    if (item.font!=null && item.font.name== "SIMHEI")
                    {
                        item.font = null;
                        //ToolsHelper.Log("FileName:"+ file+"Text:" + item.name+"Font:"+ item.font, false);
                    }
                }
                EditorUtility.SetDirty(go);
                AssetDatabase.SaveAssets();
            }
        }

        static List<string> FindUIPrefab() 
        {

            string SourcePath = Path.Combine(Application.dataPath, "GameRes/BundleRes/UI");
            var pattern = "*";
            var allFileInfo = Directory.GetFiles(SourcePath, pattern, SearchOption.AllDirectories);
            List<string> FileName = new List<string>();
            foreach (var item in allFileInfo)
            {
                if (item.EndsWith(".meta"))
                    continue;
                string dirname = item.Substring(item.LastIndexOf(SourcePath) + SourcePath.Length + 1);
                if (dirname.Contains("UIRoot"))
                    continue;
                //ToolsHelper.Log("文件名：" + dirname,false);
                FileName.Add(dirname);
            }
            return FileName;
        }


        /// <summary>
        /// 添加UI预设上Text字体为空的字体-黑体SIMHEI
        /// </summary>
        public static void SetUIPrefabTextFont()
        {
            Font font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Resources/Font/SIMHEI.TTF");

            List<string> FileName = FindUIPrefab();
            string BasePath = "Assets/GameRes/BundleRes/UI/";
            ToolsHelper.Log("font:" + font, false);

            foreach (var file in FileName)
            {
                //ToolsHelper.Log("file:" + file, false);
                GameObject go = AssetDatabase.LoadAssetAtPath(BasePath + file, typeof(GameObject)) as GameObject;
                Text[] texts = go.GetComponentsInChildren<Text>(true);
                foreach (var item in texts)
                {
                    if (item.font==null)
                    {
                        item.font = font;
                        //ToolsHelper.Log("Text:" + item.name, false);
                    }
                }
                EditorUtility.SetDirty(go);
                AssetDatabase.SaveAssets();
            }

        }


        public static void RemoveAssetBundleNames()
        {
            List<string> filterDir = new List<string>();
            filterDir.Add(AppSetting.BundleResDir);
            foreach (string fold in AppSetting.BundleArtResFolders)
                filterDir.Add(AppSetting.BundleArtResDir + fold);

            bool isClena = false;
            foreach (string assetGuid in AssetDatabase.FindAssets(""))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                string bundleName = assetImporter.assetBundleName;
                if (string.IsNullOrEmpty(bundleName))
                    continue;
                isClena = true;
                foreach (string filter in filterDir)
                {
                    if (assetPath.StartsWith(filter)) //清除非打包资源目录下的资源名
                    {
                        isClena = false;
                        break;
                    }
                }
                if (isClena)
                    assetImporter.assetBundleName = null;
            }
            ToolsHelper.Log("全部非打包资源AssetBundle名称已清除!");
        }

        /// <summary>
        /// Unity 5新AssetBundle系统，需要为打包的AssetBundle配置名称
        /// GameRes/BundleRes目录整个自动配置名称，因为这个目录本来就是整个导出
        /// </summary>
        public static void MakeAssetBundleNames()
        {            
            string baseBunldDir = AppSetting.BundleResDir;    
            // 设置新的资源名
            foreach (string filepath in Directory.GetFiles(baseBunldDir, "*.*", SearchOption.AllDirectories))
            {
                if (filepath.EndsWith(".meta")) continue;
                var importer = AssetImporter.GetAtPath(filepath);
                if (importer == null)
                {
                    ToolsHelper.Error(string.Format("Not found: {0}", filepath));
                    continue;
                }
                // var bundleName = filepath.Substring(baseBunldDir.Length, filepath.Length - baseBunldDir.Length);

                string bundleName = filepath.Substring(baseBunldDir.Length);

                bundleName = bundleName.Replace("\\", "/").ToLower();
                if (bundleName.StartsWith(AppSetting.ConfigBundleDir.ToLower()))  //config全部打到一个文件夹中
                {
                    bundleName = StringTools.SubstringIndexOf(bundleName, '/', 1);
                }
                //bundleName = bundleName.Replace("\\", "/").ToLower();
                //if (bundleName.StartsWith("lua/"))
                //{
                //    bundleName = StringUtil.SubstringIndexOf(bundleName, '/', AppSetting.LuaAssetBundleDepth);
                //}
                importer.assetBundleName = bundleName + AppSetting.ExtName;
            }
            //setAtlasIncludeInBuild(true);
            //setUIAtlasPropert(); //设置UIAtlas
            MakeArtResAssetBundleNames();
            ToolsHelper.Log("设置全部资源AssetBundle名称完成!");
        }


        /// <summary>
        /// </summary>
        public static void MakeArtResAssetBundleNames()
        {
            string baseBunldDir = AppSetting.BundleArtResDir;
            string[] folders = AppSetting.BundleArtResFolders;
            foreach (string fold in folders)
            {
                foreach (string filepath in Directory.GetFiles(baseBunldDir+ fold, "*.*", SearchOption.AllDirectories))
                {
                    if (filepath.EndsWith(".meta")) continue;
                    var importer = AssetImporter.GetAtPath(filepath);
                    if (importer == null)
                    {
                        ToolsHelper.Error(string.Format("Not found: {0}", filepath));
                        continue;
                    }
                    string bundleName = filepath.Substring(baseBunldDir.Length);
                    bundleName = bundleName.Replace("\\", "/").ToLower();
                    importer.assetBundleName = bundleName + AppSetting.ExtName;                   
                }
            }

        }

        /// <summary>
        /// 设置图集属性
        /// </summary>
        static void _SetAtlasIncludeInBuild(bool isInBuild)
        {
            string uiAtlasDir = AppSetting.BundleResDir + AppSetting.UIAtlasDir;
            DirectoryInfo dir = new DirectoryInfo(uiAtlasDir);
            string s = "bindAsDefault: " + (isInBuild ? "0" : "1");
            string r = "bindAsDefault: " + (isInBuild ? "1" : "0");
            FileInfo[] files = dir.GetFiles("*.spriteatlas", SearchOption.AllDirectories);
            int i = 0;
            foreach (FileInfo file in files)
            {
                string str = string.Empty;
                using (StreamReader stream = file.OpenText())
                {
                    str = stream.ReadToEnd();
                    stream.Close();
                    stream.Dispose();
                }
                str = str.Replace(s, r);
                using (StreamWriter write = file.CreateText())
                {
                    write.Write(str);
                    write.Close();
                    write.Dispose();
                }                
                string allPath = file.FullName;
                string temp_assetPath = allPath.Substring(allPath.IndexOf("Assets"));
                //AssetDatabase.ImportAsset(temp_assetPath, ImportAssetOptions.Default);
                //ToolsHelper.ShowProgress("设置图集属性", files.Length, ++i);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(uiAtlasDir);
        }

     

        /// <summary>
        /// 重新打包资源
        /// </summary>
        /// <returns></returns>
        public static void ReBuildAllAssetBundles()
        {
            if (ToolsHelper.IsPlaying()) return;
            ToolsHelper.ClearConsole();
            string outputPath = GetExportPath();
            Directory.Delete(outputPath, true);
            ToolsHelper.Log("删除目录: " + outputPath);
            BuildAllAssetBundles();           
        }
        /// <summary>
        /// 复制音频
        /// </summary>
        public static void CopyOnceAuido() 
        {

            BuildTarget platfrom = EditorUserBuildSettings.activeBuildTarget;
            if (platfrom != BuildTarget.Android)
                return;
            string sourcePath = AppSetting.DataPath + "GameRes/BundleRes/Audio/Once";
            string targetPath = GetExportPath() + "audio/once";
            ToolsHelper.Log("音效源目录: " + sourcePath);
            ToolsHelper.Log("targetPath: " + targetPath);
            //找到目录下所有音频文件
            DirectoryInfo folder = new DirectoryInfo(sourcePath);
            List<string> AuiodFile = new List<string>();
            
            foreach (var file in folder.GetFiles())
            {
                if (file.FullName.EndsWith(".meta"))
                    continue;
                AuiodFile.Add(file.Name);
            }
            //foreach (var item in AuiodFile)
            //{
            //    ToolsHelper.Log(item);
            //}
            foreach (var item in AuiodFile)
            {
                string FileSourcePath = Path.Combine(sourcePath, item);
                string FileTargetPathPath = Path.Combine(targetPath, item);
                if (File.Exists(FileTargetPathPath))
                    continue;
                //ToolsHelper.Log()
                File.Copy(FileSourcePath, FileTargetPathPath);
            }

            ToolsHelper.Log("音频文件复制完成");
        }

        /// <summary>
        /// 打包所有资源
        /// </summary>
        public static void BuildAllAssetBundles()
        {
            if (ToolsHelper.IsPlaying()) return;
            ToolsHelper.ClearConsole();   

            EditorCoroutineRunner.StartEditorCoroutine(_OnBuildAllAssetBundles());
        }

        static IEnumerator _OnBuildAllAssetBundles()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            yield return new WaitForSeconds(0.3f);
            //CopyHotFix();
            CreateLayerCollisionMatrix();
            yield return null;
            RemoveAssetBundleNames();
            MakeAssetBundleNames();
            yield return null;
            //RemoveUIPrefabTextFont();
            yield return new WaitForSeconds(0.1f);
            ToolsHelper.Log("资源打包中...");
            yield return null;
            var outputPath = GetExportPath();
            ToolsHelper.Log("打包路径..."+ outputPath);
#if UNITY_ANDROID
            _SetAtlasIncludeInBuild(false);
#endif
            yield return new WaitForSeconds(0.5f);//DessterministicAssetBundle

            ToolsHelper.Log("设置图集属性完成...");
            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            yield return new WaitForSeconds(0.5f);
            //_SetAtlasIncludeInBuild(true);
#if UNITY_ANDROID
            CopyOnceAuido();
#endif
            yield return null;
            CreateAssetBundleFileInfo();
            yield return null;
            //SetUIPrefabTextFont();
            yield return new WaitForSeconds(0.1f);
            watch.Stop();
            ToolsHelper.Log("资源打包完成!!用时:" + (watch.ElapsedMilliseconds / 1000.0f)+ "秒");
            AssetDatabase.Refresh();
            yield break;
        }
        /// <summary>
        /// 获取导出资源路径目录
        /// </summary>
        /// <returns></returns>
        public static string GetExportPath()
        {
            BuildTarget platfrom = EditorUserBuildSettings.activeBuildTarget;
            string projectName = AppSetting.MineGameName;
            string basePath = AppSetting.ExportResBaseDir;

            if (File.Exists(basePath))
            {
                ToolsHelper.ShowDialog("路径配置错误: " + basePath);
                throw new System.Exception("路径配置错误");
            }
            string path = null;
            var platformName = AppSetting.PlatformName;
            path = basePath +platformName+"/"+ projectName+"/";
            ToolsHelper.CreateDir(path);
            return path;
        }
        /// <summary>
        /// 获取IOS打包AB路径
        /// </summary>
        /// <returns></returns>
        public static string GetABExportPatIOS()
        {
            string projectName = AppSetting.MineGameName;
            string basePath = Application.dataPath+"/";

            if (File.Exists(basePath))
            {
                ToolsHelper.ShowDialog("路径配置错误: " + basePath);
                throw new System.Exception("路径配置错误");
            }
            string path = null;
            var platformName = AppSetting.PlatformName;
            path = basePath + platformName + "/" + projectName + "/";
            ToolsHelper.CreateDir(path);
            return path;
        }

        /// <summary>
        /// 清理冗余，即无此资源，却有AssetBundle的
        /// </summary>
        public static void CleanAssetBundlesRedundancies()
        {
            var platformName = AppSetting.PlatformName;
            var outputPath = GetExportPath();

            int count = 0;
            var toList = new List<string>(Directory.GetFiles(outputPath, "*.*", SearchOption.AllDirectories));
            for (var i = toList.Count - 1; i >= 0; i--)
            {
                var filePath = toList[i];               
                var abName = toList[i].Replace(outputPath, "").Replace('\\', '/');
                var extName = Path.GetExtension(filePath);
                if (abName != platformName && extName!=".manifest")
                {
                    //删除.meta文件
                    if (extName==".meta")
                    {
                        File.Delete(filePath);
                    }
                    else
                    {
                        if (AssetDatabase.GetAssetPathsFromAssetBundle(abName).Length == 0)
                        {
                            var manifestPath = filePath + ".manifest";
                            File.Delete(filePath);
                            ToolsHelper.Log("Delete... " + filePath);
                            count += 1;
                            if (File.Exists(manifestPath))
                            {
                                File.Delete(manifestPath);
                                ToolsHelper.Log("Delete... " + manifestPath);
                                count += 1;
                            }
                        }
                    }
                }
                //删除空文件夹
                DirectoryInfo dir = new DirectoryInfo(outputPath);
                DirectoryInfo[] subdirs = dir.GetDirectories("*.*", SearchOption.AllDirectories);
                foreach (DirectoryInfo subdir in subdirs)
                {
                    FileInfo[] subFiles = subdir.GetFiles();
                    if (subFiles.Length == 0)
                    {
                        subdir.Delete();
                    }
                }                
            }
            ToolsHelper.Log("清理冗余文件完成!! 共" + count + "个文件!");
        }

        /// <summary>
        /// 生成AB资源文件列表
        /// </summary>
        public static void CreateAssetBundleFileInfo()
        {
            string abRootPath = GetExportPath();
            string abFilesPath = abRootPath +AppSetting.ABFiles;
            if (File.Exists(abFilesPath))
                File.Delete(abFilesPath);

            var abFileList = new List<string>(Directory.GetFiles(abRootPath, "*"+AppSetting.ExtName, SearchOption.AllDirectories));
            abFileList.Add(abRootPath + AppSetting.MineGameName);
            FileStream fs = new FileStream(abFilesPath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2018, 1, 1));
            int ver =  ((int)((DateTime.Now - startTime).TotalMinutes));
            sw.WriteLine(ver + "|" + DateTime.Now.ToString("u"));
            for (int i = 0; i < abFileList.Count; i++)
            {
                string file = abFileList[i];
                long size = 0;
                string md5 = MD5Utils.MD5File(file,out size);
                string value = file.Replace(abRootPath, string.Empty).Replace("\\","/");
                sw.WriteLine(value + "|" + md5+"|"+ size);
            }
            sw.Close();
            fs.Close();
            ToolsHelper.Log("资源版本Version:"+ver+ "  已复制到剪切板");
            ToolsHelper.Log("ABFiles文件生成完成");
            ToolsHelper.CopyString(ver.ToString());
        }


        //public static void BuildHotFixAssetBundles()
        //{
        //    if (ToolsHelper.IsPlaying()) return;
        //    ToolsHelper.ClearConsole();
        //    Stopwatch watch = new Stopwatch();
        //    watch.Start();
        //    CopyHotFix();
        //    Dictionary<string, List<string>> buildMap = new Dictionary<string, List<string>>();
        //    string baseBunldDir = "Assets/GameRes/BundleRes/Data";
        //    List<string> abNames;
        //    foreach (string filepath in Directory.GetFiles(baseBunldDir, "*.*", SearchOption.AllDirectories))
        //    {
        //        if (filepath.EndsWith(".meta")) continue;
        //        AssetImporter importer = AssetImporter.GetAtPath(filepath);
        //        if (importer == null)
        //            continue;
        //        if (importer.assetBundleName == string.Empty)
        //        {
        //            ToolsHelper.Warning("文件没设置AB名:" + filepath);
        //            continue;
        //        }
        //        if (!buildMap.TryGetValue(importer.assetBundleName, out abNames))
        //        {
        //            abNames = new List<string>();
        //            buildMap.Add(importer.assetBundleName, abNames);
        //        }
        //        abNames.Add(importer.assetPath);
        //    }
        //    // 设置新的资源名
        //    List<AssetBundleBuild> abList = new List<AssetBundleBuild>();
        //    foreach (KeyValuePair<string, List<string>> keyVal in buildMap)
        //    {
        //        AssetBundleBuild ab = new AssetBundleBuild();
        //        ab.assetBundleName = keyVal.Key;
        //        ab.assetNames = keyVal.Value.ToArray();
        //        abList.Add(ab);
        //    }
        //    var outputPath = GetExportPath();
        //    BuildPipeline.BuildAssetBundles(outputPath, abList.ToArray(), BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        //    CreateAssetBundleFileInfo();
        //    watch.Stop();
        //    ToolsHelper.Log("Data文件打包完成!!用时:" + (watch.ElapsedMilliseconds / 1000.0f) + "秒");
        //}
        /// <summary>
        /// 保存碰撞矩阵数据
        /// </summary>
        public static void CreateLayerCollisionMatrix() 
        {
            string FilePath = Application.dataPath + "/GameRes/BundleRes/Data/LayerCollisionMatrix.bytes";
            string content = "";
            for (int i = 0; i < 32; i++)
            {
                for (int j = 31; j >= i; j--)
                {
                    bool Ignore = Physics.GetIgnoreLayerCollision(i, j);
                    if (Ignore)
                    {
                        content += $"{i},{j}_";
                    }
                }
            }
            ToolsHelper.SaveFile(FilePath, content);
            AssetDatabase.Refresh();
        }
    }
}