
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class LineEffect : BaseEffect
    {
        Vector3 targetPos;
        Vector3 starPos;
        public LineEffect(EffectData config, Vector3 pos, Vector3 rot,Vector3 StarPos, Vector3 TargetPos, Transform parent) : base(config, pos, rot, parent)
        {
            targetPos = TargetPos;
            starPos = StarPos;
        }

        protected override async void Play()
        {
            base.Play();
            //朝向目标
            gameObject.transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), targetPos - starPos);
            
            //计算起点
            Vector2 StartPos = GameFrameEntry.GetModule<EffectModule>().WorldToRectLocalPoint(starPos);
            //计算终点
            Vector2 EndPos = GameFrameEntry.GetModule<EffectModule>().WorldToRectLocalPoint(targetPos);

            float dis = Vector2.Distance(StartPos, EndPos);
            float ScaleX = gameObject.transform.localScale.x;
            float ScaleZ = gameObject.transform.localScale.z;
            //拉伸
            gameObject.transform.localScale = new Vector3(ScaleX, (dis / 256.0f), ScaleZ);
            gameObject.transform.position = starPos;

            gameObject.SetActive(m_isActive);
            if (Config.duration > 0) //有持续时间的自动停止
            {
                await new WaitForSeconds((float)Config.duration);
                Stop();
                onComplete?.Invoke(this);
            }
        }
    }
}
