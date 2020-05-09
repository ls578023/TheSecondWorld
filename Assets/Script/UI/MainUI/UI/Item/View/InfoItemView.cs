//工具生成不要修改
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld.UI.MainUI
{
    public partial class InfoItem : BaseItem
    {
        private Text Text;
 
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            Text = Get<Text>("Text");

        }
        /// <summary>初始化皮肤设置</summary>
        protected override void InitializeSkin()
        {

            
        }
    }
}

