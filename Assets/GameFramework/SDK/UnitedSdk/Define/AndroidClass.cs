//工具生成不要修改
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DBTSDK
{
public  class AndroidClass
{
    
        static AndroidJavaObject GameActHelper_javaClass;
        public static AndroidJavaObject GameActHelper { get 
            {
                if(GameActHelper_javaClass==null)
                    GameActHelper_javaClass = new AndroidJavaObject("com.pdragon.game.GameActHelper");
                return GameActHelper_javaClass;
            } 
        }   


        static AndroidJavaObject BaseActivityHelper_javaClass;
        public static AndroidJavaObject BaseActivityHelper { get 
            {
                if(BaseActivityHelper_javaClass==null)
                    BaseActivityHelper_javaClass = new AndroidJavaObject("com.pdragon.common.BaseActivityHelper");
                return BaseActivityHelper_javaClass;
            } 
        }   


        static AndroidJavaObject AdsManagerTemplate_javaClass;
        public static AndroidJavaObject AdsManagerTemplate { get 
            {
                if(AdsManagerTemplate_javaClass==null)
                    AdsManagerTemplate_javaClass = new AndroidJavaObject("com.pdragon.ad.AdsManagerTemplate");
                return AdsManagerTemplate_javaClass;
            } 
        }   


        static AndroidJavaObject Cocos2dxAdsViewHelper_javaClass;
        public static AndroidJavaObject Cocos2dxAdsViewHelper { get 
            {
                if(Cocos2dxAdsViewHelper_javaClass==null)
                    Cocos2dxAdsViewHelper_javaClass = new AndroidJavaObject("com.pdragon.game.feed.Cocos2dxAdsViewHelper");
                return Cocos2dxAdsViewHelper_javaClass;
            } 
        }   


}
}

