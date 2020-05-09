package com.wedobest.u3d.demo;

import com.pdragon.ad.AdsManagerTemplate;
import com.pdragon.ad.PayManager;
import com.pdragon.common.MainAct;
import com.pdragon.common.UserApp;
import com.pdragon.game.MainGameAct;
import com.unity3d.player.*;
import android.app.Activity;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.PixelFormat;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;

public class GameAct extends UnityActivity
{
    protected UnityPlayer mUnityPlayer; // don't change the name of this variable; referenced from native code

    // Setup activity layout
    @Override public void onCreate(Bundle savedInstanceState)
    {
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        super.onCreate(savedInstanceState);

        if (UserApp.curApp().isRestored()){        //如果被异常关闭
            AdsManagerTemplate.getInstance().initAds(this);
            PayManager.getInstance().init(this); //支付初始化
        }
        AdsManagerTemplate.getInstance().startRquestAds(this);
        PayManager.getInstance().init(this); //支付初始化
    }
}
