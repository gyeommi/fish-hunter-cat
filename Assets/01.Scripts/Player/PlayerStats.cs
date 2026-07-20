using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public float rangedDamage = 2;
    public float meleeDamage = 4;
    public float maxHP = 20;
    public float nowHP = 20;
    public int maxLife = 3;
    public int nowLife = 3;

    public float dashPower = 11f;
    public float dashCoolTime = 1.4f;

    public bool isUnlockDash;

    [SerializeField] PlayerHealth health;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if (health == null)
            health = gameObject.GetComponent<PlayerHealth>();
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
            health.SetLife(nowLife);
        }
        nowHP = maxHP;
    }

    public void DecreaseNowHP(float value)
    {
        nowHP -= value;
    }

    public void UnlockDash()
    {
        isUnlockDash = true;
    }
}
