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

    [SerializeField] PlayerData originPlayerData;
    [SerializeField] PlayerData playerData;

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

    public void ResetPlayerData()
    {
        playerData.rangedDamage = originPlayerData.rangedDamage;
        playerData.meleeDamage = originPlayerData.meleeDamage;

        playerData.maxHP = originPlayerData.maxHP;
        playerData.nowHP = originPlayerData.nowHP;

        playerData.maxLife = originPlayerData.maxLife;
        playerData.nowLife = originPlayerData.nowLife;

        playerData.speed = originPlayerData.speed;

        playerData.dashPower = originPlayerData.dashPower;
        playerData.dashCoolTime = originPlayerData.dashCoolTime;
        playerData.isUnlockDash = originPlayerData.isUnlockDash;

        playerData.jumpPower = originPlayerData.jumpPower;
        playerData.jumpCount = originPlayerData.jumpCount;
        playerData.jumpCountMax = originPlayerData.jumpCountMax;
    }

    public void GameStart()
    {
        ResetPlayerData();

        currentStage++;
        SceneManager.LoadScene(currentStage);

        SoundManager.instance.PlayBGM((BGMType)currentStage);
    }

    public void NextStage()
    {
        enemies.Clear();
        deadEnemies.Clear();

        currentStage++;
        SceneManager.LoadScene(currentStage);

        SoundManager.instance.PlayBGM((BGMType)currentStage);
    }

    public void GameMain()
    {
        SceneManager.LoadScene("00.Start");
        SoundManager.instance.PlayBGM(BGMType.Start);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("05.GameOver");
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("06.End");
        SoundManager.instance.PlayBGM(BGMType.Start);
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
        BossController boss = FindFirstObjectByType<BossController>();

        if (boss != null && boss.gameObject.activeInHierarchy)
        {
            boss.ResetBoss();
        }
        else
        {
            foreach (EnemyController enemy in enemies)
            {
                enemy.ResetEnemy();
            }
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

    public void DestroyEnemy()
    {
        foreach (EnemyController enemy in enemies)
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }
        enemies.Clear();
        deadEnemies.Clear();
    }
}
