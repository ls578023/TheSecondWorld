
using UnityEditor;

namespace GameFramework
{

    [InitializeOnLoad]
    public class GlobalConfig
    {
        static GlobalConfig()
        {
            PlayerSettings.Android.keystorePass = "123123";
            PlayerSettings.Android.keyaliasName = "dbttest";
            PlayerSettings.Android.keyaliasPass = "123123";
        }
    }
}