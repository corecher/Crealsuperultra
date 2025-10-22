using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField] public int quality;
    [SerializeField] public float hp;

    [SerializeField] public float minInterval;
    [SerializeField] public float maxInterval;
    [SerializeField] public float interval;
    [SerializeField] public int wave; //웨이브는 나중에 구현

    public Bullet bulletPrefab;

    public abstract IEnumerator Attack();
    public abstract void Dead();
    
}
