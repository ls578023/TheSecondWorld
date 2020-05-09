using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class AudioOnceAction : AudioActionBase
    {
        private float m_StartTime = 0;
        private float m_EndTime = 0;

        private bool m_IsPause = false;

        private int m_AndroidPluginsSoundId = 0;

        public AudioOnceAction(GameObject root, int audioId, Action<int> startCall, Action<int> endCall) : base(root, audioId, startCall, endCall)
        {
        }

        protected override void OnAwake()
        {
        }

        protected override void OnDispose()
        {
            AudioTools.DestroyAudioSource(base.s_audioSource);
            base.s_audioSource = null;
        }

        protected override void OnPause(bool isPause)
        {
            m_IsPause = isPause;
        }

        protected override async Task<AudioSource> OnPlay(AudioData audioConfig)
        {
            if (audioConfig != null)
            {
                var path = audioConfig.ResPath;
                var start_time = audioConfig.StartTime;
                var end_time = audioConfig.EndTime;
                var atype = (AudioMode)audioConfig.Atype;
                var vol = audioConfig.Vol <= 0 ? 1 : audioConfig.Vol;
                var vol_mul = GetSettingVolume();
                var name = "once_" + this.GetAudioGuid();

                this.m_StartTime = start_time;
                this.m_EndTime = end_time;
                AudioClip clip = await AudioTools.LoadAudioClip(path);
                //int soundID = 0;
                string filePath = "";
                float onceVolume = vol * vol_mul;

#if  !UNITY_EDITOR && UNITY_ANDROID//
                filePath = AudioManager.Instance.GetAndroidAudioID(audioConfig.id);
            if (!string.IsNullOrEmpty(filePath))
            {
                 m_AndroidPluginsSoundId = DBTSDK.SDKToolManager.Instance.PlayEffect(filePath, false, 1, 0, onceVolume);
                 //soundID = AndroidNativeAudio.play(fileID);
                 //soundID = AndroidNativeAudio.play(fileID, onceVolume, onceVolume);
                 //if (soundID != 0)
                 //   AndroidNativeAudio.setVolume(soundID, onceVolume, onceVolume);
            }
#endif
                if (clip != null)
                {
                    AudioSource source = AudioTools.CreateAudioSource(base.s_AudioRoot, name);
                    source.playOnAwake = false;
                    source.spatialBlend = atype == AudioMode.A2D ? 0 : 1;
                    if (atype == AudioMode.A3D)
                    {
                        //source.minDistance = audioConfig.MinDistance;
                        source.dopplerLevel = 0;
                    }
                    source.clip = clip;

#if !UNITY_EDITOR && UNITY_ANDROID
            if (m_AndroidPluginsSoundId != 0)
                source.volume = 0;
            else
                source.volume = onceVolume;
#else
                source.volume = onceVolume;
#endif

                    source.time = start_time;
                    source.loop = false;
                    source.Play();
                    return source;
                }
            }
            else
            {
                base.Stop();
            }
            return null;
        }

        protected override void OnStop()
        {
            if (this.s_audioSource != null)
            {
                if (m_AndroidPluginsSoundId != 0)
                {
                    DBTSDK.SDKToolManager.Instance.StopEffect(m_AndroidPluginsSoundId);
                }
                this.s_audioSource.Stop();
            }
            m_AndroidPluginsSoundId = 0;
            base.OnEndCall();
            base.Dispose();
        }

        protected override void RefreshAudioVolume(VoiceType voiceType, float vol)
        {
#if !UNITY_EDITOR && UNITY_ANDROID//
            float onceVolume = vol * s_Vol;
            if (m_AndroidPluginsSoundId != 0)
                DBTSDK.SDKToolManager.Instance.SetEffectsVolume(onceVolume);
                //AndroidNativeAudio.setVolume(m_AndroidPluginsSoundId, onceVolume, onceVolume);
            else
                base.RefreshAudioVolume(voiceType, vol);
#else
            base.RefreshAudioVolume(voiceType, vol);
#endif

        }

        protected override void OnUpdate()
        {
            if (base.s_audioSource != null)
            {
                if (this.m_EndTime <= 0)
                {
                    bool isPlaying = base.s_audioSource.isPlaying;
                    if (isPlaying == false && !m_IsPause)
                    {
                        base.Stop();
                    }
                }
                else
                {
                    float nowPlayTime = base.s_audioSource.time;
                    if (nowPlayTime >= this.m_EndTime)
                        base.Stop();
                }
            }
        }
    }
}