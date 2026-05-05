using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using GoveKits.Runtime.UI;
using UnityEngine;

public class Debug : MonoSingleton<Debug>
{
    private bool isDebugMode = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isDebugMode = !isDebugMode;
            LogCore.Log($"调试模式: {(isDebugMode ? "开启" : "关闭")}");
        }

        if (isDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                LogCore.Log("Debug: 清空存档数据");
                PrefsCore.DeleteAll();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LogCore.Log("Debug: 获得 100000000 金币");
                GameViewModel gameViewModel = VMContainer.Get<GameViewModel>();
                gameViewModel.UpdateMaxScore(100000000);
            }
            
        }
    }


    private void OnGUI()
    {
        if (isDebugMode)
        {
            GUI.Label(new Rect(10, 10, 300, 25), "调试模式已开启");
            GUI.Label(new Rect(10, 30, 300, 25), "按 F1 切换调试模式");
            GUI.Label(new Rect(10, 50, 300, 25), "按 0 清空存档数据");
            GUI.Label(new Rect(10, 70, 300, 25), "按 1 获得 100000000 金币");
        }
    }
}
