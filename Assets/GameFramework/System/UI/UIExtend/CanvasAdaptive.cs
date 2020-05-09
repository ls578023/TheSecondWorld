using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvas自适应
/// </summary>
[ExecuteInEditMode]
public class CanvasAdaptive : MonoBehaviour {

    // Use this for initialization
    private AspectRatioFitter aspect;
    private CanvasScaler canvas;
    float AspectWhRatio;
    public float whRatioConst;
    void Awake()
    {
        canvas = gameObject.GetComponent<CanvasScaler>();
        aspect = gameObject.GetComponent<AspectRatioFitter>();
        if (aspect == null)
            aspect = gameObject.AddComponent<AspectRatioFitter>();
        whRatioConst = canvas.referenceResolution.x / canvas.referenceResolution.y;
        GameFramework.AppSetting.IsPortrait= canvas.referenceResolution.x < canvas.referenceResolution.y;

        adaptive();
    }

    void adaptive()
    {
        float whRatio = Screen.width / (float)Screen.height;

        //WidthControlsHeight(whRatio);
        //设置默认控制参数
        if (AppSetting.IsPortrait)
        {
            if (whRatio <= whRatioConst)  //宽高比小于720*1280
                WidthControlsHeight(whRatio);
            else
                HeightControlsWidth(whRatio);
        }
        else
        {
            //横板情况下 一直使用宽控制高
            WidthControlsHeight(whRatio);
        }
        
        AspectWhRatio = whRatio;
    }
    /// <summary>
    /// 宽控制高
    /// </summary>
    /// <param name="whRatio"></param>
    void WidthControlsHeight(float whRatio)
    {
        canvas.matchWidthOrHeight = 0;
        aspect.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        aspect.aspectRatio = whRatio;
    }
    /// <summary>
    /// 高控制宽
    /// </summary>
    /// <param name="whRatio"></param>
    void HeightControlsWidth(float whRatio) 
    {

        canvas.matchWidthOrHeight = 1;
        aspect.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
        aspect.aspectRatio = whRatio;
    }
//#if UNITY_EDITOR
    void Update ()
    {
        float whRatio = Screen.width / (float)Screen.height;
        if(AspectWhRatio!= whRatio)
            adaptive();
    }
//#endif
}
