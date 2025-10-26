using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "GameData/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Base Stats")]
    public int maxHealth = 6;
    public float tear = 3;
    public int luck = 5;
    public float moveSpeed = 5f;
    public float damage;

    public float delay { get { return (16 - tear * 1.3f); }}

    public int maxLevel = 20;

    public const float maxExp = 10000f;

    [Header("Growth")]
    public AnimationCurve expCurve; // 레벨업 성장률 곡선



     public List<Item> items = new List<Item>();

    private SynergyManager synergyManager;

    void Start()
    {
        synergyManager = new SynergyManager(this);
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        ApplyItemEffect(newItem);
        synergyManager.CheckSynergies();
    }

    void ApplyItemEffect(Item item)
    {
        switch (item.name)
        {
            case "터보 엔진":
                damage += 0.5f;
                break;
            case "로켓 엔진":
                tear *= 1.3f;
                break;
            case "제트 엔진":
                luck += 1;
                break;
            case "포이즌 샷":
                // 탄환에 독속성 부여 (확률 계산은 Fire 스크립트에서)
                break;
            case "러스터 캐논":
                damage += 7;
                tear *= 0.5f;
                break;
            case "일렉 캐논":
                // 스플래시 플래그
                break;
            case "트리플 캐논":
                // 3방향 발사 플래그
                break;
            case "레이저":
                // 탄환 레이저화
                break;
            case "샤프샷":
                // 관통 플래그
                break;
            case "미니건":
                damage *= 0.3f;
                tear += 10f;
                break;
            case "슈퍼 샷":
                // 10번째 탄환 강화
                break;
        }
    }

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
