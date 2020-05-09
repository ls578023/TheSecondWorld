//工具生成不要修改

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld.UI.MainUI
{
    public partial class MainUI : BaseUI
    {
        private Button Button1;
        private Button Button2;
        private Button Button3;
        private Button Button4;
        private Text Text3;
        private Text Show;
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            Button1 = Get<Button>("Button1");
            Button2 = Get<Button>("Button2");
            Button3 = Get<Button>("Button3");
            Button4 = Get<Button>("Button4");
            Text3 = Get<Text>("Text3");
            Show = Get<Text>("Show");

        }
    }
}

