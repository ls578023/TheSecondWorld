
namespace GameFramework
{
    public enum AudioMode
    {
        A2D = 1,

        A3D = 2,
    }

    public enum AudioActionType
    {
        /// <summary>
        /// 播放一次
        /// </summary>
        Once = 1,

        /// <summary>
        /// 循环播放
        /// </summary>
        Loop = 2,

        /// <summary>
        /// 淡入淡出
        /// </summary>
        Fade = 3,

        /// <summary>
        /// 循环播放(淡入淡出式)
        /// </summary>
        LoopFade = 4,
    }

    public enum VoiceType
    {
        /// <summary>
        /// 音乐 背景音
        /// </summary>
        Music = 1,

        /// <summary>
        /// 音效 
        /// </summary>
        Sound = 2,
    }
}
