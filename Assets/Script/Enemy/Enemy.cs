using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] public int quality;
    [SerializeField] public float hp;

    [SerializeField] public float minInterval;
    [SerializeField] public float maxInterval;
    [SerializeField] public float interval;
    [SerializeField] public int wave; //���� �������� GameManager.instance.wave�� ��ü

    public Bullet bulletPrefab;

    public abstract IEnumerator Attack();
    public abstract void Shoot();
    public abstract void GetDamage(int damage);
    public abstract void Dead();
    
}
