using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private int currentStage;

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
        currentStage = SceneManager.GetActiveScene().buildIndex;
    }

    public void GameStart()
    {
        currentStage++;
        SceneManager.LoadScene(currentStage);
    }

    public void NextStage()
    {
        currentStage++;
        SceneManager.LoadScene(currentStage);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void RetryStage()
    {
        SceneManager.LoadScene(currentStage);
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RemoveMonster(GameObject monster)
    {
        monster.SetActive(false);
    }
}
