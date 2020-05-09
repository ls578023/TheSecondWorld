using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GameFramework
{
    public abstract class AudioCompositeBase
    {

        protected int s_NodeId = 0;

        protected GameObject s_AudioRoot = null;

        protected AudioCompositeBase(GameObject root, int nodeId, string name)
        {
            s_AudioRoot = Utils.NewGameObject(root, name);
            s_NodeId = nodeId;
        }


        public int GetNodeId()
        {
            return s_NodeId;
        }

        protected AudioActionBase NewAudio(AudioActionType ptype, int audioId, Action<int> startCall, Action<int> endCall)
        {
            AudioActionBase action = null;
            if (ptype == AudioActionType.Once)
                action = new AudioOnceAction(this.s_AudioRoot, audioId, startCall, endCall);
            else if (ptype == AudioActionType.Loop)
                action = new AudioLoopAction(this.s_AudioRoot, audioId, startCall, endCall);
            else if (ptype == AudioActionType.Fade)
                action = new AudioFadeAction(this.s_AudioRoot, audioId, startCall, endCall);
            else if (ptype == AudioActionType.LoopFade)
                action = new AudioLoopFadeAction(this.s_AudioRoot, audioId, startCall, endCall);
            return action;
        }

        public AudioActionType getAudioType(int audioId)
        {
            AudioActionType audioActionType = AudioActionType.Once;
            AudioData config = AudioManager.Instance.GetAudioData(audioId);
            //Debug.Log(config.id);
            if (config != null)
                audioActionType = (AudioActionType)config.Ptype;
            return audioActionType;
        }

        public abstract void Play(params int[] audioIds);

        public abstract void Play(GameObject mountNode, params int[] audioIds);

        public abstract void Stop(int StopType=0);//0-停止所有 1-停止音乐（背景音） 2-停止音效

        public abstract void Pause(bool isPause);

        public abstract void Destroy();



    }
}

