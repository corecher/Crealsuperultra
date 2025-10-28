using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Third_Enemy : Enemy
{
    GameObject player;
    Coroutine attackCoroutine;
    Animator animator;

    private void Start()
    {
        quality = (wave > 10) ? Random.Range(1, 3) : Random.Range(1, 4);//병합 과정에서 GameManager.instance.wave로 대체
        hp = (hp + wave - 1) * quality; //병합 과정에서 GameManager.instance.wave로 대체

        animator = GetComponent<Animator>();
        attackCoroutine = StartCoroutine(Attack());

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y + 0.2f, 0), 0.01f);
        if (hp <= 0) Dead();
    }

    public override IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            interval = Random.Range(minInterval, maxInterval);
            animator.SetBool("attack", true);
        }
    }

    public override void Shoot()
    {
        Instantiate(bulletPrefab, new Vector3(transform.position.x - 1.0f, transform.position.y - 0.2f, 0), transform.rotation);
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
