/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld
{
    public partial class ConfigMgr
    {

        bool IsInitialize = false;
        /// <summary> 特效配置</summary>
        public readonly Dictionary<object, TaskConfig> dicTask = new Dictionary<object, TaskConfig>();
        /// <summary> 提示</summary>
        public readonly Dictionary<object, LanguageConfig> dicLanguage = new Dictionary<object, LanguageConfig>();
        /// <summary> 特效配置</summary>
        public readonly Dictionary<object, EffectConfig> dicEffect = new Dictionary<object, EffectConfig>();


        public async Task Initialize()
        {

            if (IsInitialize)
                return;
            ConfigRead.readConfig(dicTask);
            ConfigRead.readConfig(dicLanguage);
            ConfigRead.readConfig(dicEffect);

            //读取竖表配置

            //等待全部加载完再执行自定义解析
            await ConfigRead.waitLoadComplate();
            customRead();

            IsInitialize = true;
        }
    }
}
