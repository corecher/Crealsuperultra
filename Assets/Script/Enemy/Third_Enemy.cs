using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Third_Enemy : Enemy
{
    GameObject player;
    Coroutine attackCoroutine;
    Animator animator;
    private WaveManager waveManager;
    private bool dead=true;
    private void Start()
    {
        GameObject Waves = GameObject.Find("WaveManager");
        waveManager = Waves.GetComponent<WaveManager>();
        quality = (waveManager.waveCount > 10) ? Random.Range(1, 3) : Random.Range(1, 4);//���� �������� GameManager.instance.wave�� ��ü
        hp = (hp + waveManager.waveCount - 1) * quality; //���� �������� GameManager.instance.wave�� ��ü

        animator = GetComponent<Animator>();
        attackCoroutine = StartCoroutine(Attack());

        player = GameObject.Find("Player");
        playerController = player.GetComponent<Player_Controller>();
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
            playerController.ExpUp(3);
        }
    }
}
