//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace DBTSDK{
 public class AdsIOS{
#if UNITY_IOS

     [DllImport("__Internal")]
    internal static extern void  showBannerStatic(int pos);


     [DllImport("__Internal")]
    internal static extern void  showBannerStatic(int pos,bool filterHighMemorySDK);


     [DllImport("__Internal")]
    internal static extern void  hideBannerStatic();


     [DllImport("__Internal")]
    internal static extern void  showInterstitialStatic(string game);


     [DllImport("__Internal")]
    internal static extern bool isShowVideoStatic();


     [DllImport("__Internal")]
    internal static extern bool isVideoReadyStatic();


     [DllImport("__Internal")]
    internal static extern void  showVideoStatic(int  flag);


     [DllImport("__Internal")]
    internal static extern int getAdsStatusStatic(int scene,int id);


     [DllImport("__Internal")]
    internal static extern int  showAdsStatic(int  scene,int  id,int[] rootRect,int[] pictureRect,int[] footerRect,int[] titleColor,int[] actionBackgroundColor,int[] actionColor);


     [DllImport("__Internal")]
    internal static extern void  removeAdsWidgetStatic(int  index);


     [DllImport("__Internal")]
    internal static extern void  setAdsVisibleStatic(int index,bool visible);




/// <summary>
/// 显示Banner广告 pos: 1-Bottom 2-Top
 /// </summary>
public static void  ShowBannerStatic(int pos){
        GameFramework.CLog.Log($"调用IOSshowBannerStatic: pos[{pos}]");
        showBannerStatic(pos);
}


/// <summary>
/// 显示Banner广告，静态方法 true：过滤高内存消耗广告平台，false：不过滤
 /// </summary>
public static void  ShowBannerStatic(int pos,bool filterHighMemorySDK){
        GameFramework.CLog.Log($"调用IOSshowBannerStatic: pos[{pos}]filterHighMemorySDK[{filterHighMemorySDK}]");
        showBannerStatic(pos,filterHighMemorySDK);
}


/// <summary>
/// 隐藏Banenr广告
 /// </summary>
public static void  HideBannerStatic(){
        GameFramework.CLog.Log($"调用IOShideBannerStatic: ");
        hideBannerStatic();
}


/// <summary>
/// 显示插屏广告
 /// </summary>
public static void  ShowInterstitialStatic(string game){
        GameFramework.CLog.Log($"调用IOSshowInterstitialStatic: game[{game}]");
        showInterstitialStatic(game);
}


/// <summary>
/// 是否允许显示视频播放按钮
 /// </summary>
public static bool IsShowVideoStatic(){
        GameFramework.CLog.Log($"调用IOSisShowVideoStatic: ");
        bool Reuslt =isShowVideoStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 视频是否准备好了
 /// </summary>
public static bool IsVideoReadyStatic(){
        GameFramework.CLog.Log($"调用IOSisVideoReadyStatic: ");
        bool Reuslt =isVideoReadyStatic();
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 播放视频,flag 视频标志位  0:视频送道具   1:视频复活   2：视频解释玩法/功能 
 /// </summary>
public static void  ShowVideoStatic(int  flag){
        GameFramework.CLog.Log($"调用IOSshowVideoStatic:  flag[{ flag}]");
        showVideoStatic( flag);
}


/// <summary>
/// 获取信息流广告状态:0不展示广告 1只有主图 2主图+icon/标题  3icon/标题(没有主图) scene默认2 id默认99
 /// </summary>
public static int GetAdsStatusStatic(int scene,int id){
        GameFramework.CLog.Log($"调用IOSgetAdsStatusStatic: scene[{scene}]id[{id}]");
        int Reuslt =getAdsStatusStatic(scene,id);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 显示信息流广告 :rootRect 电视框的坐标和长宽 [left, top, width, height] pictureRect 大图广告的坐标和长宽 footerRect 底部框的坐标和长宽  titleColor 底部框中标题/描述的字体颜色 [r, g, b], 0~255 actionBackgroundColor 底部框中按钮背景颜色 actionColor 底部框中按钮字体颜色 return 返回当前广告View的下标，用于移除和隐藏广告
 /// </summary>
public static int  ShowAdsStatic(int  scene,int  id,int[] rootRect,int[] pictureRect,int[] footerRect,int[] titleColor,int[] actionBackgroundColor,int[] actionColor){
        GameFramework.CLog.Log($"调用IOSshowAdsStatic: scene[{scene}]id[{id}]rootRect[{rootRect}]pictureRect[{pictureRect}]footerRect[{footerRect}]titleColor[{titleColor}]actionBackgroundColor[{actionBackgroundColor}]actionColor[{actionColor}]");
        int  Reuslt =showAdsStatic(scene,id,rootRect,pictureRect,footerRect,titleColor,actionBackgroundColor,actionColor);
        GameFramework.CLog.Log($"返回值为：[{Reuslt}]");
        return Reuslt;
}


/// <summary>
/// 移除信息流广告
 /// </summary>
public static void  RemoveAdsWidgetStatic(int  index){
        GameFramework.CLog.Log($"调用IOSremoveAdsWidgetStatic: index[{index}]");
        removeAdsWidgetStatic(index);
}


/// <summary>
/// 设置广告显示/隐藏
 /// </summary>
public static void  SetAdsVisibleStatic(int index,bool visible){
        GameFramework.CLog.Log($"调用IOSsetAdsVisibleStatic: index[{index}]visible[{visible}]");
        setAdsVisibleStatic(index,visible);
}


#endif
}
}

