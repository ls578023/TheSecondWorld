package com.wedobest.u3d.demo;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

import com.pdragon.common.UserApp;
import com.pdragon.game.WelcomeGameAct;



/**启动画面
 * @author demonhqm
 *
 */
public class StartAct extends WelcomeGameAct{
	
	
	@Override
	public void initSuccess() {
		//点击开屏广告跳转后还是回到当前Activity
//        UserApp.startActivity(this, MyGameAct.class, true, null);
//        UserApp.startActivity(this, UnityPlayerActivity.class, true, null);

		startActivityNoAnima(this, GameAct.class, true, null);
	}
	
	@Override
	public void initFail() {
		if (isFinishing()){
			return;
		}
//		UserApp.startActivity(this, MyGameAct.class, true, null);
//		UserApp.startActivity(this, UnityPlayerActivity.class, true, null);

		startActivityNoAnima(this, GameAct.class, true, null);
	}

	/**
	 * 跳转act不要任何过场动画
	 *
	 * @param ctx
	 * @param nextClass
	 * @param isFinish
	 * @param bundle
	 */
	public static void startActivityNoAnima(Context ctx, Class<?> nextClass, boolean isFinish, Bundle bundle) {
		Intent intent = new Intent(ctx, nextClass);
		if (bundle != null)
			intent.putExtras(bundle);
		ctx.startActivity(intent);
		if (isFinish) {
			((Activity) ctx).finish();
		}
		((Activity) ctx).overridePendingTransition(0, 0);
	}

}
