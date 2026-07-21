using UnityEngine;

public class BtnClick : MonoBehaviour
{
    public void StartBtn()
    {
        StageManager.instance.GameStart();
    }

    public void MainBtn()
    {
        StageManager.instance.GameMain();
    }

    public void RetryBtn()
    {
        StageManager.instance.RetryStage();
    }

    public void ExitBtn()
    {
        StageManager.instance.GameExit();
    }
}
