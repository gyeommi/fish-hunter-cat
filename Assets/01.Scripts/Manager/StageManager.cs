using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private int currentStage;

    List<EnemyController> enemies = new();
    Dictionary<GameObject, Vector3> allEnemies;
    Dictionary<GameObject, Vector3> deadEnemies;

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
        allEnemies = new Dictionary<GameObject, Vector3>();
        deadEnemies = new Dictionary<GameObject, Vector3>();
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

    public void RegisterEnemy(EnemyController enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void ResetEnemy()
    {
        foreach (EnemyController enemy in enemies)
        {
            enemy.ResetEnemy();
        }
        deadEnemies.Clear();
    }

    public void RemoveEnemy(GameObject enemy, Vector3 pos)
    {
        deadEnemies.Add(enemy, pos);
        enemy.SetActive(false);
    }

    public void ActiveEnemy()
    {
        foreach (var enemy in deadEnemies)
        {
            enemy.Key.transform.position = enemy.Value;
            enemy.Key.SetActive(true);
        }
        deadEnemies.Clear();
    }
}
