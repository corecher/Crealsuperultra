using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameData/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Base Stats")]
    public int maxHealth = 6;
    public float tear = 3;
    
    public float moveSpeed = 5f;
    

    

    public int maxLevel = 20;

    public float maxExp = 1000f;

    [Header("Growth")]
    public AnimationCurve expCurve; // 레벨업 성장률 곡선



     public Dictionary<string,int> items = new Dictionary<string,int>();

    

    /// <summary>
    /// 누적 경험치 계산
    /// </summary>
    public float GetTotalExpAtLevel(int level)
    {
        if (level <= 1) return 0f;
        float t = Mathf.Clamp01(level / (float)maxLevel);
        return expCurve.Evaluate(t) * maxExp;
    }

    /// <summary>
    /// 다음 레벨까지 필요한 경험치 반환
    /// </summary>
    public float GetRequiredExpForNextLevel(int currentLevel)
    {
        if (currentLevel >= maxLevel)
            return 0f; // 이미 만렙

        float currentTotal = GetTotalExpAtLevel(currentLevel);
        float nextTotal = GetTotalExpAtLevel(currentLevel + 1);
        return nextTotal - currentTotal;
    }
}
