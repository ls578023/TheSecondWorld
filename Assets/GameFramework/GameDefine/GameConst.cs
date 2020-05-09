using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameConst 
{
    public const string encryptKey="dotJoy";
    public const string UserDataFileName= "UserData.txt";
    public const string MineGameMainScene = "MineGameMain";
    public const int ReportIntervalTimes = 30;//上报默认时长

    public class GameFrameWorkLang
    {
        public enum GameFrameWorkLangKey
        {
            VideoNoReady=0,
            AntiAddictionTips,
        }
        /// <summary>
        /// 框架里面的多语言
        /// 1维索引代表GameFrameWorkLangKey
        /// 2维代表值 2维里面的值 
        ///0-ZH_CN,   //简体中文
        ///1-ZH_TW,  //繁体中文
        ///2-EN,         //英文
        ///3-JA,         //日语
        ///4-KO,        //韩语 
        /// </summary>
        public static string[,] LangList = { 
            {
                "广告尚未准备好",
                "廣告尚未準備好",
                "The ad is not ready",
                "広告はまだ用意されていない",
                "광고가 현재 준비 중입니다"
            }
            ,
            {

                "抵制不良游戏,拒绝盗版游戏.注意自我保护,谨防受骗上当.适度游戏益脑,沉迷游戏伤身.合理安排时间,享受健康生活。本游戏适合年满8周岁（含）以上的用户使用，为了您的健康，请合理控制游戏时间。",
                "",
                "",
                "",
                ""
            } 
        };

        public static string Get(GameFrameWorkLangKey gameFrameWorkLangKey)
        {
            if (LangList.Length < (int)gameFrameWorkLangKey)
                return"";
            GameFramework.ELangType ELangType = GameFramework.GameFrameEntry.GetModule<GameFramework.LangModule>().LangType;
            return LangList[(int)gameFrameWorkLangKey, (int)ELangType];
        }
    }
}
/// <summary>
/// 小游戏的二级玩法
/// </summary>
public enum MineGameModel
{
    /// <summary>
    /// 没有分模式
    /// </summary>
    None,
    /// <summary>
    /// 闯关模式
    /// </summary>
    LevelModel,
    /// <summary>
    /// 无尽模式
    /// </summary>
    InterminableModel,
    /// <summary>
    /// PVP对战
    /// </summary>
    PVPModel,
}
