package com.pdragon.ad;


import android.app.Activity;
import android.content.Context;


/**
 * @author demonhqm
 * 支付集成
 * 
 */
public class PayManager extends PayManagerTemplate {

	private static PayManager payManager;
	
	//当前购买商品的索引
	int mPayProductIdx = -1;
	
	public int getProductNo(String productId){
		return  -1;
	}
	
	public static PayManager getInstance(){
		if (payManager == null)
			payManager = new PayManager();
		return payManager;
	}
	
	
	/**初始化**/
	@Override
	public void init(Context ctx){		
		super.init(ctx);
	}	

	
	@Override
	public void resume() {
		super.resume();		
	}
	
	
	@Override
	public void pause() {
		super.pause();
	}
	
	@Override
	public void stop() {
		super.stop();
	}
	
	@Override
	public void exit() {

	}
	

	
	@Override
	public String buyProduct(String productID){
		setPayStatus(ADR_IAP_SUCCESS);
		return super.buyProduct(productID);
	}	
	
  

}
