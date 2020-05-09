//
//  UnityToDBTAd.m
//  testOpenApp
//
//  Created by 开发 on 2019/9/27.
//  Copyright © 2019 开发. All rights reserved.
//


#import "UnityToDBTMethod.h"


char* DBTUnityStringCopy(const char* string)
{
    if (string == NULL){
        return NULL;
    }
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

//---------
//---------
//获取当前App包名
const char* getPackageName(){
    const char* resultValue = "com.test";
    return DBTUnityStringCopy(resultValue);
}
//获取当前版本号
const char* getVersionName(){
    const char* resultValue = "1.0";
    return DBTUnityStringCopy(resultValue);
}
//获取安装版本号
const char* getInstallVersion(){
    const char* resultValue = "1.0";
    return DBTUnityStringCopy(resultValue);
}
//判断当前版本是否为安装版本
bool isInstallVersion(){
    return false;
}
//获取App渠道号
const char* getAppChannel(){
    const char* resultValue = "f3";
    return DBTUnityStringCopy(resultValue);
}
//获取App渠道ID，用于游戏A/B测试。-1:未做A/B测试; 0：A/B测试的A包;  1:A/B测试的B包;....
int getAppChannelId(){
    return -1;
}
//设置渠道ID
void setAppChannelId(int channelId){
    //[UnityHelperIOS SetNowAppABModel:channelId];
}
//是否第一次启动
bool isFirstStartVer(){
    return true;
}
//App启动次数
int getStartNum(){
    return 1;
}
//获取首次运行时间
long getFirstInstallTime(){
    return 0;
}
//获取屏幕宽高
int getScreenWidth(){
    return 640;
}
int getScreenHeight(){
    return 960;
}
//获取系统语言
extern int getSystemLanguage(){
    /**
     *  ENGLISH = 0,
     CHINESE_CN,1      //简体中文
     CHINESE = CHINESE_CN  1,
     CHINESE_TW,   2   //繁体中文
     FRENCH, 3
     ITALIAN, 4
     GERMAN, 5
     SPANISH_ES,  6    //西班牙语
     SPANISH = SPANISH_ES,6
     SPANISH_MX,   7    //西班牙语(墨西哥)
     DUTCH,8
     RUSSIAN,9
     KOREAN,     10       //朝鲜语
     JAPANESE,  11      //日语
     JAPANESE_JP = JAPANESE, 11
     HUNGARIAN, 12
     PORTUGUESE, 13
     ARABIC, 14
     NORWEGIAN, 15
     POLISH, 16
     TURKISH, 17
     UKRAINIAN, 18
     ROMANIAN, 19
     BULGARIAN, 20
     HINDI,  21          //印度
     INDONESIAN, 22       //印尼
     VIETNAMESE,  23      //越南
     BENGALI 24
     */
    return 0;
}
//获取设备deviceid
const char* getDeviceId(){
    const char* resultValue = "abcd";
    return DBTUnityStringCopy(resultValue);
}
//获取UUID
const char* getUUID(){
    const char* resultValue = "abcd";
    return DBTUnityStringCopy(resultValue);
}
//是否wifi环境
bool isWifi(){
    return true;
}
//是否越狱
bool isRootSystem(){
    return false;
}
//获取设备地区
const char* getOsCountryCode(){
    const char* resultValue = "cn";
    return DBTUnityStringCopy(resultValue);
}
//获取App发布地区0：国内，1：国外, 2:国际版
int getPublishCountryCode(){
    return 2;
}
//获取设备信息
const char* getPhoneInfo(){
    const char* resultValue = "1.0";
    return DBTUnityStringCopy(resultValue);
}
//获取设备Mac地址
const char* getLocalMacAddress(){
    const char* resultValue = "1.0";
    return DBTUnityStringCopy(resultValue);
}
//获取IDFA
const char* getIDFA(){
    const char* resultValue = "abcd";
    return DBTUnityStringCopy(resultValue);
}
//获取设备IMSI
extern const char* getIMSI(){
    const char* resultValue = "";
    return DBTUnityStringCopy(resultValue);
}
//获取App打包时间
extern long getAppBuildTime(){
    return 0;
}
//跳转到设置界面
void showPreferences(){
    //[UnityHelperIOS GoToSetMenu];
}
//获取SD卡的可用大小，单位M
int getSDAvailableSizeStatic(){
    return 1000;
}
//获取App在SDcard中的的可写路径
const char* getGameSourcePath(){
    //const char* resultValue = [[UnityHelperIOS GetWritablePathBeforeEngineLauncher] UTF8String];
    return "";
}
//--
//展示Banner广告：pos: 1-Bottom 2-Top
void showBannerStatic(int pos){
    //showBannerStatic(pos,false);
}
//展示插屏，支持传参
void showBannerStatic(int pos,bool filterHighMemorySDK){
    int type = 0;
    if (filterHighMemorySDK == true) {
        type = 1;
    }
    //[UnityiOSMethod ShowBanner:pos type:type];
}
//隐藏Banenr广告
void hideBannerStatic(){
    //[UnityiOSMethod HiddenBanner];
}
//显示插屏广告
void showInterstitialStatic(const char *game){
    NSString *tagStr = @"game";
    if (game != NULL) {
        tagStr = [NSString stringWithUTF8String:game];
    }
    //[UnityiOSMethod ShowInterstitialWithTagName:tagStr];
}
//是否允许显示视频播放按钮
bool isShowVideoStatic(){
    return true;
}
//视频是否准备
bool isVideoReadyStatic(){
    //bool state = [UnityiOSMethod IsVideoReadyState];
    //NSLog(@"UnityToDBTMethod-isVideoReadyStatic:%i",state);
    return true;
}
//根据广告请求状态动态改变按钮显示状态
void setVideoButtonStatus(int status){
    NSLog(@"UnityToDBTMethod-setVideoButtonStatus:该方法不能由unity设置视频状态");
}
//播放视频
void showVideoStatic(int flag){
    //[UnityiOSMethod ShowVideoAd:flag];
}
//视频播放回调
void afterVideoCallback(int videoFlag, int result){
    NSLog(@"UnityToDBTMethod-afterVideoCallback:该方法不能由unity设置视频状态");
}

//事件统计
void onEvent(const char* event_id, const char* label){
    if (event_id == NULL || label == NULL) {
        NSLog(@"UnityToDBTMethod-onEvent-event_id-label:该方法参数错误");
        return;
    }
    NSString *event_idStr = [NSString stringWithUTF8String:event_id];
    NSString *labelStr = [NSString stringWithUTF8String:label];
    //[UnityHelperIOS OnEvent:event_idStr label:labelStr];
}
void onEvent(const char* event_id, int n){
    if (event_id == NULL ) {
        NSLog(@"UnityToDBTMethod-onEvent-event_id-n:该方法参数错误");
        return;
    }
    NSString *event_idStr = [NSString stringWithUTF8String:event_id];
    //[UnityHelperIOS OnEvent:event_idStr n:n];
}

//时长统计
void onEventDuration(const char* event_id, int ms) {
    if (event_id == NULL ) {
        NSLog(@"UnityToDBTMethod-onEventDuration-event_id-ms:该方法参数错误");
        return;
    }
    NSString *event_idStr = [NSString stringWithUTF8String:event_id];
    //[UnityHelperIOS OnEventDuration:event_idStr ms:ms];
}
void onEventDuration(const char* event_id, const char* label,int ms){
    if (event_id == NULL || label == NULL) {
        NSLog(@"UnityToDBTMethod-onEventDuration-event_id-label-ms:该方法参数错误");
        return;
    }
    NSString *event_idStr = [NSString stringWithUTF8String:event_id];
    NSString *labelStr = [NSString stringWithUTF8String:label];
    //[UnityHelperIOS OnEventDuration:event_idStr label:labelStr ms:ms];
}

//只上报一次事件
void onEventOnlyOnce(const char* event_id, const char* label) {
    if (event_id == NULL || label == NULL) {
        NSLog(@"UnityToDBTMethod-onEventoOnlyOnce-event_id-label:该方法参数错误");
        return;
    }
    NSString *event_idStr = [NSString stringWithUTF8String:event_id];
    NSString *labelStr = [NSString stringWithUTF8String:label];
    
    
    NSString *upKeyStr = [NSString stringWithFormat:@"OnlyOnce:%@_%@",event_idStr,labelStr];
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    NSString *shouldUpdateLocal = [defaults stringForKey:upKeyStr];
    // 如果成功上报了return掉
    if ([shouldUpdateLocal isEqualToString:@"1"]) {
        return;
    }
    [defaults setObject:@"1" forKey:upKeyStr];
    [defaults synchronize];
    
    
    //[UnityHelperIOS OnEvent:event_idStr label:labelStr];
}
void onEventOnlyOnce(const char* event_id, const char* label, int n){
    if (event_id == NULL || label == NULL) {
        NSLog(@"UnityToDBTMethod-onEventoOnlyOnce-event_id-label-n:该方法参数错误");
        return;
    }
    NSString *event_idStr = [NSString stringWithUTF8String:event_id];
    NSString *labelStr = [NSString stringWithUTF8String:label];
    
    NSString *upKeyStr = [NSString stringWithFormat:@"OnlyOnce:%@_%@",event_idStr,labelStr];
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    NSString *shouldUpdateLocal = [defaults stringForKey:upKeyStr];
    // 如果成功上报了return掉
    if ([shouldUpdateLocal isEqualToString:@"1"]) {
        return;
    }
    [defaults setObject:@"1" forKey:upKeyStr];
    [defaults synchronize];
    
    //[UnityHelperIOS OnEvent:event_idStr label:labelStr n:n];
}
//获取在线参数
const char* getOnlineConfigParams(const char* param){
    if (param == NULL) {
        return DBTUnityStringCopy([@"" UTF8String]);
    }
    NSString *paramStr = [NSString stringWithUTF8String:param];
    const char* resultValue = "";
    return DBTUnityStringCopy(resultValue);
}
//按版本号获取在线参数
const char* getOnlineConfigParamsByAppVer(const char* param){
    if (param == NULL) {
        return DBTUnityStringCopy([@"" UTF8String]);
    }
    NSString *paramStr = [NSString stringWithUTF8String:param];
    const char* resultValue = "";
    return DBTUnityStringCopy(resultValue);
}
//获取App设计模式0:关闭审核(审核通过)  1：正在审核
int getDesignModeStatic(){
    return 0;
}
//获取多玩法控制开关true:显示多玩法，false:不显示
bool showMoreGameStatic(){
    return true;
}
//是否显示分享按钮，用于部分市场在打包脚本中控制变量
bool isShowShare(){
    return true;
}
//获取分享方式0:ShareSDK分享 ;  1：DBT复制粘贴分享;  2：系统分享
int getShareMode(){
    return 2;
}
//分享App
void shareText(const char* title, const char* content, const char* url){
    
    NSString *titleStr = @"";
    NSString *contentStr = @"";
    NSString *urlStr = @"";
    if (title != NULL) {
        titleStr = [NSString stringWithUTF8String:title];
    }
    if (content != NULL) {
        contentStr = [NSString stringWithUTF8String:content];
    }
    if (url != NULL) {
        urlStr = [NSString stringWithUTF8String:url];
    }
    
}
//分享图片
void shareImage(const char* title, const char* content,const char* url, const char* imgFile){
    NSString *imgFileStr = @"";
    if (imgFile != NULL) {
        imgFileStr = [NSString stringWithUTF8String:imgFile];
    }
}
//是否显示登陆入口
bool showLogin(){
    return true;
}

//一键登录-1.登录，0登出
void loginComStatic(int inOrOut){
    //[UnityHelperIOS DBTLoginAction];
}
//是否显示反馈入口
bool isShowFeedback(){
    return true;
}
//显示反馈界面
void showFeedback(){
    
}
//检查是否需要弹出升级弹框
void showUpdateDalogStatic(){
    
}
//是否为欧盟用户,游戏中用于控制是否显示按钮
bool isRequestLocationInEeaOrUnknownStatic(){
    return false;
}
//显示GDPR用户协议
void showGDPRDialogStatic(){
    
}
//调用手机震动
void vibrateStatic(long milliseconds, int shakeLevel){
    //[UnityHelperIOS ActionVibrate:milliseconds shakeLevel:shakeLevel];
}
//预加载音效
void preloadEffect(const char* path){
    
}
//播放音效
int playEffect(const char* path, bool isLoop){
    return 0;
}
int playEffect(const char* path, bool isLoop, float pitch, float pan, float gain) {
    return 0;
}
//暂停播放
void resumeEffect(int soundId){
    
}
//恢复播放
void pauseEffect(int soundId){
    
}
//停止播放
void stopEffect(int soundId){
    
}
//获取音效音量
float getEffectsVolume(){
    return 0;
}
//设置音效音量
void setEffectsVolume(float volume){
    
}
//从内部缓冲区卸载预加载效果
void unloadEffect(const char* path){
    
}
//恢复所有音效
void resumeAllEffects(){
    
}
//停止所有音效
void stopAllEffects(){
    
}
//设置音效焦点
void setAudioFocus(bool isAudioFocus){
    
}
//释放所有音效
void end(){
    
}
//打印log
void LogD(const char* TAG, const char* msg){
    
    NSString *msgStr = @"";
    if (msg != NULL) {
        msgStr = [NSString stringWithUTF8String:msg];
    }
    //[UnityHelperIOS LogD:msgStr];
}
//显示Toast提示
void showToast(const char* msg){
    NSString *msgStr = @"";
    if (msg != NULL) {
        msgStr = [NSString stringWithUTF8String:msg];
    }
    //[UnityHelperIOS ShowToast:msgStr];
}
//展示退出弹框
void showExitDialog(){
    
}
//在新页面打开URL
void openUrl(const char* url){
    NSString *urlStr = @"";
    if (url != NULL) {
        urlStr = [NSString stringWithUTF8String:url];
    }
    //[UnityHelperIOS OpenUrl:urlStr];
}
//拷贝字符串到剪切板
void copyMsgToClipboard(const char* msg){
    NSString *msgStr = @"";
    if (msg != NULL) {
        msgStr = [NSString stringWithUTF8String:msg];
    }
    //[UnityHelperIOS CopyMsgToClipboard:msgStr];
}
//获取剪切板内容
extern const char* getClipboardMsg(){
    const char* resultValue = "";
    return DBTUnityStringCopy(resultValue);
}
//拷贝图片到手机相册
void copy2SystemDCIM(const char* copyFile){
    
    if (copyFile == NULL) {
        return;
    }
    NSString *msgStr = [NSString stringWithUTF8String:copyFile];
    //[UnityHelperIOS CopyImageToLibPhoto:msgStr];
}
//打开应用市场并跳转App详情页
void gotoAppStore(const char* packageName){
    //[UnityHelperIOS GotoAppStore];
}
//获取网络状态
const char* getNetworkTypeStatic(){
    const char* resultValue = "";
    return DBTUnityStringCopy(resultValue);
}
//是否显示隐私政策和用户协议
bool isShowPolicy(){
    return true;
}
//跳转到到用户协议
void gotoTermsServiceStatic(){
    //[UnityHelperIOS JumpToUserAgreementView];
}
//跳转到到隐私政策
void gotoPrivacyPolicyStatic(){
    //[UnityHelperIOS JumpToPrivacyPolicyView];
}
//打开三方分享App
void openApp(int appid){
    /*
    新浪微博:0
    微信：1
    QQ：2
    QQ空间：3
    Facebook：4
    Twitter：5
     */
    NSString *resultStr = @"";
    if (appid == 0) {
        resultStr = @"sinaweibo://";
    }else if (appid == 1) {
        resultStr = @"wechat://";
    }else if (appid == 2) {
        resultStr = @"mqq://";
    }else if (appid == 3) {
        resultStr = @"mqzone://";
    }else if (appid == 4) {
        resultStr = @"fbapi://";
    }else if (appid == 5) {
        resultStr = @"twitter://";
    }
    //[UnityHelperIOS OpenOtherApp:resultStr];
}
//判断三方分享App是否安装
extern bool isAppInstalled(int appid){
    
    NSString *resultStr = @"";
    if (appid == 0) {
        resultStr = @"sinaweibo://";
    }else if (appid == 1) {
        resultStr = @"wechat://";
    }else if (appid == 2) {
        resultStr = @"mqq://";
    }else if (appid == 3) {
        resultStr = @"mqzone://";
    }else if (appid == 4) {
        resultStr = @"fbapi://";
    }else if (appid == 5) {
        resultStr = @"twitter://";
    }
    return false;
}
//是否显示实名认证
bool isShowRealNameRegistration(){
    return false;
}
//是否显示联系方式
bool isShowContactInformation(){
    return false;
}
//获取App使用时长
int getDurationTimeStatic(){
    return 1000;
}
//是否需要更新资源
bool onlineResStatic(){
    return false;
}

//unity加载完成，主动通知移除开屏遮盖页面
void u3DGameStart(){
    //[UnityiOSMethod GameFinishStart];
}
//显示评价
void showEvaluate(){
    //[UnityHelperIOS ShowSysComment];
}
//获取分享链接
const char* getShareUrl(){
    const char* resultValue = "";
    return DBTUnityStringCopy(resultValue);
}
//获取使用分享方式 0-旧分享 1-新分享 2-系统分享
int getEvaluateMode(){
    return 2;
}
//是否显示评价
bool isShowEvaluate(){
    
    return true;
}

//-------------
//购买商品 （iOS端暂无此功能)
//prodId:商品ID
//prodName：商品名称
//body：商品描述信息
//cpOrderNo：CP端订单号
//amount：金额 元 小数点两位
void buyProduct(const char* prodId, const char* prodName, const char* body, const char* cpOrderNo, float amount){
    NSString *prodIdStr = @"";
    NSString *prodNameStr = @"";
    NSString *bodyStr = @"";
    NSString *cpOrderNoStr = @"";
    
    if(prodId != NULL){
        prodIdStr = [NSString stringWithUTF8String:prodId];
    }
    if(prodName != NULL){
        prodNameStr = [NSString stringWithUTF8String:prodName];
    }
    if(body != NULL){
        bodyStr = [NSString stringWithUTF8String:body];
    }
    if(cpOrderNo != NULL){
        cpOrderNoStr = [NSString stringWithUTF8String:cpOrderNo];
    }
    //[DBTForUnityAdExample BuyProduct:prodIdStr prodName:prodNameStr body:bodyStr cpOrderNo:cpOrderNoStr amount:amount];
}
//通知购买结果 （iOS端暂无此功能)
//dbtOrderNo:订单编号
//cpResultStatus:通知结果 "0"-未发货，"1"-发货成功，"2"-发货失败
//cpResultMsg:通知信息
void notifyProductResult(const char* dbtOrderNo, const char* cpResultStatus, const char* cpResultMsg){
    
    NSString *dbtOrderNoStr = @"";
    NSString *cpResultStatusStr = @"";
    NSString *cpResultMsgStr = @"";
    if(dbtOrderNo != NULL){
        dbtOrderNoStr = [NSString stringWithUTF8String:dbtOrderNo];
    }
    if(cpResultStatus != NULL){
        cpResultStatusStr = [NSString stringWithUTF8String:cpResultStatus];
    }
    if(cpResultMsg != NULL){
        cpResultMsgStr = [NSString stringWithUTF8String:cpResultMsg];
    }
    //[DBTForUnityAdExample NotifyProductResult:dbtOrderNoStr cpResultStatus:cpResultStatusStr cpResultMsg:cpResultMsgStr];
}
//==========
//==========
//发送消息到Unity
void DBTUnitySendMessage(const char *callback,const char *message){
    UnitySendMessage("DBTSDKManager", callback, message);
}

//是否是发布版
bool isDebugVersion(){
    return true;
}
//安装天数
int getDaysByFirstLaunch(){
    return 1;
}

int getGameBanHaoType(){
    return 0;
}
//点击“版号信息”按钮 展示界面
void showGameBanHao(){
    
}
//获取信息流广告状态  0:不展示广告  1：只有主图  2：主图+icon/标题 3:icon/标题(没有主图)
int getAdsStatusStatic(int scene,int tag){
    return 0;
}
//展示信息流广告
int showAdsStatic(int scene, int tag, int rootRect[], int pictureRect[], int footerRect[],int titleColor[], int actionBackgroundColor[], int actionColor[]){
    return 0;
}
//移除信息流广告
void removeAdsWidgetStatic(int tag){
    
}
//显示/隐藏信息流广告
void setAdsVisibleStatic(int tag, bool visible){
    
}

//游戏调用敏感词检测 检测出有多个敏感词用 , 号分割
const char* checkSensitiveWord(const char* key){
    return DBTUnityStringCopy("");
}
//创建二维码图片
const char* createQRcodeSyncStatic(const char* content, int widthPix, int heightPix,int padding){
    return DBTUnityStringCopy("");
}
//创建二维码异步执行
void createQRcodeAsyncStatic(const char* content, int widthPix, int heightPix, int padding){
    
}
//检测是否需要实名认证
void checkNeedCertification(){
    
}
//游戏调用实名认证
void showCertification(){
    
}
