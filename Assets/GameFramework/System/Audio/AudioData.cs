using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class AudioData
    {
        public int id { get; set; }


        /// <summary>
        /// 声音类型（1：Music
        /// 2：Sound）
        /// </summary>
        public VoiceType VoiceType = VoiceType.Sound;

        /// <summary>
        /// 音效类型(1:2D or 2:3D)
        /// </summary>
        public int Atype { get; set; }
        /// <summary>
        /// 播放类型(1:once,2:loop,3:fade,4:loopFade)
        /// </summary>
        public int Ptype { get; set; }
        /// <summary>
        /// 开始播放时间
        /// </summary>
        public int StartTime { get; set; }
        /// <summary>
        /// 结束播放时间
        /// </summary>
        public int EndTime { get; set; }
        /// <summary>
        /// 音量
        /// </summary>
        public float Vol { get; set; }
        /// <summary>
        /// 音效资源路径
        /// </summary>
        public string ResPath { get; set; }
    }
}
