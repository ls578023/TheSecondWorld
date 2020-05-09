using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTSDK
{
    public class ReportDefine
    {

        /// <summary> 游戏时长</summary>
        public const string playtime = "playtime";
        /// <summary> 游戏模块时长</summary>
        public const string playtimeGameModel = "playtime_";

        /// <summary> 游戏开始次数</summary>
        public const string starttimes = "starttimes";
        /// <summary> 游戏开始次数_模块</summary>
        public const string starttimesGameModel = "starttimes_";
        /// <summary> 游戏结束次数</summary>
        public const string overtimes = "overtimes";
        /// <summary> 游戏结束次数_模块</summary>
        public const string overtimesGameModel = "overtimes_";

        /// <summary> 各关卡的开始</summary>
        public const string levelstarttimes = "levelstarttimes";
        /// <summary> 各关卡的结束</summary>
        public const string levelovertimes = "levelovertimes";

        /// <summary> 关卡首次开始</summary>
        public const string levelFirsstarttimes = "levelFirsstarttimes";
        /// <summary> 关卡首次过关</summary>
        public const string levelFirstovertimes = "levelFirstovertimes";

        /// <summary> 玩法人数</summary>
        public const string playpeople = "playpeople";
        /// <summary> 模块玩法人数</summary>
        public const string playpeopleGameModel = "playpeople_";

        /// <summary> 看视频复活次数</summary>
        public const string Videoalive = "Videoalive";
        /// <summary> 看视频复活次数-单个玩法里面的二级玩法</summary>
        public const string VideoaliveGameModel = "Videoalive_";

        /// <summary> 看视频解锁次数</summary>
        public const string VideoUnlock = "VideoUnlock";
        /// <summary> 看视频解锁次数-单个玩法里面的二级玩法</summary>
        public const string VideoUnlockGameModel = "VideoUnlock_";

        /// <summary> 看视频得奖励</summary>
        public const string Videoreward = "Videoreward";
        /// <summary> 看视频得奖励-单个玩法里面的二级玩法</summary>
        public const string VideorewardGameModel = "Videoreward_";

    }
}
