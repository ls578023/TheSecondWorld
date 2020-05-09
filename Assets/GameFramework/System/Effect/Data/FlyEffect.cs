using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;


namespace GameFramework
{
    public class FlyEffect : BaseEffect
    {
        Vector3 targetPos;
        float flyTime;
        public FlyEffect(EffectData config, Vector3 pos, Vector3 rot,Vector3 TargetPos, float FlyTime, Transform parent) : base(config, pos, rot, parent)
        {
            targetPos = TargetPos;
            flyTime = FlyTime;
        }
        protected override void Play()
        {
            gameObject.SetActive(m_isActive);
            //朝向目标
            gameObject.transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), targetPos - gameObject.transform.position);
            gameObject.transform.DOMove(targetPos, flyTime).OnComplete(
                () =>
                {
                    Stop();
                    onComplete?.Invoke(this);
                }
                );
        }
    }
}
