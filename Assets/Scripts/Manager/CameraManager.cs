using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    void LateUpdate()
    {
        Player player = VMContainer.Get<BattleViewModel>().playerCharacter;
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            targetPosition.z = -10f; // Set a fixed distance from the player
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
