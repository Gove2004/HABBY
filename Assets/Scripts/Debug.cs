using GoveKits.Runtime.Core;
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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LogCore.Log("Debug: 获得 100000000 金币");
                GameViewModel gameViewModel = VMContainer.Get<GameViewModel>();
                gameViewModel.UpdateMaxScore(100000000);
            }
        }
    }
}
