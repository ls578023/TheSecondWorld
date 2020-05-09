using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFramework
{
    public class DBTVideoBtn : Button
    {
        public enum BtnState
        {
            Hide,
            ActiveNotClick,
            Active,
        }
        BtnState state = BtnState.Active;
        public ButtonClickedEvent TSonClick { get; set; }

        protected override void Awake()
        {
            base.Awake();
            TSonClick = new ButtonClickedEvent();
        }

        public void SetButtonState(BtnState btnState)
        {
            state = btnState;
            Debug.Log(state);
            if (state == BtnState.ActiveNotClick)
            {
                this.gameObject.SetGray(true);
            }
            else
            {
                this.gameObject.SetGray(false);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TSonClick.RemoveAllListeners();
            TSonClick = null;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            switch (state)
            {
                case BtnState.Hide:
                    break;
                case BtnState.ActiveNotClick:
                    TSonClick?.Invoke();
                    break;
                case BtnState.Active:
                    base.OnPointerClick(eventData);
                    break;
            }
        }
        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);
            Debug.Log("DBTVideoBtn+OnSubmit");
        }
    }
}

