ProjectSetting.json	//��Ŀ����

[
	{
		"Id":1,					//��ĿID
		"Name":"��Ŀ��",				//��Ŀ��
		"ProjectDir":"D:/CJHFW",			//��ĿĿ¼
		"ClientDir":"$ProjectDir$/Client/CSFGameAd",	//�ͻ���Ŀ¼
		"ServerDir":"$ProjectDir$/Server/CSFServer",	//�����Ŀ¼
		"ConfigDir":"$ProjectDir$/ProtoConfig/Config",	//�����ļ�Ŀ¼
		"ProtoDir":"$ProjectDir$/ProtoConfig/Proto",	//ProtobufĿ¼
		"CodeOutId":1				//������������
	},
	{
		"Id": 2,					//��Ŀ2����
		"Name": "��Ŀ��-Ad��֧2",
		"ProjectDir": "D:/CJHFW2",
		"ClientDir": "$ProjectDir$/Client/CSFGameAd",
		"ServerDir": "$ProjectDir$/Server/CSFServer",
		"ConfigDir": "$ProjectDir$/ProtoConfig/Config",
		"ProtoDir": "$ProjectDir$/ProtoConfig/Proto",
		"CodeOutId": 1
	}
]


CodeOutSetting.json		//������������
[
	{
		"Id":1,							//����ID
		"Name":"������C#,�ͻ���Lua����",				//������
		"ServerConfig":{
			"CodeType":"C#",					//����������	�������� C# AS3	Lua   C++ Java (��ʱֻ֧��C#)
			"ConfigOutDir":"$ServerDir$/GameServer/bin/x64/Debug/DataConfig",	//�����ļ����Ŀ¼
			"ConfigMgrDir":"$ServerDir$/GameServer/XGame/ConfigMgr",		//���ù�������Ŀ¼
			"ConfigClassDir":"$ConfirMgrDir$/Config",				//�����ļ�Ŀ¼
			"ConfigInitFile":"$ConfirMgrDir$/ConfigInit.cs",				//��ʼ���ļ�
			"ConfigMgrFile":"$ConfirMgrDir$/ConfigMgr.cs",				//�������ļ�	
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