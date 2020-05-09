package com.wedobest.u3d.demo;

/**
 * Created by demonhqm on 2014-05-10.
 */
public class GameApp extends com.pdragon.game.GameApp{
    @Override
    public void onCreate() {
        //是否为正式版本:false为debug版本，注释掉后，默认为true，发布程序时一定要注释这一项。
        IS_PRO_MODE = false;
        super.onCreate();
    }
}
