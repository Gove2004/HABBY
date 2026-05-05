using DG.Tweening;
using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.15f; // 阻尼时间，值越小相机跟随越快，值越大越平滑
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        Player player = VMContainer.Get<BattleViewModel>().playerCharacter;
        if (player != null && mainCamera != null)
        {
            Vector3 targetPosition = player.transform.position;
            targetPosition.z = -10f; // 保持Z轴距离

            // 使用 SmoothDamp 替代 Lerp，能解决帧率波动带来的卡顿感
            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position, 
                targetPosition, 
                ref velocity, 
                smoothTime
            );
        }
    }


    public void Reset()
    {
        velocity = Vector3.zero;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(0f, 0f, -10f);
            mainCamera.orthographicSize = 5f; // 重置为默认大小
        }
    }


    public void StartBattleCameraEffect()
    {
        // 0.9 -> 0.5 -> 5.0
        mainCamera.orthographicSize = 0.9f;
        DOTween.To(() => mainCamera.orthographicSize, x => mainCamera.orthographicSize = x, 0.5f, 1f)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                DOTween.To(() => mainCamera.orthographicSize, x => mainCamera.orthographicSize = x, 5f, 2f)
                    .SetEase(Ease.OutQuad)
                    .SetUpdate(true);
            });
    }



    public void HurtShake()
    {
        // DoShake
        mainCamera.transform.DOShakePosition(0.2f, 0.3f, 10, 90f).SetUpdate(true);
        // 同时固色变红
        mainCamera.DOColor(Color.red, 0.1f).SetUpdate(true).OnComplete(() =>
        {
            mainCamera.DOColor(Color.black, 0.1f).SetUpdate(true);
        });
    }


}
