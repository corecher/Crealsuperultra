using UnityEngine;

[CreateAssetMenu(fileName = "BulletStat", menuName = "GameData/BulletStat")] 
public class BulletStats : ScriptableObject
{
    public float bulletDamage;
    public bool isPoison;
    public bool isSuper;
    public bool isSharp;

    public int luck = 5;
}
