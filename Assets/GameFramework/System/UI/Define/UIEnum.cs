using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    public enum EUINode
    {
        /// <summary>UI主界面节点,如:人物头像，右上角地小图，功能栏按钮等</summary>
        UIMain,
        /// <summary>UI窗口节点,如:商店,人物背包，任务面板，商城面板</summary>
        UIWindow,
        /// <summary>UI弹窗节点,如:商品购买弹出操作</summary>
        UIPopup,
        /// <summary>Tips节点,如:物品详细信息</summary>
        UITip,
        /// <summary>剧情节点</summary>
        UIAlert,
        /// <summary>剧情节点</summary>
        UIStory,
        /// <summary>系统消息节点</summary>
        UIMessage,
    }
    /// <summary>
    /// UI效果 与主 工程 EUIAnim保持一至
    /// </summary>
    public enum EUIAnim
    {
        None,       //无效果
        FadeIn,     //渐入
        FadeOut,   //渐出
        ScaleIn,    //缩放进入
        ScaleOut,  //缩放退出
        Customize,  //自定义
    }
}
