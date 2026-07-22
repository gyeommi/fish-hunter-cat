using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Attack")]
    public float rangedDamage = 2f;
    public float meleeDamage = 4f;

    [Header("HP")]
    public float maxHP = 20f;
    public float nowHP = 20f;

    [Header("Life")]
    public int maxLife = 3;
    public int nowLife = 3;

    [Header("Move")]
    public float speed = 3f;
    
    [Header("Dash")]
    public float dashPower = 11f;
    public float dashCoolTime = 1.4f;
    public bool isUnlockDash;

    [Header("Jump")]
    public float jumpPower = 8.5f;
    public int jumpCount = 0;
    public int jumpCountMax = 2;
}
