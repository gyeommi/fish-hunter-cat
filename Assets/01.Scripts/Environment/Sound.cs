using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] Slider bgmVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    private void Start()
    {
        bgmVolumeSlider.value = SoundManager.instance.bgmAudioSource.volume;
        sfxVolumeSlider.value = SoundManager.instance.sfxAudioSource.volume;
    }

    public void BGMVolumeChnaged()
    {
        SoundManager.instance.bgmAudioSource.volume = bgmVolumeSlider.value;
    }
    
    public void SFXVolumeChnaged()
    {
        SoundManager.instance.sfxAudioSource.volume = sfxVolumeSlider.value;
    }
}
