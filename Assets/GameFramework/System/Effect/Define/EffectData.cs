using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{

    public class EffectData 
    {
        /// <summary>
        /// 特效Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 特效类型
        /// 0:基本特效
        /// 1:飞行特效
        /// 2:连线特效
        /// 3：场景特效
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 特效预制名
        /// </summary>
        public string res { get; set; }
        /// <summary>
        /// 持续时间（秒）
        /// =0:不自动销毁,自行处理
        /// >0 持续时间到了自动销毁
        /// </summary>
        public double duration { get; set; }
    }
}
