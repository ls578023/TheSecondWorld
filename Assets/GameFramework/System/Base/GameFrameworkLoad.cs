using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFrameworkLoad : MonoBehaviour
{

    void Start()
    {
        CLog.Log("进入到GameFrameworkLoad场景");
        //模块关闭
        GameFrameEntry.Shutdown();

        if (GameFrameEntry.IsGoGameMenu)
        {
            if (string.IsNullOrEmpty(GameFrameEntry.GameMenuSceneName))
            {
                CLog.Error("游戏菜单场景是空的");
                return;
            }
            SceneManager.LoadScene(GameFrameEntry.GameMenuSceneName);
        }
        else
        {
            AppSetting.MineGameName = GlobalData.MineAppName;
            GameFrameEntry.GetModule<VersionMondule>();
            GameFrameEntry.GetModule<AssetbundleModule>();
            StartMineGame();
        }
    }

    async void StartMineGame()
    {
        await GameFrameEntry.Initialize();
        GameFrameEntry.Start();
        await new WaitForEndOfFrame();
        await GameFramework.LoadHelper.LoadScene(GameConst.MineGameMainScene);
    }
}
