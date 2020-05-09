
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public class UIEffect: BaseEffect
    {        
        public UIEffect(EffectData config, Vector3 pos, Vector3 rot, Transform parent) : base(config, pos, rot,parent)
        {

        }

 
        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
