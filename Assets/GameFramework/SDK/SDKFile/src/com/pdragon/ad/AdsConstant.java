package com.pdragon.ad;

public class AdsConstant {
//--------------以下为广告平台ID， 101开始为国外广告平台ID-------------
//5.联想联运市场广告ID
	/**需要copy下面两行(蓝色)到AndroidManifest.xml当中
	<!-- 联想广告应用ID -->
	<meta-data android:name="lenovo.open.appid" android:value="1803130286894.app.ln" />
	*/
	//public final static String LENOVO_BANNAR_ID = "1803130286894.app.lnjepmzkoz";			//横幅ID(Banner ID)
	//public final static String LENOVO_INTERSTITIAL_ID = "1803130286894.app.lnjepmyowf";	//插屏ID	
	//public final static String LENOVO_SPLASH_ID = "1803130286894.app.lnjeqrxdbm";			//开屏ID
	//public final static String LENOVO_VIDEO_ID = "1803130286894.app.lnjepmzt59";			//视频ID
//6.酷派联运市场广告ID
	//public static final String COOLPAD_APPID = "COOLPAD_APPID";			//应用ID
	//public static final String COOLPAD_BANNERID = "COOLPAD_BANNERID";		//横幅ID(Banner ID)
	//public static final String COOLPAD_INSERTID = "COOLPAD_INSERTID";		//插屏ID
	//public static final String COOLPAD_SPLASHID ="COOLPAD_INSERTID";		//开屏ID
//200.google Admob广告平台ID
	//public final static String ADMOB_APP_ID = "";			//应用ID
	//public final static String ADMOB_BANNER_ID = "";		//横幅ID(Banner ID)
	//public final static String ADMOB_INSTER_ID = "";		//插屏ID
	//public final static String ADMOB_NATIVE_ID = "";		//信息流ID
	//public final static String ADMOB_VIDEO_ID = "";		//插屏ID
//301.wifi万能钥匙统计ID
	public static final String WIFI_APP_ID = "TD0243";	//appId
	public static final String WIFI_MD5_KEY = "egyxvxq8VSznuAsQM4DTTeRx3ARwEbzk";	//md5 key
	public static final String WIFI_AES_KEY = "wCsVtsM2rmTjsQTa";	//aes key
	public static final String WIFI_AES_IV = "Yd7zu24G85Bd3z48";	//aes iv
//302.ShareSDK分享聚合平台ID
	public final static String SHARE_SDK_KEY = "2450972366264";		//Appkey 
	public final static String SHARE_SDK_SECRET = "2d7f8ca900f0262671da750a967cdf23";	//App Secret 
//QQ BuglyID 
	public final static String BUGLY_ID="8ab5b68ab1";
	public final static String BUGLY_KEY="ce5eaee4-b476-4a55-84ec-5356903bd32e";
//wifi连尚广告
	//public final static String LIANSHANG_APPID =""; 
	//public final static String LIANSHANG_SECRET ="";
	//public final static String LIANSHANG_BANNER_ID ="";
	//public final static String LIANSHANG_INSERT_ID ="";
	//public final static String LIANSHANG_SPLASH_ID ="";
	//联想市场，头条视频广告ID
	//public final static String LENOVO_TTAD_VIDEO_APPID = "";//app ID
	//public final static String LENOVO_TTAD_VIDEO_ID = "";//VIDEO ID
	//头条统计ID
	//public static String TOUTIAO_APP_ID = "155040";//AppID
	//public static String TOUTIAO_APP_NAME = "cyxx";//AppName必须和后台保持一致
	//微信小程序分享
	public final static String ShareImageUrl = "";//小程序图片url
	public final static String ShareWxUserName = "gh_49e1349ccbc7";//小程序原始ID
	public final static String ShareWxPath = "NONE";//小程序Path，非特殊情况填写，否则都默认为NONE
	//Constant OrtherID
//---------------------以下为控制开关，直接复制粘贴-------------------------------------------------
	//是否显示开屏广告
	public final static boolean ShowSplashAd = false;
	// 开屏广告是否回调,没有回调一定要置为false
	public static boolean SplashAdCallback = false;
	// 是否显示Banner广告
	public final static boolean ShowBannerAd = false;
	// 是否显示插屏广告
	public final static boolean ShowInterstitialAd = false;
	// 是否显示退出插屏广告
	public final static boolean ShowExitInterstitialAd = false;
	// 是否显示push广告
	public final static boolean ShowPushAd = false;
	// 是否支持支付
	public final static boolean SupportPay = false;
	// 是否使用支付退出界面
	public final static boolean ShowPayExit = false;
	//是否显示反馈
	public final static boolean ShowFeedback = true;
	//是否显示分享
	public final static boolean ShowShare = true;
	//是否显示实名认证
	public final static boolean ShowRealNameRegistration = true;
	//分享方式 0:默认为ShareSDK分享；1:DBT分享
	public final static int ShareMode = 2;
	//google市场需要的隐私政策地址,留空则不显示
	public final static String PrivacyPolicyUrl = "";
	//是否显示防沉迷文字，没有配置的情况下为true
	public final static boolean IsBeiAnApp = true;
	//是否显示开屏界面上的版号信息，没有配置的情况下为true  add time 2017-08-04
	public final static boolean IsShowSplashBanhao = true;
	//是否显示购买去广告项
	public final static boolean ShowAdsBuyItems = false;
	//是否添加升级功能，默认添加该功能，只有推广渠道会在打包规则中添加。
	public final static boolean AppUpdate = false;
	//是否显示视频
	public final static boolean ShowVideoAd = false;
	//是否显示Banner关闭按钮
	public final static boolean ShowBannerCloseButton = false;
	//延迟显示Tab页面,单位为天
	public final static int ShowMoreTabDelay = 0;
	// 是否显示联系方式
	public final static boolean ShowContactInformation = true;
	//应用类App是否显示下载类功能
	public final static boolean AppSwitchTypeDownLoad = true;
	//应用类App是否显示政策类功能
	public final static boolean AppSwitchTypePolicy = true;
	//应用类App延迟显示可能违规类功能
	public final static int AppSwitchTypeWarn = 0;
	//锁屏广告开关
	public final static boolean ShowLockAd = false;
	//设置App地区，0：国内，1：国外
	public final static int AppLocation = 0;
	//Google App 评分 0关闭，1：跳转到应用市场，2：弹出评价框
	public final static int GooglePlayCommentType = 2;
	//Google App Like 0关闭，1：打开一个URL
	public final static int GooglePlayLikeType = 1; 
	//多玩法是否显示为“更多游戏” 默认为false
	public final static boolean isShowOneEntraceForMoreGame = false;
	//连尙读书内部推广App激活调用
	public final static boolean isWifiReaderApp = false;
	//是否显示30元的购买项
	public final static boolean isShowBuyPriceMoreThen30 = true;
	//是否为Oppo广告
	public final static boolean isOppoAdsSDK = false;
	//插屏间隔时间
	public final static int InterstititalIntervalTime = 1;
	//是否为多比特聚合广告App
	public static boolean isDobestJH = false;
	//添加GameID
	public static int GAMEID = 71;
	//Constant Switch
}
