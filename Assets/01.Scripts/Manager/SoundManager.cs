using System.Collections;
using UnityEngine;

public enum SFXType
{
    Claw,
    CannedTuna,
    Gun,
    Jump,
    SmallFish,
    GoldFish,
    UIClick
}

public enum BGMType
{
    Start,
    Stage1,
    Stage2,
    Stage3,
    BossStage
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip[] bgmClip;
    public AudioClip[] sfxClip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBGM(BGMType.Start);
    }

    public void PlaySFX(SFXType type)
    {
        if ((int)type > sfxClip.Length)
            return;

        sfxAudioSource.PlayOneShot(sfxClip[(int)type]);
    }

    public void PlayBGM(BGMType type)
    {
        if ((int)type > bgmClip.Length)
            return;

        bgmAudioSource.clip = bgmClip[(int)type];
        bgmAudioSource.Play();
    }

    public void BGMVolume(float vol)
    {
        bgmAudioSource.volume = vol;
    }

    public void SFXVolume(float vol)
    {
        sfxAudioSource.volume = vol;
    }
}
