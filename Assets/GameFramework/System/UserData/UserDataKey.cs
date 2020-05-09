using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    public class UserDataKey
    {
        public const string MusicGameVolume = "MusicGameVolume";
        public const string SoundGameVolume = "SoundGameVolume";
        /// <summary>游戏安装的天数</summary>
        public const string Day = "Day";
        /// <summary>
        /// 上报玩家第一次进入的key
        /// </summary>
        public const string Playpeople = "playpeople";

        /// <summary>游戏开始的关卡数</summary>
        public const string StartLevleCount = "StartLevleCount";
        /// <summary>游戏完成的关卡数</summary>
        public const string OverLevleCount = "OverLevleCount";

    }
}

