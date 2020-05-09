package com.pdragon.ad;



public class PayConstant {

//---1:vivo支付	
	public static final String VIVO_CP_ID = "20160604160023104584";			//Cp-ID
	public static final String VIVO_APP_KEY = "0702f11a3362749662c89acb188dda7b";		//App-key
	public static final String VIVO_APP_ID = "56f8c3aabda9633ffa7874855569af7d";		//App-ID
	

//---2:oppo支付
	/**需要copy下面两行(蓝色)到AndroidManifest.xml当中
	<!-- oppo支付AppKey -->
	<meta-data android:name="app_key" android:value="9KLCdgfS1esKk0W0Os0So4SKc" />
	*/
	public static final String OPPO_APP_ID = "3657589";		//appid
	public static final String OPPO_APP_KEY = "9KLCdgfS1esKk0W0Os0So4SKc";		//appkey
	public static final String OPPO_APP_SECRET = "3fE37c8256ff92C53701e6d5297D5295";	//appsecret
	
//---3:华为支付
    public static final String HUAWEI_APP_ID = "100471725"; //APPID
    public static final String HUAWEI_CP_ID = "900086000021133624"; //CPID
    public static final String HUAWEI_PAY_ID = "900086000021133624"; //支付ID
    //支付公钥
    public static final String HUAWEI_PAY_RSA_PUBLIC = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiAFCpWBMDjzQsJEA9NreKGCyI6PxoRuz9r1J/SibXdepmMY2Ljsgd1Uu56iP3JX7tqe8z3jsTcooshK3mz4P723JIyIvmNl5X3Vi+aqwulr3uJl6sWZqcTfB3OYpc+Gj1vArsrXLZboXndOdqz2HbcfLE5NOKjpwYXHrTIW3ASYdbwXRWhuFEc/p6MPGhyZxbdamCtMTX8FKNp/IEtwXg7EIcPk+QaSVY4AvnueRsaT3rlMvHQ4nWqHw6xqz1ymSO2gcm5XQO943KuR2HWgYc3zkSyyTJSP12mtc3z3ehIPHShFEMAhlIWgWKZ+1XrH7QCcdaWNnJmT/5yfW5yG5FQIDAQAB";
    //支付私钥
    public static final String HUAWEI_PAY_RSA_PRIVATE = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCIAUKlYEwOPNCwkQD02t4oYLIjo/GhG7P2vUn9KJtd16mYxjYuOyB3VS7nqI/clfu2p7zPeOxNyiiyErebPg/vbckjIi+Y2XlfdWL5qrC6Wve4mXqxZmpxN8Hc5ilz4aPW8Cuytctluhed052rPYdtx8sTk04qOnBhcetMhbcBJh1vBdFaG4URz+now8aHJnFt1qYK0xNfwUo2n8gS3BeDsQhw+T5BpJVjgC+e55GxpPeuUy8dDidaofDrGrPXKZI7aBybldA73jcq5HYdaBhzfORLLJMlI/Xaa1zfPd6Eg8dKEUQwCGUhaBYpn7VesftAJx1pY2cmZP/nJ9bnIbkVAgMBAAECggEAb9pxgGdJRbBYhc5LthTG/vg/qbYshC6vfG7DChS6apxym/XwG2d/VQVWtSlZX7ZuNROQ4iT2Wye2/nUMUf3hxy1Ibb+w24mIG60EQIdUH3+vGkAHuxya0BBmPRGB6A8b2yrdFXYUGM7Km3+tvWa2GeBmMOxGLc6wHEQ/m0ihcKwx+uGB1koSdSJuNOv+IkfP6yK82J6GPUaZu0eR0x5X3ymzPxhKm66jkvla5mQYyRSut+R8vIq6ilECAD1qHtwYe+eFf45lm4c/w8c9o9F67p5TbdKvMhc15Rq+pJFWxEX3zG2WYtXmfIHcj5sZ7En/F3Tt+MPLr43cj6n3UN+EYQKBgQD80HT7/cAk3mrSWDLgNm7OlgSdbfNJdWjFS8nHbA4E4Saj15Nk7T8n06csUEUnLvhlLr2T1zp38TChRSrJcotUW14ktIZl6DvPtHJHGmyALn9Hv7HTwlZ/WeRVhDGFJgM7nCWo/s7GirQhTNsdw5kok40xdl0mzLYXcW5Wb17COQKBgQCJt/4wrFr0SwZZhC2Ue5QEic87QGgfY/5xSt3+5BV1rmIpZWfpZJ1wZ23B65hkvSKQOa7maWPOsovWFcspSn+5Rm+h1ZKVvk1Ys2I5EPq3oXvPDTg9D+Qm5GFptGOFqUbXk5TzQMiSeG/LV/v61Uj1ZYXlLYRjxq88N4yuAgr9vQKBgQDHae48rkUXT9FWpXdL3+O7VB3Dx+x07wV7SnKYuKS+OJJv/iUnIPQGCC7/Bznk1Gnd9eZXbReTcE35h0Nertkz6vXxYev5ChvTk/PoHfGjkgmXoJ819z6fKVqwEZu1+Ovn4xzZllOny9SVx7e2XIi8ttZSQ5jqd+LRzwlHaIWIEQKBgEiuznypuicMIYHE/LlMr0Xl5XUD/O/0OilYtej8P0coja4DUuAetejI7WhXCsq/9ynfB6ubG43PNXX7comMQ/RodCoZKY/WK8QQiFpN18RqeYlBFNlFNchA6Bh9l7jHKhhET2xCWJjU70uGV/p97k91EUFu0FxIU9uYBasgcOSRAoGBAKTXqM1Q+xiv08nKIUjqdddrzufZ/E3798nHk94zxLMa682QfWo+Krk3RC/6p1cg+Zdj1WfNWgMCQLod5wiC79gfDoDP0RNeC5ilwkm3MH1pIdOYd8m2wPsFKU2m9B2rOMQhcZTMU7JIfPjOfhxE3a+E3OUrUt1nxfyi8fvd2x6T";
    //浮标密钥,改成了游戏私钥
    public static final String HUAWEI_BUO_SECRET = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCOn0PrnvB5w/n7ezpoHB82mwAYV6KYqnsxEw/OetSVz1afLEYg1gHkBTD7qst2vNX9PRIWg8WVPjZtEQ6ZPTEAtuEIGrfd2kmfR5Q1BxEu7txdaffB8hw54tW4qqbwUHXbDfoxh9cBdWEH7x9DU+w57ambV6h72xfocIN7rSZHjUremG6u0pSbLGKMvtrfYw/+gbRTNhQN0/4vKiJ4k85P7bdFqABOF+oHNEOHH6pIIfq7NoaKfTyxK7HRVl+wqiaWALsUElj1VUiHjN63koMLIAjgJhR9AgF6y687Tw2kgOb0xdbVIfL6gX5Jw5qrGwHkwHu3NU4DrXzG2E+SeJmbAgMBAAECggEAdgHNnx2pd/bx1mUF1LLAGNxzXbDVNmCJAff1sjDryPD/iUCG3hB1DAhHmVSeeSaSILDxUHPDflM1kVT1sVFyRFcNJzmEPSqqlAIT3TR1wVGeQCS3/V+ofzTzBZMeAeVxPsj35ReyjOBkYr42Le04nk3JcmE7ZQ/JXmgKQsDn1XcTzvvZhPTI5JbK+Ds5jhbx34WKDdL8irwN/jtd0/OZfvsoBPhNerMHvq7ttOOPJ0n7lyq8mQp9yAEgvpUr2Lc9STtGdP5QltQ1tfpLlwJHoADQ22x5K0riK6fdy7jC2/raFHwIE9PfWdpGUKwVYJ/mTL8S6A7q4xP5cfkZq47sQQKBgQDP8xUp0ujjcBAYykTJcWoAaZ3M/Bno/HNAQohm2TXIsc7Z7FOjZgcO8eRRozfOPBUnPI3VAVdF3AcPV/Md0QByK3qMu9xsiztsuHyYwf0xqTN1li4OXpVqTPvvWvFfwAKDcCV/9masm8lIV920ZYhwE5mpNWoTs5kd+ssgwEYqUQKBgQCvk9iCN/Oimjcwu89UfVn7mabamydCxL5828CnsZo3koXTFb4QR70mkregESodFrk94aM1db1rOxVt+AE/aOzRd4ETC+zkUTojXl0/CD3ACBTtHmDiMXpSWpqhsUHhHwQwx7OWvt/lCyQc2meQ1mgAOAZaXx1GJMPk2rZYyZoeKwKBgG+RwjcQW7c7Nse2LjXOTcsu32VNlTE10TgW+kwEGiE1lP/DFhTC9uKD0bYjVhumjMriaE5yS7rFlFwdGelTa2PWBAhh+p+aDCYGzYLAAfKVD3D1RHydLpk7+KexKXPv9ulCUDxZnTja1KGl0uOo3g0T1wu8Bs/POkWlezp1LrAhAoGAahXcpaGE9eRUIH/538T1j43hQQiqCi+yIOLuXuRyl81SSp5Cfre51AqmJHL6jSTjvD4PTQfc8WN7qTxGFLgjDtU0CfwUoEdr517m3GTwWo0Hh9XpGAJpNEVGVRgzBACnmYmwOw8NXOkEYuKb3OmPYUJ4pDYADBe+vRJhRvhYxI0CgYEAnjRxJfPmQrSvKf5jghbii6rn9yhFxWjPH9iMJ7llF7OfYDv+Vq6LzL3vy9BvS9X8oxFZuPsl3uZ9U0ouNAX9qPZxnPF1lySort3LUoji+m3TeWVkjqMUTmFdA46GeMHSsIsOa5C026c+FkmA753395zTUxk6CCXyK5QL3Y6IB/s="; 
	//公司名称
    public static final String HUAWEI_USER_NAME =  "武汉多比特信息科技有限公司";
	
//---4:魅族支付
    public static final String MEIZU_ID = "3198096";			//AppID
    public static final String MEIZU_KEY = "75e4e4f3106a40aebf68ae430994e0e7";			//AppKey
    public static final String MEIZU_SECRET = "O0cglo6ccDpb4nKnUkMtHcNzqLYQCjsp";	//AppSecret
	
//---5:酷派支付
	/**需要copy下面两行(蓝色)到AndroidManifest.xml当中
	<!-- 酷派支付AppID -->
	<meta-data android:name="DC_APPID" android:value="5000009679" />
	**/
    public static final String CoolPad_APP_ID="5000009679"; 		//APP ID
    public static final String CoolPad_App_Key="b462d91ae1634bb0b0786f59926d2f8a"; 		//appkey(帐号)
    public static final String CoolPad_Pay_Key="NkQ3RTYwRDYxQ0UzN0UwMjRFRDdGRjUyRkNCRjIyRDhEQzc0MkRFM01UWTVOalV3T0RRNU5qUTVOVFkyT0RrNU5ERXJNalF4TlRFd05ETXdNemN3TWpjNU16QTRPVGMxTURnek5ETTRNamd5T0RjNE1ESTVOREF4";		//payKey(支付秘钥)
	
//---6:小米支付
    public static final String XIAOMI_APPID = "2882303761517739300";	//AppID
    public static final String XIAOMI_APPKEY = "5301773977300";	//AppKEY
    public static final String XIAOMI_SECRET = "ocOSM8gBt2DC2ZM9LhW30w==";	//AppSecret
	
//---7:金立支付
    public static final String JINLI_API_KEY = "25556EFE6B1D4BFCB3A1F091F6E70D1E";	//API_KEY
    public static final String JINLI_PRIVATE_KEY = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAI6yhxu/GsVfyH7OgN3OHBqQEgHjTxQUc1s5UlpF1qRuzVshe6KrnbEWKOsUN785j0reG7IeHO8z8fe/zmdMZakxSBbCClElG6MKS+Lw1C9dyklcMT/b/5MXvrnAuY0xbd2eaKl8AJMq8HMcOItyRSXFT1X+kpDoh31e7dthKFItAgMBAAECgYBW8IyCqi9sW4wzPiujd+UFhguCnPRcHo2d4dG78Fry+hMh18eU94xvDTpG552DY1VbXvFu1wv37SUaNgFSTGiO+wovIZIFdvfGEViGCkdkR/o1gFs4CkaYKwFjIHedvzzesRvLT2vj0eIFlZre8GHhZ23OitGT8BwEJ8CsOflbPQJBAO9M8xvfnxmhtvDmhztYu2o3FMiumqFm8OI+TMfdClvVFR9tXqL8Vb3XRqCa/BlLFIpcezsDE1o7OonFBgmFSAMCQQCYp8inaNwpR/8iQlLwfWRZp2RJPH1T5Dbsa9M3H7oC+qCxvmnj5tyITYauOVcWCWk+Y+TOeM79IbhIwf61+F4PAkAp38E6w2rHxXCJvw0y6VgCQhk09LjCPY0xSc2Nu7QwVZ0Ynr7MrnMigSUuvXAXzPePLpexv9wHEg4wRXXE/LmZAkBy9nCTOtIKuLC4UTB68kO/jONmkApmQkjmlXFUYy1Hjw2zrg261yKf95qE3KPr8ZxzovEuWSaw1VMeYBJ9YhUbAkEAjiYdOY1urD3HuAIJsxXDpugUWcgSaZ255DGPvDQI4LnkDpz4WfJzFhvPxNdG+sUQD2pRmaXETPSLpMJWu6wf4g==";	//PRIVATE_KEY
    /*
     * 安智支付
     * 
     */
    public static final String AnZhi_Key ="";
    public static final String AnZhi_SECRET ="";
    /*
     * 阿里支付
     * 
     */
	public static final String Ali_ApiKey ="";
	public static final String Ali_GameID ="";
	//-------10搜狗游戏sdk
	/**
	 * <meta-data android:name="sg_gamesdk_single_gid" android:value="XXX" />
	 */
	public static final String SOGO_KEY_ID="XXXXXX";
	//4399支付ID
	public static final String GameKey ="4399GameKey";
	public static final String PAY_SECRET = "4399PaySecret";
	//Dobest支付ID
	public static final String PINGPP_APP_ID = "app_DCSqbHf5y9S0nD4W";
	
	//Constant PayID
	
	/******商品列表******/
	//商品id
	public static final String[] PayItemIds = 
	{
		"com.cyxx.removeAds",//0 去广告
		"com.cyxx.1yuan", //1 对应1元
		"com.cyxx.6yuan", //6 对应6元
		"com.cyxx.12yuan", //12 对应12元
		"com.cyxx.18yuan", //18 对应18元
		"com.cyxx.6meanings", //6 解锁成语详解
	};
	//名称
	public static final String[] PayItemTitles =
	{
		"去广告",
		"8800金币",
		"1200金币",
		"2600金币",
		"4000金币",
		"成语详解",
		"去广告",
	};
	//描述
	public static final String[] PayItemDescs =
	{
		"购买后，去除游戏内所有广告",
		"1元购买8800金币",
		"6元购买3000金币",
		"12元购买6800金币",
		"18元购买12800金币",
		"购买后，解锁游戏内所有成语详解",
	};
	//价格单位元，精确两位小数
	public static final String[] PayItemPrices =
	{
		"18.00",
		"1.00",
		"6.00",
		"12.00",
		"18.00",
		"6.00"
	};
}

