using System.Collections;
using UnityEngine;

public class Second_Enemy : Enemy
{
    Coroutine attackCoroutine;
    Animator animator;

    private void Start()
    {
        quality = (wave > 10) ? Random.Range(1, 3) : Random.Range(1, 4); //병합 과정에서 GameManager.instance.wave로 대체
        hp = (hp + wave - 1) * quality; //병합 과정에서 GameManager.instance.wave로 대체

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
        Instantiate(bulletPrefab, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation);
    }

    public override void EndAttack()
    {
        animator.SetBool("attack", false);
    }

    public override void GetDamage(int damage)
    {
        hp -= damage;
    }

    public override void Dead()
    {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        animator.SetTrigger("dead");
        Destroy(gameObject, 1f);
    }
}
