

using System.Threading.Tasks;

namespace GameFramework
{

    internal abstract class ModulePriority
    {
        public const int DebuggerPriority = 10;

        public const int DataMondule = 11;

        public const int GameModuleLogicModule = 12;

        public const int SoundMondule = 13;

        public const int EffectModule = 20;

        public const int LangModule = 30;

        public const int UIModule = 40;

        public const int GameDataMondule = 50;

        public const int TimeModule = 60;

        public const int ObjectPoolPriority = 70;

        public const int ProcedurePriority = 90;

        public const int FsmPriority = 80;

        public const int PreLoadPriority = 100;

        public const int ConfigModule = 110;

        public const int AssetbundleModule = 120;

        public const int VersionMondule = 130;


    }


    internal abstract class GameFrameModule
    {
        internal bool IsInitialize;
        /// <summary>
        /// Module优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        internal virtual int Priority
        {
            get
            {
                return 0;
            }
        }
        internal abstract Task Initialize();

        /// <summary>
        /// 启动入口
        /// </summary>
        internal abstract void Start();


        /// <summary>
        /// 游戏模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);


        /// <summary>
        /// 游戏模块轮询。在Update后调用
        /// </summary>
        internal virtual void LateUpdate()
        {

        }

        /// <summary>
        /// 关闭并清理游戏框架模块。
        /// </summary>
        internal abstract void Shutdown();

    }
}
