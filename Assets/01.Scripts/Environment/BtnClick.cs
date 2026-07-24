using UnityEngine;

public class BtnClick : MonoBehaviour
{
    public void StartBtn()
    {
        SoundManager.instance.PlaySFX(SFXType.UIClick);
        StageManager.instance.GameStart();
    }

    public void MainBtn()
    {
        SoundManager.instance.PlaySFX(SFXType.UIClick);
        StageManager.instance.GameMain();
    }

    public void RetryBtn()
    {
        SoundManager.instance.PlaySFX(SFXType.UIClick);
        StageManager.instance.RetryStage();
    }

    public void ExitBtn()
    {
        SoundManager.instance.PlaySFX(SFXType.UIClick);
        StageManager.instance.GameExit();
    }

    public void BtnSound()
    {
        SoundManager.instance.PlaySFX(SFXType.UIClick);
    }

    public void TimeStop()
    {
        Time.timeScale = 0f;
    }

    public void TimeStart()
    {
        Time.timeScale = 1f;
    }
}
