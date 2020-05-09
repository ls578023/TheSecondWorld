using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using GameFramework;

namespace TheSecondWorld.UI.MainUI
{
    public partial class InfoUI : BaseUI
    {
        int itemNum = 4;
        List<InfoItem> itemList;
        List<Vector3> targetPosList;
        float offset;

        /// <summary>XXXX界面</summary>
        public InfoUI()
        {
            UINode = EUINode.UIWindow;     //UI节点
            OpenAnim = EUIAnim.None;      //UI打开效果
            CloseAnim = EUIAnim.None;   //UI关闭效果 
        }
        
        /// <summary>添加事件监听</summary>
        override protected void Awake()
        {
            MsgDispatcher.AddEventListener(TheSecondWorld.GlobalEventType.NowRotation, ShowRotation);
            offset = InfoItem.GetComponent<RectTransform>().sizeDelta.x;
            targetPosList = new List<Vector3>();
            for(int i=0;i< Grid.transform.childCount;i++)
            {
                targetPosList.Add(Grid.transform.GetChild(i).localPosition);
            }

            itemList = new List<InfoItem>();
            for (int i=0; i< itemNum;i++)
            {
                InfoItem item = new InfoItem();
                item.Instantiate(InfoItem, Root.transform);
                item.transform.localPosition = new Vector3(-offset, targetPosList[i].y, 0);
                item.transform.DOLocalMove(targetPosList[i], 1).SetEase(Ease.OutBack).SetDelay(i*0.1f);
                itemList.Add(item);
            }
            MsgDispatcher.SendMessage(TheSecondWorld.GlobalEventType.GetRotation);
        }
        
         /// <summary>刷新</summary>
        public override void Refresh()
        {
        }


        void ShowRotation(object[] arg)
        {
            Vector3 rotation = (Vector3)arg[0];

            itemList[0].SetData("旋转信息");
            itemList[1].SetData("X轴 : " + rotation.x.ToString("0.00"));
            itemList[2].SetData("Y轴 : " + rotation.y.ToString("0.00"));
            itemList[3].SetData("Z轴 : " + rotation.z.ToString("0.00"));

        }


        /// <summary>释放UI引用</summary>
        public override void Dispose()
        {
            MsgDispatcher.RemoveEventListener(TheSecondWorld.GlobalEventType.NowRotation, ShowRotation);
            itemList.Dispose();
        }
    }
}

