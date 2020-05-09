using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld
{
    public class AirShip : MonoBehaviour
    {
        Vector2 startPos;
        Vector3 offset;
        Vector3 modelPos = new Vector3(0, 0, 5);
        int rotateRate = 8;
        float dot;

        void Start()
        {
            InputManager.AddTouchBeginListener(TouchBegin);
            InputManager.AddTouchMoveListener(TouchMove);
            InputManager.AddTouchEndListener(TouchEnd);
            MsgDispatcher.AddEventListener(TheSecondWorld.GlobalEventType.GetRotation, GetRotation);
            transform.localRotation = Quaternion.Euler(TaskSystem.TaskCtrl.I.GetRotation());
            transform.localPosition = modelPos;
            DontDestroyOnLoad(transform.parent.gameObject);
        }

        void TouchBegin(float x, float y)
        {
            startPos = new Vector2(x, y);
            dot = Vector3.Dot(Camera.main.transform.forward, transform.forward);
        }

        void TouchMove(float x, float y)
        {
            if (dot > 0)
                offset = new Vector3(startPos.y - y, x - startPos.x, 0);
            else
                offset = new Vector3(y - startPos.y, x - startPos.x, 0);
            Vector3 newRotation = transform.localRotation.eulerAngles - offset * Time.deltaTime * rotateRate;

            if (newRotation.x > 180)
                newRotation.x = newRotation.x - 360;
            newRotation.x = Mathf.Clamp(newRotation.x, -60, 60f);

            transform.localRotation = Quaternion.Euler(newRotation);
            MsgDispatcher.SendMessage(TheSecondWorld.GlobalEventType.NowRotation, transform.localRotation.eulerAngles);
            startPos = new Vector2(x, y);
        }

        void TouchEnd(float x, float y)
        {
            TaskSystem.TaskCtrl.I.SetRotation(transform.localRotation.eulerAngles);
        }

        void GetRotation(object[] arg)
        {
            MsgDispatcher.SendMessage(TheSecondWorld.GlobalEventType.NowRotation, transform.localRotation.eulerAngles);
        }

        private void OnDestroy()
        {
            MsgDispatcher.RemoveEventListener(TheSecondWorld.GlobalEventType.GetRotation, GetRotation);
        }

    }
}