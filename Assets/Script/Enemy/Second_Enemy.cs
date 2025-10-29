using System.Collections;
using UnityEngine;

public class Second_Enemy : Enemy
{
    Coroutine attackCoroutine;
    Animator animator;
    private WaveManager waveManager;
    private bool dead=true;
    private void Start()
    {
        GameObject Waves = GameObject.Find("WaveManager");
        waveManager = Waves.GetComponent<WaveManager>();
        quality = (waveManager.waveCount > 10) ? Random.Range(1, 3) : Random.Range(1, 4); //���� �������� GameManager.instance.wave�� ��ü
        hp = (hp + waveManager.waveCount - 1) * quality; //���� �������� GameManager.instance.wave�� ��ü

        animator = GetComponent<Animator>();
        attackCoroutine = StartCoroutine(Attack());
        playerController = GameObject.Find("Player").GetComponent<Player_Controller>();
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

    public override void GetDamage(float damage)
    {
        hp -= damage;
    }

    public override void Dead()
    {
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        animator.SetTrigger("dead");
        Destroy(gameObject, 1f);
        if (dead)
        {
            waveManager.enemyCount--;
            dead = false;
            playerController.ExpUp(2);
        }
    }
}
