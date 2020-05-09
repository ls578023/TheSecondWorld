//
//  UnityToDBTAd.h
//  testOpenApp
//
//  Created by 开发 on 2019/9/27.
//  Copyright © 2019 开发. All rights reserved.
//


#ifndef UnityToDBTMethod_hpp
#define UnityToDBTMethod_hpp

extern "C"
{
    //获取当前App包名
    extern const char* getPackageName();
    //获取当前版本号
    extern const char* getVersionName();
    //获取安装版本号
    extern const char* getInstallVersion();
    //判断当前版本是否为安装版本
    extern bool isInstallVersion();
    //获取App渠道号
    extern const char* getAppChannel();
    //获取App渠道ID，用于游戏A/B测试。-1:未做A/B测试; 0：A/B测试的A包;  1:A/B测试的B包;....
    extern int getAppChannelId();
    //设置渠道ID(老用户升级后，往往会把老用户设置为-1)
    extern void setAppChannelId(int channelId);
    //是否第一次启动
    extern bool isFirstStartVer();
    //App启动次数
    extern int getStartNum();
    //获取首次运行时间
    extern long getFirstInstallTime();
    //获取屏幕宽高
    extern int getScreenWidth();
    extern int getScreenHeight();
    //获取系统语言
    extern int getSystemLanguage();
    //获取设备deviceid
    extern const char* getDeviceId();
    //获取UUID
    extern const char* getUUID();
    //是否wifi环境
    extern bool isWifi();
    //是否越狱
    extern bool isRootSystem();
    //获取设备地区
    extern const char* getOsCountryCode();
    //获取App发布地区0：国内，1：国外, 2:国际版
    extern int getPublishCountryCode();
    //获取设备信息
    extern const char* getPhoneInfo();
    //获取设备Mac地址
    extern const char* getLocalMacAddress();
    //获取IDFA
    extern const char* getIDFA();
    //获取设备IMSI
    extern const char* getIMSI();
    //获取App打包时间
    extern long getAppBuildTime();
    //跳转到设置界面
    extern void showPreferences();
    //获取SD卡的可用大小，单位M
    extern int getSDAvailableSizeStatic();
    //获取App在SDcard中的的可写路径
    extern const char* getGameSourcePath();
    //--
    //展示Banner广告：pos: 1-Bottom 2-Top
    extern void showBannerStatic(int pos,bool filterHighMemorySDK);
    //隐藏Banenr广告
    extern void hideBannerStatic();
    //显示插屏广告
    extern void showInterstitialStatic(const char *game);
    //是否允许显示视频播放按钮
    extern bool isShowVideoStatic();
    //视频是否准备
    extern bool isVideoReadyStatic();
    //根据广告请求状态动态改变按钮显示状态
    extern void setVideoButtonStatus(int status);
    //播放视频
    extern void showVideoStatic(int flag);
    //视频播放回调
    extern void afterVideoCallback(int videoFlag, int result);
    //--
    //事件统计
    extern void onEvent(const char* event_id, const char* label);
    //时长统计
    extern void onEventDuration(const char* event_id, const char* label,int ms);
    //只上报一次事件
    extern void onEventOnlyOnce(const char* event_id, const char* label, int n);
    //获取在线参数
    extern const char* getOnlineConfigParams(const char* param);
    //按版本号获取在线参数
    extern const char* getOnlineConfigParamsByAppVer(const char* param);
    //获取App设计模式0:关闭审核(审核通过)  1：正在审核
    extern int getDesignModeStatic();
    //获取多玩法控制开关true:显示多玩法，false:不显示
    extern bool showMoreGameStatic();
    //是否显示分享按钮，用于部分市场在打包脚本中控制变量
    extern bool isShowShare();
    //获取分享方式0:ShareSDK分享 ;  1：DBT复制粘贴分享;  2：系统分享
    extern int getShareMode();
    //分享App
    extern void shareText(const char* title, const char* content, const char* url);
    //分享图片
    extern void shareImage(const char* title, const char* content,const char* url, const char* imgFile);
    //是否显示登陆入口
    extern bool showLogin();
    //一键登录-1.登录，0登出
    extern void loginComStatic(int inOrOut);
    //是否显示反馈入口
    extern bool isShowFeedback();
    //显示反馈界面
    extern void showFeedback();
    //检查是否需要弹出升级弹框
    extern void showUpdateDalogStatic();
    //是否为欧盟用户,游戏中用于控制是否显示按钮
    extern bool isRequestLocationInEeaOrUnknownStatic();
    //显示GDPR用户协议
    extern void showGDPRDialogStatic();
    //调用手机震动
    extern void vibrateStatic(long milliseconds, int shakeLevel);
    //--
    //预加载音效
    extern void preloadEffect(const char* path);
    //播放音效
    extern int playEffect(const char* path, bool isLoop);
    //暂停播放
    extern void resumeEffect(int soundId);
    //恢复播放
    extern void pauseEffect(int soundId);
    //停止播放
    extern void stopEffect(int soundId);
    //获取音效音量
    extern float getEffectsVolume();
    //设置音效音量
    extern void setEffectsVolume(float volume);
    //从内部缓冲区卸载预加载效果
    extern void unloadEffect(const char* path);
    //恢复所有音效
    extern void resumeAllEffects();
    //停止所有音效
    extern void stopAllEffects();
    //设置音效焦点
    extern void setAudioFocus(bool isAudioFocus);
    //释放所有音效
    extern void end();
    //--
    //打印log
    extern void LogD(const char* TAG, const char* msg);
    //显示Toast提示
    extern void showToast(const char* msg);
    //展示退出弹框
    extern void showExitDialog();
    //在新页面打开URL
    extern void openUrl(const char* url);
    //拷贝字符串到剪切板
    extern void copyMsgToClipboard(const char* msg);
    //获取剪切板内容
    extern const char* getClipboardMsg();
    //拷贝图片到手机相册
    extern void copy2SystemDCIM(const char* copyFile);
    //打开应用市场并跳转App详情页
    extern void gotoAppStore(const char* packageName);
    //获取网络状态
    extern const char* getNetworkTypeStatic();
    //是否显示隐私政策和用户协议
    extern bool isShowPolicy();
    //跳转到到用户协议
    extern void gotoTermsServiceStatic();
    //跳转到到隐私政策
    extern void gotoPrivacyPolicyStatic();
    //打开三方分享App
    extern void openApp(int appid);
    //判断三方分享App是否安装
    extern bool isAppInstalled(int appid);
    //是否显示实名认证
    extern bool isShowRealNameRegistration();
    //是否显示联系方式
    extern bool isShowContactInformation();
    //获取App使用时长
    extern int getDurationTimeStatic();
    //是否需要更新资源
    extern bool onlineResStatic();
    //游戏start
    extern void u3DGameStart();
    //显示评价
    extern void showEvaluate();
    //获取分享链接
    extern const char* getShareUrl();
    //获取使用分享方式 0-旧分享 1-新分享 2-系统分享
    extern int getEvaluateMode();
    //是否显示评价
    extern bool isShowEvaluate();
    //-------------
    //购买商品 （iOS端此功能所需的支付库需要单独引入)
    //prodId:商品ID
    //prodName：商品名称
    //body：商品描述信息
    //cpOrderNo：CP端订单号
    //amount：金额 元 小数点两位
    extern void buyProduct(const char* prodId, const char* prodName, const char* body, const char* cpOrderNo, float amount);
    //通知购买结果 （iOS端此功能所需的支付库需要单独引入)
    //dbtOrderNo:订单编号
    //cpResultStatus:通知结果 "0"-未发货，"1"-发货成功，"2"-发货失败
    //cpResultMsg:通知信息
    extern void notifyProductResult(const char* dbtOrderNo, const char* cpResultStatus, const char* cpResultMsg);
    //--------------------------
    //--------------------------
    //发送消息到Unity
    extern void DBTUnitySendMessage(const char *callback,const char *message);
	
//是否是发布版
    extern bool isDebugVersion();
//安装天数
    extern int getDaysByFirstLaunch();
    
extern int getGameBanHaoType();
//点击“版号信息”按钮 展示界面
extern void showGameBanHao();
//获取信息流广告状态  0:不展示广告  1：只有主图  2：主图+icon/标题 3:icon/标题(没有主图)
extern int getAdsStatusStatic(int scene,int tag);
//展示信息流广告
extern int showAdsStatic(int scene, int tag, int rootRect[], int pictureRect[], int footerRect[],int titleColor[], int actionBackgroundColor[], int actionColor[]);
//移除信息流广告
extern void removeAdsWidgetStatic(int tag);
//显示/隐藏信息流广告
extern void setAdsVisibleStatic(int tag, bool visible);

//游戏调用敏感词检测 检测出有多个敏感词用 , 号分割
extern const char* checkSensitiveWord(const char* key);
//创建二维码图片
extern const char* createQRcodeSyncStatic(const char* content, int widthPix, int heightPix,int padding);
//创建二维码异步执行
extern void createQRcodeAsyncStatic(const char* content, int widthPix, int heightPix, int padding);
//检测是否需要实名认证
extern void checkNeedCertification();
//游戏调用实名认证
extern void showCertification();
}
//----------
#endif /* AAA_hpp */
