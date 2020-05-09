//工具生成不要修改

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld.UI.MainUI
{
    public partial class InfoUI : BaseUI
    {
        private GameObject Grid;
        private GameObject InfoItem;
        private GameObject Root;
 
        /// <summary>初始化UI控件</summary>
        override protected void InitializeComponent()
        {
            Grid = Get("Grid");
            InfoItem = Get("InfoItem");
            Root = Get("Root");

        }
    }
}

