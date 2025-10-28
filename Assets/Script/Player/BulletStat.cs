using UnityEngine;

[CreateAssetMenu(fileName = "BulletStat", menuName = "GameData/BulletStat")] 
public class BulletStats : ScriptableObject
{
    public float bulletDamage;
    public bool isPoison;
    
    public bool isSharp;
    public bool isSuper;
    public int luck = 5;
}
