
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using GameFramework;

public class BuildAPKTools

{
    static string[] SCENES = FindEnabledEditorScenes();
    /// <summary>
    /// 0-BUGLY ID 1-BUGLY kEY 2-安卓包名 3-app名称 4-App的Key 5-PrjBuild中的名称
    /// </summary>
    static List<string> appSetting;

    public static void BulidTarget(EPlatformType pfType,string channelid="")
    {
        appSetting = ReadAppSettingTxt();
        string pfName = pfType.ToString();
        string app_name = $"{ Application.productName }.{pfName}_{ DateTime.Now.ToString("yyyyMMdd_HHmm")}";
        string target_dir = "";
        string target_name = "";
        BuildTarget buildTarget = BuildTarget.Android;
        string applicationPath = Application.dataPath.Replace("/Assets", "");        
        switch (pfType)
        {
            case EPlatformType.SDK_ReleaseIOS:
            case EPlatformType.SDK_TestIOS:
                target_dir = applicationPath + "/Builds/BuildIOS";
                target_name = app_name;
                buildTarget = BuildTarget.iOS;
                break;
            default:
                target_dir = applicationPath + "/Builds/BuildAndroid";
                target_name = app_name;
                buildTarget = BuildTarget.Android;
                PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
                EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
                //SetKey();
                break;
        }

        if (string.IsNullOrEmpty(channelid))
        {
            //Test_IOS_Proj_xfkp
            channelid = "Test_"+buildTarget.ToString() + "_" + AppSetting.MineGameName;
        }

        Directory.CreateDirectory(target_dir);
        //PlayerSettings.bundleVersion = "9.9.9";
        //PlayerSettings.ve = "9999";
        if (pfType== EPlatformType.SDK_Test || pfType == EPlatformType.VersionPackage)
        {
            //安卓测试版设置包名
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.wedobest.u3d.demo");

            if(pfType == EPlatformType.VersionPackage)
            {
                //版本包添加预定义宏
                string ScriptingDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
                if (!ScriptingDefine.Contains("VersionPackage"))
                {
                    if (string.IsNullOrEmpty(ScriptingDefine))
                        ScriptingDefine = "VersionPackage";
                    else
                        ScriptingDefine += ";VersionPackage";
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, ScriptingDefine);
                }
            }
        }else if(pfType== EPlatformType.SDK_Release)
        {
            //安卓发布版设置包名
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, GetAppSettingInfo(2));
        }
       BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, target_dir + "/" + target_name, buildTarget, BuildOptions.None);
        
        if (pfType == EPlatformType.SDK_ReleaseIOS || pfType == EPlatformType.SDK_TestIOS)
            return;
        ToolsHelper.Log("生成Android工程完成，开始替换SDK文件");
        //整理安卓工程
        string projectPath = target_dir + "/" + target_name + $"/{Application.productName}/";
        //string projectPath = target_dir + "/" + "GameFrameworkTest.SDK_Test_20191210_1012" + $"/{Application.productName}/";
        EditorCoroutineRunner.StartEditorCoroutine(_OnBuildAllAssetBundles(pfType,projectPath, channelid));
    }

    //工程整理
    static IEnumerator _OnBuildAllAssetBundles(EPlatformType pfType,string projectPath,string channelid) 
    {
        AndroidProjectArrangement(projectPath);
        yield return null;//
        if (pfType == EPlatformType.SDK_Test|| pfType== EPlatformType.VersionPackage)
        {
            //如果是测试版本，修改广告文件
            #region 修改发布渠道
            //修改AndroidManifest
            string MainfestPath = projectPath + "AndroidManifest.xml";
            if (File.Exists(MainfestPath))
            {
                try
                {
                    string AndroidManifest = File.ReadAllText(MainfestPath);
                    AndroidManifest = AndroidManifest.Replace("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", channelid);
                    File.WriteAllText(MainfestPath, AndroidManifest);
                }
                catch (Exception e)
                {

                    Debug.LogError(e);
                }
            }
            else
                Debug.LogError($"未找到AndroidManifest.xml文件+{MainfestPath}");
            #endregion

            //修改AdsConstant
            string AdsConstantPath = projectPath + "src/com/pdragon/ad/AdsConstant.java";
            if (File.Exists(AdsConstantPath))
            {
                string str1 = "ShowSplashAd";
                string str2 = "SplashAdCallback";
                string str3 = "ShowBannerAd";
                string str4 = "ShowInterstitialAd";
                string str5 = "ShowVideoAd";
                try
                {
                    string[] lines = File.ReadAllLines(AdsConstantPath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains(str1) || lines[i].Contains(str2) || lines[i].Contains(str3) || lines[i].Contains(str4)
                            || lines[i].Contains(str5))
                        {
                            lines[i]=lines[i].Replace("false", "true");
                        }
                    }
                    File.WriteAllLines(AdsConstantPath, lines);
                }
                catch (Exception e)
                {

                    Debug.LogError(e);
                }
            }


            //删除生成的UnityPlayerActivity.java文件
            string UnityPlayerActivityPath = projectPath + "src/com/wedobest/u3d/demo/UnityPlayerActivity.java";
            if (File.Exists(UnityPlayerActivityPath))
            {
                File.Delete(UnityPlayerActivityPath);
            }
            //去掉VersionPackage宏定义
            if (pfType== EPlatformType.VersionPackage)
            {
                //CopyVerPackgetFile(projectPath);//自动引用 不用复制文件操作

                //版本包添加预定义宏
                string ScriptingDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
                ScriptingDefine = ScriptingDefine.Replace(",VersionPackage", "");
                ScriptingDefine = ScriptingDefine.Replace("VersionPackage", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, ScriptingDefine);
            }
        }
        ToolsHelper.Log("发布版本操作");
        yield return new WaitForSeconds(0.1f);//DessterministicAssetBundle
        //正式版修改包名
        if (pfType == EPlatformType.SDK_Release)
            ReleaseSDKFileModify(projectPath);

        yield return null;
        //由于加入了版本包文件，所以得删除版本包资源
        if(pfType!= EPlatformType.VersionPackage)
        {
            DeleteVersionPackageFile(projectPath);
        }
        yield return null;
        //替换APPName
        //找到所有的strings.xml
        ModifyStringsXmlFile(projectPath, pfType== EPlatformType.SDK_Test?PlayerSettings.productName:GetAppSettingInfo(3));
        ToolsHelper.Log("发包完成.");
        yield return null;
        ToolsHelper.ShowExplorer(projectPath);

    }
    /// <summary>
    /// 删除版本包文件
    /// </summary>
    static void DeleteVersionPackageFile(string projectPath) 
    {
        string fileName1 = "libs/BasePopup-2.2.1.aar";
        string fileName2 = "libs/dbtcertification-1.0-release.aar";
        string DirectoryName = "src/com/pdragon/third";
        File.Delete(Path.Combine(projectPath, fileName1));
        File.Delete(Path.Combine(projectPath, fileName2));
        Directory.Delete(Path.Combine(projectPath, DirectoryName), true);
    }

    /// <summary>
    /// 修改所有的strings.xml文件
    /// </summary>
    static void ModifyStringsXmlFile(string projectPath, string AppName) 
    {

        var pattern = "*";
        var allFileInfo = Directory.GetFiles(projectPath+ "res", pattern, SearchOption.AllDirectories);
        foreach (var item in allFileInfo)
        {
            if (item.Contains("strings.xml"))
            {
                Debug.Log("strings.xml:"+item);
                string conent = File.ReadAllText(item);
                conent = conent.Replace("xxxxx", AppName);
                File.WriteAllText(item, conent);
            }
        }
    }

    /// <summary>
    /// 安卓工程整理
    /// </summary>
    static void AndroidProjectArrangement(string TargetPath)
    {
        ToolsHelper.Log($"生成的工程路径：{TargetPath}");
        string ScrPath = "src/main";
        try
        {
            //把src/main/目录下jniLibs文件改成libs
            string JniLibsPath = Path.Combine(TargetPath, ScrPath, "jniLibs");
            string JniLibsTargerPath = Path.Combine(TargetPath, ScrPath, "libs");
            Directory.Move(JniLibsPath, JniLibsTargerPath);
            //复制src/main文件夹下所有文件到根目录
            ToolsHelper.CopyDirToDir(Path.Combine(Path.Combine(TargetPath, ScrPath)), TargetPath.Remove(TargetPath.Length - 1));
            //删除src/文件夹
            Directory.Delete(Path.Combine(TargetPath, "src"), true);
            //java目录改成src目录
            Directory.Move(Path.Combine(TargetPath, "java"), Path.Combine(TargetPath, "src"));
            //复制SDK文件
            CopySDKFile(TargetPath);
        }
        catch (Exception e)
        {
            ToolsHelper.Error(e.Message);
        }

    }

    const string SDKFilePath = "/GameFramework/SDK/SDKFile";
    static void CopySDKFile(string targetPath) 
    {
        string Path = Application.dataPath + SDKFilePath;
        targetPath = targetPath.Remove(targetPath.Length - 1);
        ToolsHelper.CopyDirToDir(Path, targetPath);
    }

    /// <summary>
    /// 复制版本包 实名认证文件
    /// </summary>
    /// <param name="targetPath"></param>
    static void CopyVerPackgetFile(string targetPath)
    {
        string Path = Application.dataPath + "/GameFramework/SDK/VerPackageFile";
        targetPath = targetPath.Remove(targetPath.Length - 1);
        ToolsHelper.CopyDirToDir(Path, targetPath);
    }
    /// <summary>
    /// 正式发布版本修改SDK文件路径和位置
    /// </summary>
    static void ReleaseSDKFileModify(string projectPath) 
    {
        ToolsHelper.Log("发布修改代码包名");
        string ScriptDir = "src/";
        string packgeName = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
        string packgeNamePath = packgeName.Replace(".", "/");
        string targetDir = projectPath + ScriptDir + packgeNamePath;

        string SourceDir = "com/wedobest";
        string SourcePath = projectPath + ScriptDir + $"{SourceDir}/u3d/demo";
        //吧com/wedobest文件下的内容copy到包文件夹中
        ToolsHelper.CopyDirToDir(SourcePath, targetDir);
        Directory.Delete(projectPath + ScriptDir + SourceDir, true);

        //删除生成的UnityPlayerActivity.java文件
        string UnityPlayerActivityPath = targetDir + "/UnityPlayerActivity.java";
        if (File.Exists(UnityPlayerActivityPath))
        {
            File.Delete(UnityPlayerActivityPath);
        }

        //修改JAVA文件的包名
        var pattern = "*";
        var allFileInfo = Directory.GetFiles(targetDir, pattern, SearchOption.AllDirectories);
        foreach (var item in allFileInfo)
        {
            ModifyJavaFile(item);
        }
        //修改AndroidManifest
        string MainfestPath = projectPath + "AndroidManifest.xml";
        if (File.Exists(MainfestPath))
        {
            try
            {
                string AndroidManifest = File.ReadAllText(MainfestPath);
                AndroidManifest = AndroidManifest.Replace("com.wedobest.u3d.demo", packgeName);
                AndroidManifest = AndroidManifest.Replace("pn5tLrJ0ZG+ndHN062QzZOt1ZW2qEQAAxREAAA==", GetAppSettingInfo(4));
                File.WriteAllText(MainfestPath, AndroidManifest);
            }
            catch (Exception e)
            {

                Debug.LogError(e);
            }
        }
        else
            Debug.LogError($"未找到AndroidManifest.xml文件+{MainfestPath}");
        //修改build.gradle文件
        string buildgradle = projectPath + "build.gradle";

        if (File.Exists(buildgradle))
        {
            try
            {
                string[] lines = File.ReadAllLines(buildgradle);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("UnityAdsTest"))
                    {
                        lines[i] = $"//{lines[i]}";
                        break;
                    }
                }
                File.WriteAllLines(buildgradle, lines);
            }
            catch (Exception e)
            {

                Debug.LogError(e);
            }
        }
        else
            Debug.LogError($"未找到build.gradle文件+{buildgradle}");
        //修改AdsConstant.java文件
        string AdsConstantPath = projectPath + "src/com/pdragon/ad/AdsConstant.java";
        if (File.Exists(AdsConstantPath))
        {
            try
            {
                string[] lines = File.ReadAllLines(AdsConstantPath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("public final static String BUGLY_ID"))
                    {
                        lines[i] = $@"	public final static String BUGLY_ID=""{GetAppSettingInfo(0)}"";";
                    }
                    if (lines[i].Contains("public final static String BUGLY_KEY")) 
                    {
                        lines[i] = $@"	public final static String BUGLY_KEY=""{GetAppSettingInfo(1)}"";";
                    }
                }
                File.WriteAllLines(AdsConstantPath, lines);
            }
            catch (Exception e)
            {

                Debug.LogError(e);
            }
        }
        else
            Debug.LogError($"未找到AdsConstant.java文件+{AdsConstantPath}");

        //修改PrjBuild.ini文件
        string PrjBuild = projectPath + "PrjBuild.ini";
        if (File.Exists(PrjBuild))
        {
            try
            {
                string[] lines = File.ReadAllLines(PrjBuild);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains("sign="))
                    {
                        lines[i] = $"sign={GetAppSettingInfo(5)}";
                    }
                }
                File.WriteAllLines(PrjBuild, lines);
            }
            catch (Exception e)
            {

                Debug.LogError(e);
            }
        }
        else
            Debug.LogError($"未找到PrjBuild.ini文件+{PrjBuild}");
    }

    static void ModifyJavaFile(string filePath) 
    {
        string Str = Application.productName + "/src";
        string packageName= filePath.Substring(filePath.LastIndexOf(Str) + Str.Length+1);
        packageName = packageName.Replace("\\", "/");
        packageName = packageName.Substring(0,packageName.LastIndexOf("/"));
        packageName = packageName.Replace("/", ".");

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].TrimStart().StartsWith("package"))
                {
                    //Debug.Log("找到包名  " + lines[i]);
                    lines[i] = $"package {packageName};";
                    break;
                }
            }
            File.WriteAllLines(filePath, lines);
        }
        catch (Exception e)
        {

            Debug.LogError(e);
        }
    }

    const string MineGameMainScene = "Assets/GameRes/BundleRes/Scene/MineGameMain.unity";
    static EditorBuildSettingsScene[] ProjectScenes;
    /// <summary>
    /// 客户端发单独包时，场景只保留 MineGameMain
    /// </summary>
    public static void ModifyBuildSettingsScene()
    {
        ProjectScenes = EditorBuildSettings.scenes;
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[1];
        scenes[0] = new EditorBuildSettingsScene(MineGameMainScene, true);
        EditorBuildSettings.scenes = scenes;
    }
    /// <summary>
    /// 客户端发包完成后还原场景
    /// </summary>
    public static void ReductionBuildSettingsScene()
    {
        if (ProjectScenes == null)
            return;
        EditorBuildSettings.scenes = ProjectScenes;
    }

    private static void SetKey()
    {
        string applicationPath = Application.dataPath.Replace("Assets", "")+ "user.keystore";
        //Debug.Log("Key:"+applicationPath);
        PlayerSettings.Android.keystoreName = applicationPath;
        PlayerSettings.Android.keystorePass = "123456";
        PlayerSettings.Android.keyaliasName = "dadada";
        PlayerSettings.Android.keyaliasPass = "123456";
    }

    private static void CopyHYSDKFiles(string sdkName)
    {
        string sdkMidsdk = $"Assets/SDK/{sdkName}/midsdk.txt";
        string targetMidsdk = $"Assets/Plugins/Android/assets/midsdk.txt";
        AssetDatabase.CopyAsset(sdkMidsdk, targetMidsdk);

        string sdkMidsdkAAR = $"{Application.dataPath}/SDK/{sdkName}/midsdk-release.aar";
        string targetMidsdkAAR = $"{Application.dataPath}/Plugins/Android/libs/midsdk-release.aar";
        File.Copy(sdkMidsdkAAR, targetMidsdkAAR, true);

        string sdkMideventAAR = $"{Application.dataPath}/SDK/{sdkName}/midevent-release.aar";
        string targetMideventAAR = $"{Application.dataPath}/Plugins/Android/libs/midevent-release.aar";
        AssetDatabase.CopyAsset(sdkMideventAAR, targetMideventAAR);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        //只打包前一个场景
        int sceneNum = 1;
        int currNum=0;
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            currNum++;
            EditorScenes.Add(scene.path);
            if (currNum >= sceneNum)
                break;
        }
        return EditorScenes.ToArray();
    }

    /// <summary>
    /// 改变平台类型
    /// </summary>
    /// <param name="pfType"></param>
    //public static void ChagePlatformType(EPlatformType pfType,bool isAccPwdTest = false)
    //{
    //    string pfName = pfType.ToString();
    //    BuildTarget buildTarget = BuildTarget.Android;
    //    BuildTargetGroup buildTargetGroup = BuildTargetGroup.Android;
    //    switch (pfType)
    //    {
    //        case EPlatformType.HY_MC:
    //        case EPlatformType.HY_GP:
    //            buildTarget = BuildTarget.Android;
    //            buildTargetGroup = BuildTargetGroup.Android;
    //            break;
    //        case EPlatformType.HY_IOS:
    //            buildTarget = BuildTarget.iOS;
    //            buildTargetGroup = BuildTargetGroup.iOS;
    //            break;
    //        case EPlatformType.AccountPwd:
    //            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
    //                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, isAccPwdTest?"TEST": string.Empty);
    //            else
    //                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, isAccPwdTest ? "TEST" : string.Empty);
    //            return;
    //    }
    //    PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, pfName);


    //    if (EditorUserBuildSettings.activeBuildTarget != buildTarget)
    //        ToolsHelper.Error($"平台宏切换失败,当前为平台为:{EditorUserBuildSettings.activeBuildTarget},请切换至{buildTarget}");
    //    else
    //        ToolsHelper.Log($"平台宏已经切换到{pfName}");
    //}

    static List<string> ReadAppSettingTxt()
    {
        string AppSetting = Application.dataPath + "/GameRes/AppIcon/AppSetting.txt";

        Debug.Log("ReadAppSettingTxt:" + AppSetting);
        List<string> list = new List<string>();
        if (File.Exists(AppSetting))
        {
            string[] lines = File.ReadAllLines(AppSetting);
            if (lines != null && lines.Length > 0)
            {
                try
                {
                    foreach (var item in lines)
                    {
                        Debug.Log("APPSetting:" + item);
                        list.Add(item.Split(':')[1]);
                    }
                }
                catch (Exception e)
                {

                    Debug.LogError(e);
                }
            }
        }
        return list;
    }
    /// <summary>
    /// 获取APP的设置信息
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    static string GetAppSettingInfo(int index) 
    {
        if (appSetting == null)
            return "";
        if (index>=appSetting.Count)
            return "";
        return appSetting[index];
    }
}
