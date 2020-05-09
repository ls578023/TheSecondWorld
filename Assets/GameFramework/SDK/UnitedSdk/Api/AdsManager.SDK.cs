//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK
{
public partial class AdsManager
{
    
    /// <summary>
    /// 显示Banner广告 pos: 1-Bottom 2-Top
    /// </summary>
    public  void  ShowBannerStatic(int pos){
        GameFramework.CLog.Log($"调用代理层showBannerStatic:pos[{pos}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.ShowBannerStatic(pos);
        #elif UNITY_IOS 
        AdsIOS.ShowBannerStatic(pos);
        #endif

    }


    /// <summary>
    /// 显示Banner广告，静态方法 true：过滤高内存消耗广告平台，false：不过滤
    /// </summary>
    public  void  ShowBannerStatic(int pos,bool filterHighMemorySDK){
        GameFramework.CLog.Log($"调用代理层showBannerStatic:pos[{pos}]filterHighMemorySDK[{filterHighMemorySDK}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.ShowBannerStatic(pos,filterHighMemorySDK);
        #elif UNITY_IOS 
        AdsIOS.ShowBannerStatic(pos,filterHighMemorySDK);
        #endif

    }


    /// <summary>
    /// 隐藏Banenr广告
    /// </summary>
    public  void  HideBannerStatic(){
        GameFramework.CLog.Log($"调用代理层hideBannerStatic:");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.HideBannerStatic();
        #elif UNITY_IOS 
        AdsIOS.HideBannerStatic();
        #endif

    }


    /// <summary>
    /// 显示插屏广告
    /// </summary>
    public  void  ShowInterstitialStatic(string game){
        GameFramework.CLog.Log($"调用代理层showInterstitialStatic:game[{game}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.ShowInterstitialStatic(game);
        #elif UNITY_IOS 
        AdsIOS.ShowInterstitialStatic(game);
        #endif

    }


    /// <summary>
    /// 是否允许显示视频播放按钮
    /// </summary>
    public  bool IsShowVideoStatic(){
        GameFramework.CLog.Log($"调用代理层isShowVideoStatic:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AdsAndroid.IsShowVideoStatic();
        #elif UNITY_IOS
        Result = AdsIOS.IsShowVideoStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 视频是否准备好了
    /// </summary>
    public  bool IsVideoReadyStatic(){
        GameFramework.CLog.Log($"调用代理层isVideoReadyStatic:");
        
        bool Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AdsAndroid.IsVideoReadyStatic();
        #elif UNITY_IOS
        Result = AdsIOS.IsVideoReadyStatic();
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 播放视频,flag 视频标志位  0:视频送道具   1:视频复活   2：视频解释玩法/功能 
    /// </summary>
    public  void  ShowVideoStatic(int  flag){
        GameFramework.CLog.Log($"调用代理层showVideoStatic: flag[{ flag}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.ShowVideoStatic( flag);
        #elif UNITY_IOS 
        AdsIOS.ShowVideoStatic( flag);
        #endif

    }


    /// <summary>
    /// 获取信息流广告状态:0不展示广告 1只有主图 2主图+icon/标题  3icon/标题(没有主图) scene默认2 id默认99
    /// </summary>
    public  int GetAdsStatusStatic(int scene,int id){
        GameFramework.CLog.Log($"调用代理层getAdsStatusStatic:scene[{scene}]id[{id}]");
        
        int Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AdsAndroid.GetAdsStatusStatic(scene,id);
        #elif UNITY_IOS
        Result = AdsIOS.GetAdsStatusStatic(scene,id);
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 显示信息流广告 :rootRect 电视框的坐标和长宽 [left, top, width, height] pictureRect 大图广告的坐标和长宽 footerRect 底部框的坐标和长宽  titleColor 底部框中标题/描述的字体颜色 [r, g, b], 0~255 actionBackgroundColor 底部框中按钮背景颜色 actionColor 底部框中按钮字体颜色 return 返回当前广告View的下标，用于移除和隐藏广告
    /// </summary>
    public  int  ShowAdsStatic(int  scene,int  id,int[] rootRect,int[] pictureRect,int[] footerRect,int[] titleColor,int[] actionBackgroundColor,int[] actionColor){
        GameFramework.CLog.Log($"调用代理层showAdsStatic:scene[{scene}]id[{id}]rootRect[{rootRect}]pictureRect[{pictureRect}]footerRect[{footerRect}]titleColor[{titleColor}]actionBackgroundColor[{actionBackgroundColor}]actionColor[{actionColor}]");
        
        int  Result=default;
        #if  JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID
        Result = AdsAndroid.ShowAdsStatic(scene,id,rootRect,pictureRect,footerRect,titleColor,actionBackgroundColor,actionColor);
        #elif UNITY_IOS
        Result = AdsIOS.ShowAdsStatic(scene,id,rootRect,pictureRect,footerRect,titleColor,actionBackgroundColor,actionColor);
        #endif
        GameFramework.CLog.Log($"结果为:[{ Result}]");
        return Result;

    }


    /// <summary>
    /// 移除信息流广告
    /// </summary>
    public  void  RemoveAdsWidgetStatic(int  index){
        GameFramework.CLog.Log($"调用代理层removeAdsWidgetStatic:index[{index}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.RemoveAdsWidgetStatic(index);
        #elif UNITY_IOS 
        AdsIOS.RemoveAdsWidgetStatic(index);
        #endif

    }


    /// <summary>
    /// 设置广告显示/隐藏
    /// </summary>
    public  void  SetAdsVisibleStatic(int index,bool visible){
        GameFramework.CLog.Log($"调用代理层setAdsVisibleStatic:index[{index}]visible[{visible}]");
        
        #if JumpSDK || UNITY_EDITOR
        #elif UNITY_ANDROID 
        AdsAndroid.SetAdsVisibleStatic(index,visible);
        #elif UNITY_IOS 
        AdsIOS.SetAdsVisibleStatic(index,visible);
        #endif

    }


}
}

