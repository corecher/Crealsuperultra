using System.Collections.Generic;
using UnityEngine;

public class SynergyManager
{
    private PlayerStats player;

    public SynergyManager(PlayerStats player)
    {
        this.player = player;
    }

    public void CheckSynergies()
    {
        Dictionary<string, int> itemTypeCount = new Dictionary<string, int>();

        foreach (var item in player.items)
        {
            if (!itemTypeCount.ContainsKey(item.itemType))
                itemTypeCount[item.itemType] = 0;
            itemTypeCount[item.itemType]++;
        }

        // --- 엔진 시너지 ---
        if (HasAll(itemTypeCount, "엔진", 3))
        {
            player.damage += 1;
            player.tear += 1;
            player.luck += 1;
            player.maxHealth += 1;
            Debug.Log("🔥 엔진 시너지 발동!");
        }

        // --- 캐논 시너지 ---
        if (HasAll(itemTypeCount, "캐논", 2))
        {
            Debug.Log("💥 캐논 시너지 발동! 4방향 발사");
            // 탄환 시스템에서 방향 4개로 설정
        }
    }

    private bool HasAll(Dictionary<string, int> dict, string itemType, int required)
    {
        return dict.ContainsKey(itemType) && dict[itemType] >= required;
    }
}
