using System.Collections;
using UnityEngine;

public class First_Enemy : Enemy
{
    Coroutine attackCoroutine;

    private void Start()
    {
        hp = (hp + wave - 1) * quality;
        attackCoroutine = StartCoroutine(Attack());
    }

    private void Update()
    {
        if (hp <= 0) Dead();
    }

    public override IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            interval = Random.Range(minInterval, maxInterval);
            Debug.Log("АјАн!");
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    public override void Dead()
    {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        Destroy(gameObject);
    }
}
