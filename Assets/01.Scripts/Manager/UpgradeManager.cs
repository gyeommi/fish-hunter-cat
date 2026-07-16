using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Dash,
    MeleeAttack,
    RangedAttack,
    HP
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public Dictionary<UpgradeType, int> upgradeLevel = new Dictionary<UpgradeType, int>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (UpgradeType type in System.Enum.GetValues(typeof(UpgradeType)))
        {
            upgradeLevel[type] = 0;
        }
    }

    public void SetUpgradeStat(UpgradeType type)
    {
        int level = upgradeLevel[type];

        switch (type)
        {
            case UpgradeType.MeleeAttack:
                switch (level)
                {
                    case 0:
                        PlayerStats.instance.AddAttackMeleeDamage(1);
                        break;
                    case 1:
                        PlayerStats.instance.AddAttackMeleeDamage(3);
                        break;
                    case 2:
                        PlayerStats.instance.AddAttackMeleeDamage(5);
                        break;
                    default:
                        return;
                }
                upgradeLevel[type]++;
                break;

            case UpgradeType.RangedAttack:
                switch (level)
                {
                    case 0:
                        PlayerStats.instance.AddAttackRangedDamage(1);
                        break;
                    case 1:
                        PlayerStats.instance.AddAttackRangedDamage(3);
                        break;
                    case 2:
                        PlayerStats.instance.AddAttackRangedDamage(5);
                        break;
                    default:
                        return;
                }
                upgradeLevel[type]++;
                break;

            case UpgradeType.Dash:
                switch (level)
                {
                    case 0:
                        PlayerStats.instance.UnlockDash();
                        break;
                    case 1:
                        PlayerStats.instance.SetDashPower();
                        break;
                    case 2:
                        PlayerStats.instance.SetDashCoolTime();
                        break;
                    default:
                        return;
                }
                upgradeLevel[type]++;

                break;

            case UpgradeType.HP:
                switch (level)
                {
                    case 0:
                        PlayerStats.instance.AddNowHP();
                        break;
                    case 1:
                        PlayerStats.instance.AddNowHP();
                        break;
                    case 2:
                        PlayerStats.instance.AddNowHP();
                        break;
                    default:
                        return;
                }
                upgradeLevel[type]++;
                break;
        }
    }

    public string GetUpgradeText(UpgradeType type)
    {
        int level = upgradeLevel[type];

        switch (type)
        {
            case UpgradeType.MeleeAttack:
                switch (level)
                {
                    case 0: return "통조림 공격력 +1";
                    case 1: return "통조림 공격력 +3";
                    case 2: return "통조림 공격력 +5";
                    default: return "MAX";
                }

            case UpgradeType.RangedAttack:
                switch (level)
                {
                    case 0: return "발톱 공격력 +1";
                    case 1: return "발톱 공격력 +3";
                    case 2: return "발톱 공격력 +5";
                    default: return "MAX";
                }

            case UpgradeType.Dash:
                switch (level)
                {
                    case 0: return "대시 해금";
                    case 1: return "대시 거리 +5";
                    case 2: return "대시 쿨타임 -0.5초";
                    default: return "MAX";
                }

            case UpgradeType.HP:
                switch (level)
                {
                    case 0: return "HP 회복";
                    case 1: return "HP 회복";
                    case 2: return "HP 회복";
                    default: return "HP 회복";
                }
        }
        return "";
    }
}
