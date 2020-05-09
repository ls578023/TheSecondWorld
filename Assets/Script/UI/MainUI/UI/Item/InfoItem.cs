using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameFramework;

namespace TheSecondWorld.UI.MainUI
{
    public partial class InfoItem : BaseItem
    {
        public Action<InfoItem> onClick; //Item点击事件

        /// <summary>添加事件监听</summary>
        override protected void Awake()
        {
             gameObject.AddClick(self_Click);     //当前对象点击事件             
        }
        /// <summary>设置数据<param name="data"></param>
        public void SetData(string str)
        {
            Text.text = str;
        }
        /// <summary>刷新Item</summary>
        public override void Refresh()
        {
        }

        /// <summary>当前对象点击事件</summary>
        void self_Click()
        {
            onClick?.Invoke(this);
        }

        /// <summary>释放Item引用</summary>
        public override void Dispose()
        {
            onClick = null;
        }
    }
}

