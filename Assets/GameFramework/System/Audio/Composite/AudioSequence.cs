using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class AudioSequence : AudioCompositeBase
    {

        private Queue<AudioActionBase> m_AudioQueue = null;

        private bool m_IsStop = false;

        public AudioSequence(GameObject root, int nodeId, string name)
            : base(root, nodeId, name)
        {
            m_AudioQueue = new Queue<AudioActionBase>();
        }



        private void EnqueueAudio(AudioActionBase action)
        {
            if (action != null)
                m_AudioQueue.Enqueue(action);
        }

        private AudioActionBase DequeueAudio()
        {
            AudioActionBase action = null;
            if (m_AudioQueue != null && m_AudioQueue.Count > 0)
            {
                action = m_AudioQueue.Dequeue();
            }
            return action;
        }

        private AudioActionBase PeekAudio()
        {
            AudioActionBase action = null;
            if (m_AudioQueue != null && m_AudioQueue.Count > 0)
            {
                action = m_AudioQueue.Peek();
            }
            return action;
        }

        private bool IsCanPlay()
        {
            bool isNeed = m_AudioQueue != null && m_AudioQueue.Count <= 0;
            return isNeed;
        }

        private void OnStartCall(int audioGuid)
        {
            MsgDispatcher.SendMessage(GlobalEventType.AudioPlayStart, audioGuid);
        }

        private void OnEndCall(int audioGuid)
        {
            if (!m_IsStop)
            {
                DequeueAudio();
            }
            MsgDispatcher.SendMessage(GlobalEventType.AudioPlayEnd, audioGuid);
            if (!m_IsStop)
            {
                AudioActionBase nextAction = PeekAudio();
                if (nextAction != null)
                    nextAction.Play();
            }
        }

        public override void Destroy()
        {
            m_AudioQueue = null;
            m_IsStop = false;
            GlobalUnityEngineAPI.Destroy(base.s_AudioRoot);
        }

        public override void Pause(bool isPause)
        {
            if (m_AudioQueue != null && m_AudioQueue.Count > 0)
            {
                foreach (AudioActionBase action in m_AudioQueue)
                {
                    if (action != null && action.GetAudioSource() != null)
                        action.Pause(isPause);
                }
            }
        }

        public override void Play(params int[] audioList)
        {
            if (audioList != null && audioList.Length > 0)
            {
                for (int i = 0; i < audioList.Length; i++)
                {
                    int audioId = audioList[i];
                    this.PlayOneAudio(audioId);
                }
            }
        }

        public override void Play(GameObject mountNode, params int[] audioIds)
        {

        }

        public void PlayOneAudio(int audioId)
        {
            AudioActionType ptype = base.getAudioType(audioId);
            if (ptype == AudioActionType.Loop)
                ptype = AudioActionType.Once;

            AudioActionBase audioAction = null;
            audioAction = base.NewAudio(ptype, audioId, OnStartCall, OnEndCall);
            if (IsCanPlay())
            {
                audioAction.Play();
            }
            EnqueueAudio(audioAction);
        }

        public override void Stop(int stopType=0)
        {
            if (m_AudioQueue != null)
            {
                m_IsStop = true;
                AudioActionBase action = null;
                while ((action = DequeueAudio()) != null)
                {
                    if (action != null && action.GetAudioSource() != null)
                        action.Stop();
                }
            }
            this.Destroy();
        }
    }
}
