using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] public int quality;
    [SerializeField] public float hp;

    [SerializeField] public float minInterval;
    [SerializeField] public float maxInterval;
    [SerializeField] public float interval;
    [SerializeField] public int wave; //병합 과정에서 GameManager.instance.wave로 대체

    public Bullet bulletPrefab;

    public abstract IEnumerator Attack();
    public abstract void Shoot();
    public abstract void GetDamage(int damage);
    public abstract void Dead();
    
}
