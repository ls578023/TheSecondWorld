
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 游戏路径管理
/// </summary>

public class GamePath
{
    private bool isInit = false;

    private static GamePath _instance;

    private string _rootPath = "";

    private string _logPath = "";

    private string _userDataPath = "";

    private string _streamingAssetsPath = "";

    private string _mineGameDataPath = "";


    public static GamePath Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GamePath();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 外部资源的根目录
    /// </summary>
    public string MineGameDataPath
    {
        get
        {
            return _mineGameDataPath;
        }
    }


    /// <summary>
    /// 外部资源的根目录
    /// </summary>
    public string RootPath
    {
        get
        {
            return _rootPath;
        }
    }

    /// <summary>
    /// 日志打印路径
    /// </summary>
    public string LogPath
    {
        get
        {
            return _logPath;
        }
    }

    /// <summary>
    /// 用户数据路径
    /// </summary>
    public string UserDataPath
    {
        get
        {
            return _userDataPath;
        }
    }



    /// <summary>
    /// StreamingAssets路径
    /// </summary>
    public string StreamingAssetsPath
    {
        get
        {
            return _streamingAssetsPath;
        }
    }

    /// <summary>
    /// 平台文件夹父节点
    /// </summary>
    public string PlatformFolder
    {
        get
        {
            string platform = "PC";
#if UNITY_ANDROID
            platform = "Android";
#elif UNITY_IOS
            platform = "IOS";
#else

#endif
            return platform;
        }
    }


    GamePath()
    {
        if (!isInit)
        {
            Init();
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isInit)
        {
            return;
        }
        string path = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath + "/";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.persistentDataPath + "/";
        }
        else
        {
            path = Application.dataPath;
            int index = path.LastIndexOf("/");
            if (index != -1)
            {
                path = path.Substring(0, index + 1);
            }
        }

        _rootPath = path + "LocalFile/";
        _logPath = _rootPath + "Log/";
        _userDataPath = _rootPath + "UserData/";

        this.InitStreamingAssetsPath();

        CreateDirectory(_rootPath);
        CreateDirectory(_logPath);
        CreateDirectory(_userDataPath);

        isInit = true;
    }

    public void FindMineGamePath() 
    {
        _mineGameDataPath = _rootPath + GameFramework.AppSetting.MineGameName + "/";
        CreateDirectory(_mineGameDataPath);
    }

    private void InitStreamingAssetsPath()
    {
        string spath = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            spath = "jar:file://" + Application.dataPath + "!/assets";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            spath = "file://" + Application.dataPath + "/Raw";

        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            spath = Application.dataPath + "/StreamingAssets";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            spath = "file://" + Application.streamingAssetsPath;
        }
        else
        {
            spath = Application.streamingAssetsPath;
        }
        _streamingAssetsPath = spath;
    }

    private void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}


