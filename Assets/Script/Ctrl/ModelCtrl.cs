using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

namespace TheSecondWorld
{
    public class ModelCtrl : ControlBase<ModelCtrl>, IControl
    {
        GameObject model;

        public void OnInit()
        {
            InitAirShip();

        }

        async void InitAirShip()
        {
            GameObject parent = GameObject.Find("ModelRoot");
            if (parent == null)
                parent = new GameObject("ModelRoot");
            model = await LoadHelper.LoadPrefab("AirShip");
            model.transform.SetParent(parent.transform, false);
            
        }

        public bool ActiveUpdate { get; set; }

        public void Updata(float elapseSeconds, float realElapseSeconds)
        {

        }

        public void OnDispose()
        {

        }
    }
}