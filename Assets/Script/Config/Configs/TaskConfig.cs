using System;
using System.Collections.Generic;
using GameFramework;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace TheSecondWorld
{
    /// <summary>特效配置</summary>
    public class TaskConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// 任务Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string des { get; set; }
    }
}
