using GoveKits.Runtime.Core;
using GoveKits.Runtime.Storage;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip mainBGMClip;
    public void PlayMainBGM()
        => AudioCore.PlayBGM(mainBGMClip);


    [SerializeField] private AudioClip battleBGMClip;
    public void PlayBattleBGM()
        => AudioCore.PlayBGM(battleBGMClip);


    // ============================================================


    [SerializeField] public AudioClip uiClickClip;
    public void PlayUIClick()
        => AudioCore.PlayDynamic(AudioChannel.UI, uiClickClip);


    [SerializeField] private AudioClip uiUpgradeClip;
    public void PlayUIUpgrade()
        => AudioCore.PlayDynamic(AudioChannel.UI, uiUpgradeClip);


    [SerializeField] private AudioClip levelUpClip;
    public void PlayLevelUp()
        => AudioCore.PlayDynamic(AudioChannel.SFX, levelUpClip);


    [SerializeField] private AudioClip expballClip;
    private float lastExpBallTime = 0f;

    public void PlayExpBall()
    {
        if (Time.unscaledTime - lastExpBallTime > 0.05f)
        {
            AudioCore.PlayDynamic(AudioChannel.SFX, expballClip);
            lastExpBallTime = Time.unscaledTime;
        }
    }
    

    [SerializeField] private AudioClip selectClip;
    public void PlaySelect()
        => AudioCore.PlayDynamic(AudioChannel.SFX, selectClip);


    [SerializeField] private AudioClip fireClip;
    public void PlayFire()
        => AudioCore.PlayDynamic(AudioChannel.SFX, fireClip);

    [SerializeField] private AudioClip hitClip;
    private float lastHitTime = 0f;

    public void PlayHit()
    {
        if (Time.unscaledTime - lastHitTime > 0.05f)
        {
            AudioCore.PlayDynamic(AudioChannel.SFX, hitClip);
            lastHitTime = Time.unscaledTime;
        }
    }

}
