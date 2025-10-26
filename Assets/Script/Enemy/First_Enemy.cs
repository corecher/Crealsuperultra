using System.Collections;
using UnityEngine;

public class First_Enemy : Enemy
{
    Coroutine attackCoroutine;
    Animator animator;

    private void Start()
    {
        quality = (wave > 10) ? Random.Range(1, 3) : Random.Range(1, 4);
        hp = (hp + wave - 1) * quality;

        animator = GetComponent<Animator>();
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
            animator.SetTrigger("attack");
        }
    }

    public override void Shoot()
    {
        Instantiate(bulletPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, transform.position.z), transform.rotation);
    }

    public override void Dead()
    {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        animator.SetTrigger("dead");
        Destroy(gameObject, 1f);
    }
}
