using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class AudioParallel : AudioCompositeBase
    {

        private Dictionary<int, AudioActionBase> m_AudioDic = null;

        public AudioParallel(GameObject root, int nodeId, string name)
            : base(root, nodeId, name)
        {
            m_AudioDic = new Dictionary<int, AudioActionBase>();
        }

        private void AddAudio(int audioGuid, AudioActionBase action)
        {
            if (m_AudioDic != null && action != null)
            {
                m_AudioDic[audioGuid] = action;
            }
        }

        private AudioActionBase GetAduio(int audioGuid)
        {
            AudioActionBase action = null;
            if (m_AudioDic != null && m_AudioDic.ContainsKey(audioGuid))
            {
                action = m_AudioDic[audioGuid];
            }
            return action;
        }

        private void RemoveAudio(int audioGuid)
        {
            if (m_AudioDic != null && m_AudioDic.ContainsKey(audioGuid))
            {
                m_AudioDic.Remove(audioGuid);
            }
        }

        private void OnStartCall(int audioGuid, AudioActionBase action)
        {
            this.AddAudio(audioGuid, action);
            MsgDispatcher.SendMessage(GlobalEventType.AudioPlayStart, audioGuid);
        }

        private void OnEndCall(int audioGuid)
        {
            this.RemoveAudio(audioGuid);
            MsgDispatcher.SendMessage(GlobalEventType.AudioPlayEnd, audioGuid);
        }

        public override void Destroy()
        {
            m_AudioDic = null;
            GlobalUnityEngineAPI.Destroy(base.s_AudioRoot);
        }

        public override void Pause(bool isPause)
        {
            if (m_AudioDic != null && m_AudioDic.Count > 0)
            {
                foreach (KeyValuePair<int, AudioActionBase> pair in m_AudioDic)
                {
                    AudioActionBase action = pair.Value;
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
            if (mountNode == null)
                return;
            base.s_AudioRoot = mountNode;
            if (audioIds != null && audioIds.Length > 0)
            {
                for (int i = 0; i < audioIds.Length; i++)
                {
                    int audioId = audioIds[i];
                    this.PlayOneAudio(audioId);
                }
            }
        }

        public void PlayOneAudio(int audioId)
        {
            AudioActionType ptype = base.getAudioType(audioId);
            AudioActionBase audioAction = null;
            Action<int> startCall = delegate (int aid)
              {
                  OnStartCall(aid, audioAction);
              };
            audioAction = base.NewAudio(ptype, audioId, startCall, OnEndCall);
            audioAction.Play();
        }

        public override void Stop(int stopType=0)
        {
            if (m_AudioDic != null && m_AudioDic.Count > 0)
            {
                lock (m_AudioDic)
                {
                    List<int> Keys = new List<int>(m_AudioDic.Keys);
                    for (int i = 0; i < Keys.Count; i++)
                    {
                        var action = GetAduio(Keys[i]);
                        if (action != null && action.GetAudioSource() != null && (stopType == 0 || stopType == (int)action.S_VoiceType))
                        {
                            action.Stop();
                            m_AudioDic.Remove(i);
                        }
                    }
                }
                //m_AudioDic.Clear();
            }
            //this.Destroy();
        }
    }
}
