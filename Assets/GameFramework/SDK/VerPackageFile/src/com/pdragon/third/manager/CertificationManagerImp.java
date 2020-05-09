package com.pdragon.third.manager;

import android.app.Activity;
import android.content.Context;
import android.os.Handler;
import android.text.TextUtils;

import com.android.volley.DefaultRetryPolicy;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.VolleySingleton;
import com.android.volley.toolbox.StringRequest;
import com.google.gson.Gson;

import com.pdragon.ad.AdsContantReader;
import com.pdragon.ad.CertificationDelegate;
import com.pdragon.ad.PayManager;
import com.pdragon.common.BaseActivityHelper;
import com.pdragon.common.UserApp;
import com.pdragon.common.onlinetime.DBTOnlineTimeAgent;
import com.pdragon.common.onlinetime.DBTOnlineTimeCallback;
import com.pdragon.common.utils.AESCrypt;
import com.pdragon.common.utils.DBTUploadInstallInfo;
import com.pdragon.common.utils.EncryptUtil;
import com.pdragon.common.utils.TypeUtil;

import java.io.UnsupportedEncodingException;
import java.lang.ref.WeakReference;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import dbtcertification.CertificationManager;
import dbtcertification.DBTLogControl;
import dbtcertification.tools.CheckIdCardUtils;
import dbtcertification.ui.CustomPopUpWindow.Alert_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub11_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub12_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub13_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub1_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub21_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub2_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub3_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub5_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.Alert_Sub6_CustomBP;
import dbtcertification.ui.CustomPopUpWindow.CustomBPWindow;
import android.support.annotation.Keep;

@Keep
public class CertificationManagerImp implements com.pdragon.common.managers.CertificationManager {

    private WeakReference<Context> mContext = null;


    //==== Configs Start ====
    // 网络相关
    private static final String HostUrl = "http://rn.weplaybubble.com";
    //    private static final String HostUrl = "http://192.168.10.46:8080";
    private static final String RequestPath = "/RNServ/rn/verify.do";
    // Http请求解密密钥
    private static final String API_KEY = "20191126dbt";


    // 网络请求状态码
    private static final int REQUEST_FAIL_NO_INTERNET = -10;
    private static final int REQUEST_SUCCESS_DATA_ERROR = -11;
    private static final int REQUEST_FAIL = -1;
    private static final int REQUEST_SUCCESS = 0;


    // 网络请求默认提示
    private static final String RENZHEN_SUCCESS_TIPS = "认证成功";
    private static final String RENZHEN_FAIL_TIPS = "认证失败";
    private static final String RENZHEN_FAIL_NO_NETWORK_TIPS = "网络连接失败";

    private static final String RENZHEN_FAIL_SPECIAL_TIPS = "您输入的身份证号有误";
    //==== Config End ====


    //==== 数据埋点 Start ====
    private static final String EVENT_ID = "shimingrenzheng";
    private static final String EVENT_ID_LABEL_OPEN1 = "open1";
    private static final String EVENT_ID_LABEL_TIYAN = "tiyan";
    private static final String EVENT_ID_LABEL_QVRENZHEN = "qurenzheng";
    private static final String EVENT_ID_LABEL_FANHUI = "fanhui";
    private static final String EVENT_ID_LABEL_OPEN2 = "open2";
    private static final String EVENT_ID_LABEL_OPEN1ture = "open1ture";
    private static final String EVENT_ID_LABEL_OPEN2ture = "open2ture";
    //==== 数据埋点 End ====


    //==== Info ===


    private static final String UnderAgePay_title = "未成年人消费保护";
    private static final String UnderAgePay_tips = "本游戏禁止未成年人充值付费";

    private static final String UnderAgeTimeLimit_title = "未成年人限时提醒";
    private static final String UnderAgeTimeLimit_tips = "现在这个时间段是您的最佳休息时间，明天我们再见吧";


    private static boolean isControlShiWan = false;
    private static boolean isControlUnderage = false;
    private static boolean isShiMinClosable = false;

    /**
     * 是否试玩中的状态
     */
    private static boolean isShiWanIng = false;

    /**
     * 记录带输入框的弹框。
     */
    private Alert_CustomBP certificationInputAlert;
    /**
     * 约定的gameCert类型。
     */
    private int mGameCert;
    /**
     * 用于标记是否是试玩之前和试玩之后。
     */
    private boolean isAfterShiWan = false;
    /**
     * 记录identificationCard号码。
     */
    private String identificationCard = "";


        /**
     * 记录用户姓名。
     */
    private String userName;



    /**
     * 处理gameCert == 1的重试逻辑
     * 0： 往服务器发送校验，服务器超时，网络错误，都算成功，不管服务器返回结果成功还是失败，均为通过。
     * 1：重试1次，往服务器发送校验，服务器返回校验成功，不重试，其他情况均，弹出认证失败弹框。提示用户重新输入，再次输入后，往服务器发送校验，不管服务器结果成功还是失败，均为通过.
     */
    private int gameCertRetry = 1;
    private int retryCount = 1;


    /////////////// ----- CertificationManager interface Start ---- ///////////

    /**
     * 开始检测
     *
     * @param ctx 注册上下文
     */
    @Override
    public void startCheck(Context ctx) {
        mContext = new WeakReference<>(ctx);

        // 开启控制台
        showLogConsole();

        CertificationManager.getInstance().registerContext(ctx);
        int state = CertificationManager.getInstance().getCerttificationState();

        int gameCert = AdsContantReader.getAdsContantValueInt("GameCert", 0);

        gameCert = TypeUtil.ObjectToIntDef(BaseActivityHelper.getOnlineConfigParams("GameCert"), gameCert);


        if (!BaseActivityHelper.isInstallVersion(getContext())){
            BaseActivityHelper.setCertificationInfo(-2);
            return;
        }

        this.mGameCert = gameCert;

        // 重设控制状态
        resetControlState();

        // 是否读取GameCertRetry字段。
        if (getIsDBTRetryValid()) {
            // 设置gameCertRetry，重置retryCount;
            gameCertRetry = TypeUtil.ObjectToIntDef(BaseActivityHelper.getOnlineConfigParams("GameCertRetry"), 1);
            DBTLogControl.i(CertificationManager.TAG, "gameCertRetry=" + gameCertRetry);
            if (gameCertRetry == 0) {
                retryCount = 0;
            }
        }



        // 当认证信息未知或者认证失败，则调用验证

        if ((state == CertificationManager.CERTIFICATIONSTATE_UNKNOW) || state == CertificationManager.CERTIFICATIONSTATE_FAIL) {


            if (checkShiWan()) {

                // 走试玩逻辑
                if (CertificationManager.getInstance().isShiWanOutdated()) {
                    // 试玩过期了
                    DBTLogControl.i(CertificationManager.TAG, "试玩过期了，弹框验证");
                    isAfterShiWan = true;
                    goRenZhengStep2(isAfterShiWan);
                } else {
                    // 开始试玩
                    startShiWanLimit();
                }

            } else {
                DBTLogControl.i(CertificationManager.TAG, "初始化正常实名验证");
                // 走正常逻辑
                initCertification();
            }

        } else if (state == CertificationManager.CERTIFICATIONSTATE_SUCCESS_UNDERAGE) {
            DBTLogControl.i(CertificationManager.TAG, "开启未成年限制");

            // 开启未成年人限制
            startUnderAgeLimit();
        } else {
            DBTLogControl.i(CertificationManager.TAG, "实名已经认证通过");
        }

    }
	
	
	@Override
	public void startCheckFromPayLimit(Context ctx){
		startCheck(ctx);
		UserApp.showToastInThread(UserApp.curApp(), "未进行实名认证，不支持购买", false);
	 }
    

    /**
     * CertificationManager 的接口方法
     *
     * @param context
     */
    @Override
    public void showLimitPayDialog(Context context) {
        DBTLogControl.i(CertificationManager.TAG, "展示支付限制弹框");

        ((Activity) getContext()).runOnUiThread(new Runnable() {
            @Override
            public void run() {

                String title = UnderAgePay_title;
                String tips = UnderAgePay_tips;

                Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        return true;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                }, title, tips, getContext().getString(com.dbttools.dbtcertification.R.string.dbt_certification_alert_gotit));

                bp.showPopupWindow();
            }
        });

    }


    /////////////// ----- CertificationManager interface End ---- ///////////


    private void resetControlState() {
        switch (this.mGameCert) {
            case 10:
                isShiMinClosable = true;
                isControlShiWan = false;
                isControlUnderage = false;
                break;
            case 11:
                isShiMinClosable = false;
                isControlShiWan = true;
                isControlUnderage = false;
                break;
            case 12:
            case 13:
                isShiMinClosable = false;
                isControlShiWan = true;
                isControlUnderage = true;
                break;
            case 20:
            case 21:
                isControlUnderage = false;
                break;
            case 22:
                isControlUnderage = true;
                break;
            default:
                break;
        }
    }

    public void showLogConsole() {
        DBTLogControl.i(CertificationManager.TAG, "开启日志控制台");
        boolean isDebugMode = UserApp.isShowLog();
        CertificationManager.getInstance().showLogConsole(isDebugMode).setDebug(isDebugMode);
    }


    /**
     * 检测是否为试玩状态。
     *
     * @return true为试玩状态，false为非试玩状态。
     */
    private boolean checkShiWan() {
        return CertificationManager.getInstance().isShiWanActive();
    }


    /**
     * 试玩限制的回掉
     */
    private DBTOnlineTimeCallback shiWanLimit = new DBTOnlineTimeCallback() {
        @Override
        public void onLiveTimeSave(Long aLong, int i) {
            // 试玩结束，弹窗提醒
            if (!CertificationManager.getInstance().checkPlayState(CertificationManager.CERTIFICATION_CHECK_TYPE_SHIWAN, aLong, false)) {
                // 开启弹框提醒
                if (shiWanLimit == null) return;

                // 试玩结束
                isShiWanIng = false;

                // 移除监听
                DBTOnlineTimeAgent.instance().timeCallbcks.remove(shiWanLimit);
                //数据埋点
                BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_OPEN2);
                isAfterShiWan = true;

                ((Activity) getContext()).runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        goRenZhengStep2(isAfterShiWan);
                    }
                });

                shiWanLimit = null;
            }
        }
    };


    /**
     * 是否启用DBT retry，(GameCert == 1时需要获取参数来确定判断次数，默认1)
     *
     * @return true 启用DBT重试逻辑，false，不启用。
     */
    private boolean getIsDBTRetryValid() {
        return (mGameCert == 10) || (mGameCert == 11) || (mGameCert == 12);
    }


    /**
     * 是否启用限制弹框延时，目前仅针对第三方SDK
     *
     * @return true 启用延时，false，不启用延时。
     */
    private boolean getIsLimitAlertDelay() {
        return (mGameCert / 10) == 2;
    }


    /**
     * 限制弹框延时时间，默认2000豪秒
     *
     * @return 返回毫秒数
     */
    private int getIsLimitAlertDelayTime() {
        return 2000;
    }


    /**
     * 开启试玩限制
     */
    private void startShiWanLimit() {

        // 开始限制未成年人开关
        if (!isControlShiWan)return;

        // 正在试玩中的状态
        isShiWanIng = true;
        //获取App的运行总时长
        Long liveTime = TypeUtil.ObjectToLongDef(DBTOnlineTimeAgent.instance().getLiveTimeSp(), 0);
        CertificationManager.getInstance().startShiWanTime(liveTime);
        DBTOnlineTimeAgent.instance().addTimeCallbck(shiWanLimit);
    }


    private boolean isTimeAreaLimit() {
        // GameCert 为 1 2 3 均需要做时间段限制
        return mGameCert != 0;
    }


    /**
     * 未成年人限制的回掉
     */
    private DBTOnlineTimeCallback underAgeLimit = new DBTOnlineTimeCallback() {
        @Override
        public void onLiveTimeSave(Long aLong, int i) {
            if (underAgeLimit == null) return;
            // 游戏时间到，弹窗提醒
            if (!CertificationManager.getInstance().checkPlayState(CertificationManager.CERTIFICATION_CHECK_TYPE_UNDERAGE, aLong, isTimeAreaLimit())) {

                // 移除监听
                DBTOnlineTimeAgent.instance().timeCallbcks.remove(underAgeLimit);


                if (!CertificationManager.getInstance().isInPlayTimeArea() && isTimeAreaLimit()) {
                    // 可玩时间区域以外，只是在gamecert=2的时候，才会起作用
                    DBTLogControl.i(CertificationManager.TAG, "在可玩时间段8:00 -- 22:00 之外,禁止玩游戏");

                    ((Activity) getContext()).runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                                @Override
                                public boolean itemClick(int viewType, int position) {

                                    // 退出游戏
                                    ((Activity) getContext()).finish();
                                    return false;
                                }

                                @Override
                                public void viewDismiss() {

                                }
                            }, UnderAgeTimeLimit_title, UnderAgeTimeLimit_tips, "退出游戏");

                            bp.showPopupWindow();
                        }
                    });

                } else {
                    // 可玩时间区域以内，并且时间达到上限
                    // 开启弹框提醒
                    final int minutes = CertificationManager.getInstance().getCurrentLimitMinutes();


                    ((Activity) getContext()).runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                                @Override
                                public boolean itemClick(int viewType, int position) {

                                    // 退出游戏
                                    ((Activity) getContext()).finish();
                                    return false;
                                }

                                @Override
                                public void viewDismiss() {

                                }
                            }, minutes);

                            bp.showPopupWindow();
                        }
                    });
                }

                underAgeLimit = null;
            }
        }
    };


    /**
     * 禁用支付，经过讨论，不对支付做处理。
     */
    private void invalidPayment() {

    }


    /**
     * 开启未成年人限制
     */
    private void startUnderAgeLimit() {


        // 开始限制未成年人开关
        if (!isControlUnderage)return;

        DBTLogControl.i(CertificationManager.TAG, "开启未成年人限制");
        // 未成年人禁用支付
        invalidPayment();

        Long liveTime = TypeUtil.ObjectToLongDef(DBTOnlineTimeAgent.instance().getLiveTimeSp(), 0);

        if (!CertificationManager.getInstance().isInPlayTimeArea() && isTimeAreaLimit()) {


            // 启动判断
            // 可玩时间区域以外，只是在gamecert=2的时候，才会起作用
            DBTLogControl.i(CertificationManager.TAG, "在可玩时间段8:00 -- 22:00 之外,禁止玩游戏");

            ((Activity) getContext()).runOnUiThread(new Runnable() {
                @Override
                public void run() {

                    // 延迟显示弹框，针对第三方SDK登陆操作会抢占焦点
                    if (getIsLimitAlertDelay()) {

                        new Handler().postDelayed(new Runnable() {
                            @Override
                            public void run() {
                                ((Activity) getContext()).runOnUiThread(new Runnable() {
                                    @Override
                                    public void run() {

                                        Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                                            @Override
                                            public boolean itemClick(int viewType, int position) {

                                                // 退出游戏
                                                ((Activity) getContext()).finish();
                                                return false;
                                            }

                                            @Override
                                            public void viewDismiss() {

                                            }
                                        }, UnderAgeTimeLimit_title, UnderAgeTimeLimit_tips, "退出游戏");

                                        bp.showPopupWindow();

                                    }
                                });
                            }
                        }, getIsLimitAlertDelayTime());


                    } else {


                        Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                            @Override
                            public boolean itemClick(int viewType, int position) {

                                // 退出游戏
                                ((Activity) getContext()).finish();
                                return false;
                            }

                            @Override
                            public void viewDismiss() {

                            }
                        }, UnderAgeTimeLimit_title, UnderAgeTimeLimit_tips, "退出游戏");

                        bp.showPopupWindow();


                    }
                }
            });
        } else if (CertificationManager.getInstance().startUnderAgeTime(liveTime)) {
            // 未成年人需要开启时间限制
            DBTOnlineTimeAgent.instance().addTimeCallbck(underAgeLimit);

        } else {
            // 开启弹框提醒，时间过期
            final int minutes = CertificationManager.getInstance().getCurrentLimitMinutes();


            // 延迟显示弹框，针对第三方SDK登陆操作会抢占焦点
            if (getIsLimitAlertDelay()) {

                new Handler().postDelayed(new Runnable() {
                    @Override
                    public void run() {

                        Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                            @Override
                            public boolean itemClick(int viewType, int position) {

                                // 退出游戏
                                ((Activity) getContext()).finish();
                                return false;
                            }

                            @Override
                            public void viewDismiss() {

                            }
                        }, minutes);

                        bp.showPopupWindow();


                    }
                }, getIsLimitAlertDelayTime());
            } else {


                Alert_Sub6_CustomBP bp = new Alert_Sub6_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int viewType, int position) {

                        // 退出游戏
                        ((Activity) getContext()).finish();
                        return false;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                }, minutes);

                bp.showPopupWindow();
            }


        }

    }


    /**
     * 获取上下文
     *
     * @return 返回上下文
     */
    private Context getContext() {
        if (mContext.get() != null) return mContext.get();
        return null;
    }


    /**
     * 重试一次自减retryCount
     */
    private void decreaseRetryCount() {
        if (retryCount > 0) retryCount--;
    }


    /**
     * 初始化认证
     */
    private void initCertification() {

        DBTLogControl.i(CertificationManager.TAG, "GameCert=" + mGameCert);
        if (getIsDBTRetryValid()) {
            // 需要实名认证-调用多比特认证，身份证 + 姓名
            goRenZhengStep1();
        } else if (mGameCert == 13) {
            // 调用第三方实名认证， 身份证 + 姓名
            goRenZhengStep1();
        } else if ((mGameCert / 10) == 2) {
            // 调用第三方SDK认证
            thirdPartyRenZhen();
        } else {
            BaseActivityHelper.setCertificationInfo(-2);
        }
        // GameCert 为 0 不做任何处理
    }

/////////////// ----- Third Party  Certification Start ---- ///////////

    /**
     * 调用支付SDK认证
     */
    private void thirdPartyRenZhen() {
        DBTLogControl.i(CertificationManager.TAG, "调用支付SDK认证");
        PayManager.getInstance().startCertification(new CertificationDelegate() {
            @Override
            public void onSkip(int i) {
                DBTLogControl.i(CertificationManager.TAG, "调用支付SDK认证 skip");
                BaseActivityHelper.setCertificationInfo(-2);
            }

            @Override
            public void onFailed(int i, String s) {
                DBTLogControl.i(CertificationManager.TAG, "调用支付SDK认证 fail");
                BaseActivityHelper.setCertificationInfo(0);
            }

            @Override
            public void onSuccess(boolean b) {
                DBTLogControl.i(CertificationManager.TAG, "调用支付SDK认证 success");
                if (b) {
                    // 通知授权服务 未成年
                    CertificationManager.getInstance().setCertification(CertificationManager.CERTIFICATIONSTATE_SUCCESS_UNDERAGE);
                    // 通知游戏层 1 为未成年人
                    BaseActivityHelper.setCertificationInfo(1);
                    // Warning： 目前 第三方SDK 实名开启未成年人监控，后期待定
                    startUnderAgeLimit();
                } else {
                    // 通知授权服务 成年
                    CertificationManager.getInstance().setCertification(CertificationManager.CERTIFICATIONSTATE_SUCCESS);
                    // 通知游戏层 2 为成年人
                    BaseActivityHelper.setCertificationInfo(2);
                }

                // 身份信息传至服务器
                notifyCertificationSuccessInfo(null,null,!b);

            }
        },mGameCert);
    }


/////////////// ----- Third Party  Certification end ---- ///////////




    private boolean getDisplayShiWan(){
       //获取常量配置，并且不在试玩中状态
       return    AdsContantReader.getAdsContantValueBool("isDisplayCertificationShiWan", true) && !isShiWanIng;
    }

/////////////// -----  Certification Start ---- ///////////


    /**
     * 实名认证-步骤一
     */
    private void goRenZhengStep1() {

        //数据埋点
        BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_OPEN1);

        Alert_CustomBP alert;
        boolean displayShiWan = getDisplayShiWan();

        if (displayShiWan) {


            if (isShiMinClosable) {
                // 可关闭弹窗
                alert = new Alert_Sub12_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        if (i1 == 1) {
                            //数据埋点
                            BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_QVRENZHEN);
                            goRenZhengStep2(false);
                        } else {
                            //数据埋点
                            BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_TIYAN);
                            // 通知游戏层 -1 为试玩
                            BaseActivityHelper.setCertificationInfo(-1);
                            startShiWanLimit();
                        }
                        return true;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                });
            } else {
                // 不可关闭弹窗
                alert = new Alert_Sub1_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        if (i1 == 1) {
                            //数据埋点
                            BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_QVRENZHEN);
                            goRenZhengStep2(false);
                        } else {
                            //数据埋点
                            BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_TIYAN);
                            // 通知游戏层 -1 为试玩
                            BaseActivityHelper.setCertificationInfo(-1);
                            startShiWanLimit();
                        }
                        return true;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                });
            }


        } else {
            if (isShiMinClosable){
                // 没有试玩 , 可关闭按钮
                alert = new Alert_Sub13_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        if (i1 == 1) {
                            goRenZhengStep2(false);
                        }
                        return true;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                });

            }else {
                // 没有试玩，不可关闭按钮
                alert = new Alert_Sub11_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        if (i1 == 1) {
                            goRenZhengStep2(false);
                        }
                        return true;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                });
            }


        }

        alert.showPopupWindow();

    }


    /**
     * 实名认证-步骤二
     *
     * @param isAfterShiWan 是否试玩之后调用
     */
    private void goRenZhengStep2(boolean isAfterShiWan) {

        Alert_CustomBP alert;

        if (isAfterShiWan) {
            alert = new Alert_Sub21_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                @Override
                public boolean itemClick(int i, int i1) {
                    return false;
                }

                @Override
                public void viewDismiss() {

                }
            }, new Alert_Sub2_CustomBP.EditTextChangeListener() {
                @Override
                public void textChanged(int i, String s, int i1) {

                }

                @Override
                public void afterClickConfirm(String s, String s1) {

                    goRenZhengStep3(s, s1, new VerificationListener() {
                        @Override
                        public void verificationResult(int state, String msg) {
                            if (certificationInputAlert != null) {
                                if (state != REQUEST_FAIL_NO_INTERNET) {
                                    certificationInputAlert.dismiss();
                                    certificationInputAlert = null;
                                    goRenZhengStep4(state, msg);
                                } else {
                                    certificationInputAlert.doExtraThing(Alert_Sub2_CustomBP.EXTRA_STATE_CODE_RESET_ALERT_MSG, RENZHEN_FAIL_NO_NETWORK_TIPS);
                                }
                            }
                        }
                    });
                }

                @Override
                public void clickBack() {

                }
            }, null);
        } else {

            boolean displayShiWan = getDisplayShiWan();

            // 不显示试玩无返回按钮
            if (!displayShiWan) {
                alert = new Alert_Sub21_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        return false;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                }, new Alert_Sub2_CustomBP.EditTextChangeListener() {
                    @Override
                    public void textChanged(int i, String s, int i1) {

                    }

                    @Override
                    public void afterClickConfirm(String s, String s1) {
                        // 防重复点击
                        ((Alert_Sub2_CustomBP) certificationInputAlert).disableBtnsAfterClickNect();
                        goRenZhengStep3(s, s1, new VerificationListener() {
                            @Override
                            public void verificationResult(int state, String msg) {
                                if (certificationInputAlert != null) {
                                    if (state != REQUEST_FAIL_NO_INTERNET) {
                                        certificationInputAlert.dismiss();
                                        certificationInputAlert = null;
                                        goRenZhengStep4(state, msg);
                                    } else {
                                        // 解除重复点击
                                        ((Alert_Sub2_CustomBP) certificationInputAlert).enableBtnsAfterReset();
                                        certificationInputAlert.doExtraThing(Alert_Sub2_CustomBP.EXTRA_STATE_CODE_RESET_ALERT_MSG, RENZHEN_FAIL_NO_NETWORK_TIPS);
                                    }
                                }
                            }
                        });
                    }

                    @Override
                    public void clickBack() {

                    }
                }, "");
            } else {

                alert = new Alert_Sub2_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                    @Override
                    public boolean itemClick(int i, int i1) {
                        return false;
                    }

                    @Override
                    public void viewDismiss() {

                    }
                }, new Alert_Sub2_CustomBP.EditTextChangeListener() {
                    @Override
                    public void textChanged(int i, String s, int i1) {

                    }

                    @Override
                    public void afterClickConfirm(String s, String s1) {
                        // 防重复点击
                        ((Alert_Sub2_CustomBP) certificationInputAlert).disableBtnsAfterClickNect();
                        goRenZhengStep3(s, s1, new VerificationListener() {
                            @Override
                            public void verificationResult(int state, String msg) {
                                if (certificationInputAlert != null) {
                                    // 网络问题不跳转到下个界面
                                    if (state != REQUEST_FAIL_NO_INTERNET) {
                                        certificationInputAlert.dismiss();
                                        certificationInputAlert = null;
                                        goRenZhengStep4(state, msg);
                                    } else {
                                        // 解除重复点击
                                        ((Alert_Sub2_CustomBP) certificationInputAlert).enableBtnsAfterReset();
                                        certificationInputAlert.doExtraThing(Alert_Sub2_CustomBP.EXTRA_STATE_CODE_RESET_ALERT_MSG, RENZHEN_FAIL_NO_NETWORK_TIPS);
                                    }

                                }
                            }
                        });
                    }

                    @Override
                    public void clickBack() {
                        //数据埋点
                        BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_FANHUI);

                        certificationInputAlert.dismiss();
                        goRenZhengStep1();
                    }
                });
            }
        }

        certificationInputAlert = alert;
        alert.showPopupWindow();
    }


    /**
     * 实名认证-步骤三
     * 请求检验身份证号和姓名
     *
     * @param name           姓名
     * @param identification 身份证号码
     * @param lisener        回掉监听
     */
    private void goRenZhengStep3(String name, String identification, VerificationListener lisener) {
        requestCertification(name, identification, lisener);
    }


    /**
     * 实名认证-步骤四
     * 认证结果
     *
     * @param result 验证结果，0为认证成功， -1 以及其他为认证失败
     * @param msg
     */
    private void goRenZhengStep4(int result, String msg) {


        // 只有DBT重试有效才尝试拦截
        if (getIsDBTRetryValid()) {
            // 根据GameCertRetry拦截事件
            if (retryCount == 0) {
                DBTLogControl.i(CertificationManager.TAG, "DBTRetry生效，重置result和msg");
                result = REQUEST_SUCCESS;
                msg = RENZHEN_SUCCESS_TIPS;
            }
        }

        Alert_CustomBP alert;
        // 验证成功
        if (result == REQUEST_SUCCESS) {


            //数据埋点 分别在open1下和open2下通过认证的人数
            if (isAfterShiWan) {
                BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_OPEN2ture);
            } else {
                BaseActivityHelper.onEventOnlyOnce(EVENT_ID, EVENT_ID_LABEL_OPEN1ture);
            }


            alert = new Alert_Sub3_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                @Override
                public boolean itemClick(int i, int i1) {
                    return true;
                }

                @Override
                public void viewDismiss() {

                }
            });

            alert.showPopupWindow();

            //注册认证成功通知,注册前检查是否为未成年人,未成年开启时长度监控

            if (CheckIdCardUtils.checkIsUnderAgeByIdCard(identificationCard)) {
                DBTLogControl.i(CertificationManager.TAG, "认证为未成年人");
                CertificationManager.getInstance().setCertification(CertificationManager.CERTIFICATIONSTATE_SUCCESS_UNDERAGE);
                // 通知游戏层 1 为未成年人
                BaseActivityHelper.setCertificationInfo(1);
                // 开启未成年人限制
                startUnderAgeLimit();

                // 身份信息通知到服务器
                notifyCertificationSuccessInfo(userName,identificationCard,false);

            } else {
                DBTLogControl.i(CertificationManager.TAG, "认证为成年人");
                CertificationManager.getInstance().setCertification(CertificationManager.CERTIFICATIONSTATE_SUCCESS);
                // 通知游戏层 2 为成年人
                BaseActivityHelper.setCertificationInfo(2);


                // 身份信息通知到服务器
                notifyCertificationSuccessInfo(userName,identificationCard,true);

            }

            isAfterShiWan = false;


        } else {

            // 特殊处理
            if (result == 1) {
                msg = RENZHEN_FAIL_SPECIAL_TIPS;
            }
            //注册认证失败通知
            CertificationManager.getInstance().setCertification(CertificationManager.CERTIFICATIONSTATE_FAIL);
            // 通知游戏层 0 为认证失败
            BaseActivityHelper.setCertificationInfo(2);

            alert = new Alert_Sub5_CustomBP(getContext(), new CustomBPWindow.ItemClickListener() {
                @Override
                public boolean itemClick(int i, int i1) {
                    decreaseRetryCount();
                    goRenZhengStep2(isAfterShiWan);
                    return true;
                }

                @Override
                public void viewDismiss() {

                }
            }, msg);
            alert.showPopupWindow();
        }
    }


    /**
     * 验证结束接口
     */
    static interface VerificationListener {
        /**
         * 验证接口
         *
         * @param state 状态，0 验证成功,-1 验证失败
         * @param msg   消息
         */
        void verificationResult(int state, String msg);
    }


    private static class ResponseBean {
        private int code;
        private String msg;

        public int getCode() {
            return code;
        }

        public void setCode(int code) {
            this.code = code;
        }

        public String getMsg() {
            return msg;
        }

        public void setMsg(String msg) {
            this.msg = msg;
        }
    }


    /**
     * Network request
     *
     * @param name
     * @param identification
     */
    private void requestCertification(String name, String identification, final VerificationListener listener) {


        identificationCard = identification;
        userName = name;


        Response.Listener<String> ConfigCuccesslistener = new Response.Listener<String>() {
            public void onResponse(String data) {
                UserApp.LogD(data);
                String decryptData = getDecryptConfig(data);

                if (TextUtils.isEmpty(decryptData)) {
                    UserApp.LogD("实名认证接口请求返回数据异常");
                    if (listener != null) {
                        listener.verificationResult(REQUEST_SUCCESS_DATA_ERROR, "验证失败");
                    }
                } else {
                    Gson gson = new Gson();
                    ResponseBean bean = gson.fromJson(decryptData, ResponseBean.class);
                    if ((listener != null) && (bean != null)) {
                        UserApp.LogD("实名认证接口请求返回成功");
                        listener.verificationResult(bean.getCode(), bean.getMsg());
                    } else {
                        UserApp.LogD("实名认证接口请求返回数据异常");
                        listener.verificationResult(REQUEST_SUCCESS_DATA_ERROR, "验证失败");
                    }
                }
            }
        };

        Response.ErrorListener ConfigErrorListener = new Response.ErrorListener() {
            public void onErrorResponse(VolleyError paramVolleyError) {
                if ((listener != null)) {
                    UserApp.LogD("实名认证接口网络请求失败");
                    listener.verificationResult(REQUEST_FAIL_NO_INTERNET, paramVolleyError.getMessage());
                }
            }
        };


        HashMap<String, Object> hashMap = new HashMap();

        hashMap.put("appVer", UserApp.getVersionName(getContext()));
        hashMap.put("pkg", UserApp.getAppPkgName(getContext()));
        hashMap.put("orichnl", UserApp.curApp().getUmengChannel());
        hashMap.put("chnl", UserApp.getAppChannelStatic());
        hashMap.put("instVer", BaseActivityHelper.getInstallVersion(getContext()));
        hashMap.put("devId", UserApp.getDeviceId(false));

        hashMap.put("realName", name);
        hashMap.put("idCard", identification);

        int cert = getIsDBTRetryValid() ? 1 : 2;
        hashMap.put("cert", cert);

        String url = getURL(HostUrl + RequestPath, hashMap);


        StringRequest request = new StringRequest(url, ConfigCuccesslistener, ConfigErrorListener);
        request.setRetryPolicy(new DefaultRetryPolicy(5000, 3, 10000.0F));
        VolleySingleton.getInstance(getContext()).addToRequestQueue(request);
    }

    /**
     * 获取AES解密数据
     *
     * @param data 解密前的数据
     * @return 解密后的字符串
     */
    private String getDecryptConfig(String data) {
        if (!TextUtils.isEmpty(data) && !data.contains("appId")) {
            try {
                data = AESCrypt.decrypt(data, API_KEY, (String) null);
            } catch (Exception var3) {
                UserApp.LogD("getDecryptConfig e : " + var3.getMessage());
                var3.printStackTrace();
            }
        }

        return data;
    }



 /**
     * 通知服务器实名认证信息
     * @param name 姓名
     * @param identifier 身份证
     * @param isMature 是否成年人
     */
    void  notifyCertificationSuccessInfo(String name,String identifier,boolean isMature){
        HashMap<String,Object> map = new HashMap<>();
        map.put("name",name);
        map.put("idCard",identifier);
        map.put("isMature",isMature);
        Gson gson=new Gson();
        String json=gson.toJson(map);

        DBTUploadInstallInfo uploadInstallInfo = new DBTUploadInstallInfo(UserApp.curApp(), 4, json);
        uploadInstallInfo.upload(null);
    }



    private String getURL(String url, HashMap<String, Object> params) {

        String urls = null;
        StringBuilder urlBuild = new StringBuilder();
        urlBuild.append(url);
        StringBuilder encodeBuild = new StringBuilder();
        if (params != null && !params.isEmpty()) {
            urlBuild.append("?").append("apiVer").append("=").append("1.0").append("&").append("ENCODE_DATA").append("=");
            Iterator iterator = params.entrySet().iterator();

            while (iterator.hasNext()) {
                Map.Entry<String, Object> param = (Map.Entry) iterator.next();
                if (param.getValue() instanceof String) {
                    encodeBuild.append((String) param.getKey()).append('=').append(this.urlEncode((String) param.getValue()));
                } else {
                    encodeBuild.append((String) param.getKey()).append('=').append(param.getValue());
                }

                if (iterator.hasNext()) {
                    encodeBuild.append('&');
                } else {
                    urls = urlBuild.toString() + EncryptUtil.DBT_EasyEncrypt(encodeBuild.toString());
                }
            }
        }

        return urls;
    }


    private String urlEncode(String value) {
        try {
            if (value == null) {
                return "";
            }

            value = URLEncoder.encode(value, "UTF-8");
        } catch (UnsupportedEncodingException var3) {
        }

        return value;
    }


}
