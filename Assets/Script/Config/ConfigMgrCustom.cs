using GameFramework;
using System;
using System.Collections.Generic;

/*命名空间请更改成自己游戏得命名空间*/
namespace TheSecondWorld
{
    /// <summary>需要自定义解析方法写在这里面</summary>
    public partial class ConfigMgr
    {
        private static ConfigMgr _instance;
        public static ConfigMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigMgr();
                }
                return _instance;
            }
        }
       
        private void customRead()
        {

        }
    }

}

