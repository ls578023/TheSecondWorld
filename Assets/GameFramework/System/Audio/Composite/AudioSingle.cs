using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class AudioSingle : AudioCompositeBase
    {

        private AudioActionBase m_NowPlayAudio = null;

        private AudioActionBase m_WaitPlayAudio = null;


        public AudioSingle(GameObject root, int nodeId, string name)
            : base(root, nodeId, name)
        {

        }


        private void OnStartCall(int audioGuid)
        {
            MsgDispatcher.SendMessage(GlobalEventType.AudioPlayStart, audioGuid);
        }

        private void OnEndCall(int audioGuid)
        {
            m_NowPlayAudio = null;
            MsgDispatcher.SendMessage(GlobalEventType.AudioPlayEnd, audioGuid);
            if (m_WaitPlayAudio != null)
            {
                m_NowPlayAudio = this.m_WaitPlayAudio;
                this.m_NowPlayAudio.Play();
                this.m_WaitPlayAudio = null;
            }
        }

        public override void Destroy()
        {
            m_NowPlayAudio = null;
            GlobalUnityEngineAPI.Destroy(base.s_AudioRoot);
        }

        public override void Pause(bool isPause)
        {
            if (m_NowPlayAudio != null && m_NowPlayAudio.GetAudioSource() != null)
            {
                m_NowPlayAudio.Pause(isPause);
            }
        }

        public override void Play(params int[] audioIds)
        {
            int audioId = 0;
            if (audioIds != null && audioIds.Length > 0)
                audioId = audioIds[0];
            AudioActionType ptype = base.getAudioType(audioId);
            AudioActionBase audioAction = null;
            audioAction = base.NewAudio(ptype, audioId, OnStartCall, OnEndCall);
            if (m_NowPlayAudio != null)
            {
                this.m_WaitPlayAudio = audioAction;
                m_NowPlayAudio.Stop();
            }
            else
            {
                this.m_NowPlayAudio = audioAction;
                this.m_NowPlayAudio.Play();
            }
        }

        public override void Play(GameObject mountNode, params int[] audioIds)
        {

        }

        public override void Stop(int stopType=0)
        {
            if (m_NowPlayAudio != null)
            {
                m_NowPlayAudio.Stop();
            }
            this.Destroy();
        }
    }
}
