ProjectSetting.json	//项目设置

[
	{
		"Id":1,					//项目ID
		"Name":"项目名",				//项目名
		"ProjectDir":"D:/CJHFW",			//项目目录
		"ClientDir":"$ProjectDir$/Client/CSFGameAd",	//客户端目录
		"ServerDir":"$ProjectDir$/Server/CSFServer",	//服务端目录
		"ConfigDir":"$ProjectDir$/ProtoConfig/Config",	//配置文件目录
		"ProtoDir":"$ProjectDir$/ProtoConfig/Proto",	//Protobuf目录
		"CodeOutId":1				//导出语言配置
	},
	{
		"Id": 2,					//项目2配置
		"Name": "项目名-Ad分支2",
		"ProjectDir": "D:/CJHFW2",
		"ClientDir": "$ProjectDir$/Client/CSFGameAd",
		"ServerDir": "$ProjectDir$/Server/CSFServer",
		"ConfigDir": "$ProjectDir$/ProtoConfig/Config",
		"ProtoDir": "$ProjectDir$/ProtoConfig/Proto",
		"CodeOutId": 1
	}
]


CodeOutSetting.json		//导出语言设置
[
	{
		"Id":1,							//配置ID
		"Name":"服务器C#,客户端Lua导出",				//配置名
		"ServerConfig":{
			"CodeType":"C#",					//服务器语言	语言类型 C# AS3	Lua   C++ Java (暂时只支持C#)
			"ConfigOutDir":"$ServerDir$/GameServer/bin/x64/Debug/DataConfig",	//配置文件输出目录
			"ConfigMgrDir":"$ServerDir$/GameServer/XGame/ConfigMgr",		//配置管理器根目录
			"ConfigClassDir":"$ConfirMgrDir$/Config",				//配置文件目录
			"ConfigInitFile":"$ConfirMgrDir$/ConfigInit.cs",				//初始化文件
			"ConfigMgrFile":"$ConfirMgrDir$/ConfigMgr.cs",				//管理器文件	
		},
		"ClientConfig":{
			"CodeType":"AS3",
			"ConfigOutDir":"$ClientDir$/bin/h5/res/config",
			"ConfigMgrDir":"$ClientDir$/src/ConfigData",
			"ConfigClassDir":"$ConfigMgrDir$/Config",
			"ConfigInitFile":"$ConfirMgrDir$/ConfigInit.as",
			"ConfigMgrFile":"$ConfirMgrDir$/ConfigMgr.as",
		},
	},
	
]