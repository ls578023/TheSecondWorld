using System;
using System.Collections.Generic;
using GameFramework;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace TheSecondWorld
{
    /// <summary>提示</summary>
    public class LanguageConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => id;
        /// <summary>
        /// id
        /// 例UI:LoginUI.btnLoing
        /// 例表:Test/id或Test/字段/id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 中文：0字
        /// </summary>
        public string zh_cn { get; set; }
        /// <summary>
        /// 英语
        /// </summary>
        public string en { get; set; }
    }
}
