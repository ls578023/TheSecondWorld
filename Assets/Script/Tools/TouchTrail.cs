using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld
{
    public class TouchTrail
    {
        RectTransform rect;
        RectTransform parent;
        Camera uiCamera;
        GameObject self;

        public TouchTrail()
        {
            LoadEffect();
            InputManager.AddTouchBeginListener(TouchBegin);
            InputManager.AddTouchMoveListener(TouchMove);
            InputManager.AddTouchEndListener(TouchEnd);
        }

        async void LoadEffect()
        {
            parent = GameFrameEntry.GetModule<UIModule>().UIRoot;
            uiCamera = GameFrameEntry.GetModule<UIModule>().UICamera;
            self = await LoadHelper.LoadPrefab("TouchTrail");
            self.transform.SetParent(parent);
            self.transform.SetAsLastSibling();
            rect = self.GetComponent<RectTransform>();
        }

        void TouchBegin(float x, float y)
        {
            self.SetActive(false);
            SetPos(x, y);
        }

        void TouchMove(float x, float y)
        {
            self.SetActive(true);
            SetPos(x, y);
        }

        void TouchEnd(float x, float y)
        {
            SetPos(x, y);
        }

        void SetPos(float x, float y)
        {
            Vector2 myPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, new Vector2(x, y), uiCamera, out myPos);
            rect.anchoredPosition = myPos;
        }

        public void TrailDestory()
        {
            //InputManager.RemoveTouchBeginListener(TouchBegin);
            //InputManager.RemoveTouchMoveListener(TouchMove);
            //InputManager.RemoveTouchEndListener(TouchEnd);
        }
    }

}