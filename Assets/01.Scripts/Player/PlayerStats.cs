using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    
    [SerializeField] PlayerData playerData;

    public float rangedDamage;
    public float meleeDamage;
    
    public float maxHP;
    public float nowHP;
    
    public int maxLife;
    public int nowLife;

    public float speed;
    
    public float dashPower;
    public float dashCoolTime;
    public bool isUnlockDash;

    public float jumpPower;
    public int jumpCount;
    public int jumpCountMax;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void LoadData()
    {
        rangedDamage = playerData.rangedDamage;
        meleeDamage = playerData.meleeDamage;

        maxHP = playerData.maxHP;
        nowHP = playerData.nowHP;

        maxLife = playerData.maxLife;
        nowLife = playerData.nowLife;
        
        speed = playerData.speed;

        dashPower = playerData.dashPower;
        dashCoolTime = playerData.dashCoolTime;
        isUnlockDash = playerData.isUnlockDash;

        jumpPower = playerData.jumpPower;
        jumpCount = playerData.jumpCount;
        jumpCountMax = playerData.jumpCountMax;
    }

    public void SetDashPower()
    {
        dashPower += 5;
    }

    public void SetDashCoolTime()
    {
        dashCoolTime -= 0.5f;
    }

    public void AddAttackRangedDamage(float value)
    {
        rangedDamage += value;
    }

    public void AddAttackMeleeDamage(float value)
    {
        meleeDamage += value;
    }

    public float SetRangedDamage()
    {
        return rangedDamage;
    }

    public float SetMeleeDamage()
    {
        return meleeDamage;
    }

    public void AddNowHP()
    {
        if (nowHP == maxHP)
        {
            nowLife++;
        }
        nowHP = maxHP;
        UIManager.instance.RefreshHPUI();
    }

    public void DecreaseNowHP(float value)
    {
        nowHP = Mathf.Clamp(nowHP - value, 0, maxHP);
    }

    public void UnlockDash()
    {
        isUnlockDash = true;
        Debug.Log("Unlock : " + isUnlockDash);
    }

    public void SaveData()
    {
        playerData.rangedDamage = rangedDamage;
        playerData.meleeDamage = meleeDamage;

        playerData.maxHP = maxHP;
        playerData.nowHP = nowHP;

        playerData.maxLife = maxLife;
        playerData.nowLife = nowLife;

        playerData.speed = speed;

        playerData.dashPower = dashPower;
        playerData.dashCoolTime = dashCoolTime;
        playerData.isUnlockDash = isUnlockDash;

        playerData.jumpPower = jumpPower;
        playerData.jumpCountMax = jumpCountMax;

        Debug.Log("Save : " + playerData.isUnlockDash);
    }
}
