using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public interface IControl
    {
        /// <summary>
        /// 激活UpData
        /// </summary>
        bool ActiveUpdate { get; set; }
        void OnInit();

        void Updata(float elapseSeconds, float realElapseSeconds);

        void OnDispose();
    }
}
