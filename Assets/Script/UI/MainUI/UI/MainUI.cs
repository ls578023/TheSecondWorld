using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using GameFramework;
using TheSecondWorld.TaskSystem;

namespace TheSecondWorld.UI.MainUI
{
    public partial class MainUI : BaseUI
    {
        int id = 1;
        int infoUI = 0;

        int effect1 = 0;
        int effect3 = 0;

        /// <summary>XXXX界面</summary>
        public MainUI()
        {
            UINode = EUINode.UIWindow;     //UI节点
            OpenAnim = EUIAnim.None;      //UI打开效果
            CloseAnim = EUIAnim.None;   //UI关闭效果 

        }
        
        /// <summary>添加事件监听</summary>
        override protected void Awake()
        {
            AddListener();
            Button1.AddClick(Button1Click);
            Button2.AddClick(Button2Click);
            Button3.AddClick(Button3Click);
            Button4.AddClick(Button4Click);
            //Button1.GetComponent<Image>().SetSprite("bg", "Common");
            Text3.text = GameFrameEntry.GetModule<LangModule>().Get("100");
            Refresh();
           
        }

        void AddListener()
        {
            MsgDispatcher.AddEventListener(GlobalEventType.RefreshTask, RefreshTask);
        }

        void RefreshText()
        {
            MsgDispatcher.RemoveEventListener(GlobalEventType.RefreshTask, RefreshTask);
        }

         /// <summary>刷新</summary>
        public override void Refresh()
        {
            TaskData data = TaskCtrl.I.GetTask(id);
            if (data != null)
                Show.text = data.taskDes + "  " + data.isFinish;
        }

        void Button1Click()
        {
            id = 1;
            if (effect1 == 0)
                effect1 = GameFrameEntry.GetModule<EffectModule>().Show(1, Button1.transform.position + Vector3.up * 400, new Vector3(-90, 0, 0), Button1.transform);
            else
            {
                GameFrameEntry.GetModule<EffectModule>().Close(effect1);
                effect1 = 0;
            }
            MsgDispatcher.SendMessage(GlobalEventType.TaskFinish, id);
            Refresh();           
        }

        void Button2Click()
        {
            id = 2;
            GameFrameEntry.GetModule<EffectModule>().Show(2, Button2.transform);
            MsgDispatcher.SendMessage(GlobalEventType.TaskFinish, id);
            Refresh();
        }

        void Button3Click()
        {
            id = 3;
            if (effect3 == 0)
                effect3 = GameFrameEntry.GetModule<EffectModule>().Show(3, Button3.transform);
            else
            {
                GameFrameEntry.GetModule<EffectModule>().Close(effect3);
                effect3 = 0;
            }
            MsgDispatcher.SendMessage(GlobalEventType.TaskFinish, id);
            if(infoUI == 0)
            {
                GameFrameEntry.GetModule<UIModule>().Show<InfoUI>();
                infoUI = 1;
            }
            else
            {
                GameFrameEntry.GetModule<UIModule>().Close<InfoUI>();
                infoUI = 0;
            }
            Refresh();
        }

        void Button4Click()
        {
            id = 4;
            Button4.transform.DOLocalMoveY(400f, 0.3f).SetLoops(2,LoopType.Yoyo);
            MsgDispatcher.SendMessage(GlobalEventType.TaskFinish, id);
            Refresh();
            ChangeState();
        }

        void ChangeState()
        {
            LoadHelper.LoadScene("PortraitScene");
            Screen.orientation = ScreenOrientation.Portrait;
        }

        void RefreshTask(object[] arg)
        {

        }

        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
        }
    }
}

