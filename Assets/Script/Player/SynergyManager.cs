using System.Collections.Generic;
using UnityEngine;

public class SynergyManager
{
    private List<string> vistied = new List<string>();

    private PlayerStats player;

    public SynergyManager(PlayerStats player)
    {
        this.player = player;
    }

    ///<summary>
    ///시너지 아이템이 다 모였는지 체크
    ///</summary>
    public void CheckSynergies()
    {
        Dictionary<string, int> itemTypeCount = new Dictionary<string, int>();

        foreach (var item in player.items)
        {
            if (!vistied.Contains(item.name))//이러면 중복 방지가 되겠죠잉?
            {
                if (!itemTypeCount.ContainsKey(item.itemType))
                {
                    itemTypeCount[item.itemType] = 0;
                }
                itemTypeCount[item.itemType]++;
                vistied.Add(item.name);
            }

        }

        // --- 엔진 시너지 ---
        if (HasAll(itemTypeCount, "엔진", 3))
        {
            player.damage += 1;
            player.tear += 1;
            player.luck += 1;
            player.maxHealth += 1;
            Debug.Log("엔진 시너지 발동!");
        }

        // --- 캐논 시너지 ---
        if (HasAll(itemTypeCount, "캐논", 2))
        {
            Debug.Log("캐논 시너지 발동! 4방향 발사");
            // 탄환 시스템에서 방향 4개로 설정
        }
    }

    private bool HasAll(Dictionary<string, int> dict, string itemType, int required)
    {
        return dict.ContainsKey(itemType) && dict[itemType] >= required;
    }
}
