using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private int currentStage;

    public List<GameObject> enemyList;

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
        enemyList = new List<GameObject>();
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

    public void RemoveEnemy(GameObject monster)
    {
        enemyList.Add(monster);
        monster.SetActive(false);
    }

    public void ActiveEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetActive(true);
        }
        enemyList.Clear();
    }
}
