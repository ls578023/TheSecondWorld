using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class AudioLoopFadeAction : AudioActionBase
    {
        //最大音量，配置来的std_audio
        private float m_MaxVolume = 0;

        //淡入淡出时长ms
        private int m_FadeTime = 1000;

        //音效片段长度
        private float m_ClipLenght = 0;

        //是否在结束淡出
        private bool m_IsFadeEnding = false;

        //音效片段开始时间
        private float m_StartTime = 0;

        //音效片段结束时间
        private float m_EndTime = 0;

        private int m_FadeTimeId = 0;

        public AudioLoopFadeAction(GameObject root, int audioId, Action<int> startCall, Action<int> endCall) : base(root, audioId, startCall, endCall)
        {
        }

        protected override void OnAwake()
        {
        }

        protected override void OnDispose()
        {
            if (m_FadeTimeId != 0)
                GameFrameEntry.GetModule<TimeModule>().RemoveTime(m_FadeTimeId);
            AudioTools.DestroyAudioSource(base.s_audioSource);
            base.s_audioSource = null;
        }

        protected override void OnPause(bool isPause)
        {
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
                var name = "loopFade_" + this.GetAudioGuid();

                this.m_StartTime = start_time;
                this.m_EndTime = end_time;
                this.m_MaxVolume = vol * vol_mul;
                AudioClip clip = await AudioTools.LoadAudioClip(path);
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
                    source.volume = 0;
                    source.time = start_time;
                    source.loop = true;
                    source.Play();
                    this.m_ClipLenght = clip.length;
                    this.OnFade(source, true);
                    this.m_IsFadeEnding = false;
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
            if (!this.m_IsFadeEnding)
            {
                Action func = delegate ()
                {
                    if (this.s_audioSource != null)
                    {
                        this.s_audioSource.Stop();
                    }
                    base.OnEndCall();
                    base.Dispose();
                    this.m_IsFadeEnding = false;
                };
                this.m_IsFadeEnding = true;
                if (base.s_audioSource != null)
                    this.m_MaxVolume = base.s_audioSource.volume * GetSettingVolume();
                OnFade(base.s_audioSource, false, func);
            }
            else
                base.Dispose();
        }

        protected override void RefreshAudioVolume(VoiceType voiceType, float vol)
        {
            if (this.s_VoiceType == voiceType)
            {
                m_MaxVolume = vol * s_Vol;
            }
            base.RefreshAudioVolume(voiceType, vol);

        }

        /// <summary>
        /// 是否变大,不是则变小
        /// </summary>
        /// <param name="audio"></param>
        /// <param name="isHigh"></param>
        private void OnFade(AudioSource audio, bool isHigh, Action onFadeEnd = null)
        {
            if (audio != null)
            {
                TimeModule.CompleteHandler completeHandler = delegate (int id)
                {
                    GameFrameEntry.GetModule<TimeModule>().RemoveTime(id);
                    if (audio == null)
                        return;
                    if (isHigh)
                        audio.volume = this.m_MaxVolume;
                    else
                    {
                        audio.volume = 0;
                        if (onFadeEnd != null)
                            onFadeEnd();
                    }
                };

                TimeModule.EveryHandle everyHandle = delegate (int id, int time)
                {
                    if (audio == null)
                        return;
                    float volume = 0;
                    if (isHigh)
                        volume = this.m_MaxVolume * ((m_FadeTime - (float)time) / (float)this.m_FadeTime);
                    else
                    {
                        volume = this.m_MaxVolume * ((float)time / (float)this.m_FadeTime);
                    }
                    //Util.Log("aaaaaaaaaaaaaa-->", time, volume);
                    audio.volume = volume;
                };
                if (m_FadeTimeId != 0)
                {
                    GameFrameEntry.GetModule<TimeModule>().RemoveTime(m_FadeTimeId);
                }
                m_FadeTimeId = GameFrameEntry.GetModule<TimeModule>().SetCountDownByMillisecond(this.m_FadeTime, completeHandler, everyHandle);
            }
        }

        private void OnPingPangFade()
        {
            Action func = delegate ()
            {
                this.m_IsFadeEnding = false;
                OnFade(base.s_audioSource, true);
            };
            this.m_IsFadeEnding = true;
            OnFade(base.s_audioSource, false, func);
        }

        protected override void OnUpdate()
        {
            if (base.s_audioSource != null)
            {
                if (this.m_EndTime <= 0)
                {
                    bool isPlaying = base.s_audioSource.isPlaying;
                    float nowPlayTime = base.s_audioSource.time;
                    float fadeEndTime = this.m_ClipLenght - (float)this.m_FadeTime / 1000f;

                    //单播放时长小于等于fade的时长时做播放结束的fade
                    if (isPlaying && nowPlayTime >= fadeEndTime && !this.m_IsFadeEnding)
                    {
                        OnPingPangFade();
                    }
                }
                else
                {
                    float nowPlayTime = base.s_audioSource.time;
                    if (nowPlayTime >= this.m_EndTime - (float)this.m_FadeTime / 1000f)
                    {
                        OnPingPangFade();
                    }
                }
            }
        }
    }
}