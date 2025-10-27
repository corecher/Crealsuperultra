using UnityEngine;

public class SynergyManager
{
    private string[] Engines = { "Turbo Engine", "Rocket Engine", "Jet Engine" };
    private string[] Cannons = { "Ruster Cannon", "Triple Cannon" };

    private PlayerStats playerStats;
    private BulletStats bulletStats;

    public SynergyManager(PlayerStats playerStats, BulletStats bulletStats)
    {
        this.playerStats = playerStats;
        this.bulletStats = bulletStats;
    }

    public void CheckSynergy()
    {
        if (EngineSynergy())
        {
            playerStats.maxHealth++;
            bulletStats.bulletDamage++;
            bulletStats.luck++;
            playerStats.tear++;
            Debug.Log("엔진 시너지 발동");
        }

        if (CannonSynergy())
        {
            Debug.Log("캐논 시너지 발동");
        }
    }

    public bool EngineSynergy()
    {
        foreach (string e in Engines)
        {
            if (!playerStats.items.ContainsKey(e)||playerStats.items[e] <= 0) return false;
        }
        return true;
    }
    
    public bool CannonSynergy()
    {
        foreach (string e in Cannons)
        {
            if (!playerStats.items.ContainsKey(e)||playerStats.items[e] <= 0) return false;
        }
        return true;
    }
}