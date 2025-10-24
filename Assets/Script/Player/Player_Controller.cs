using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    Rigidbody2D rb;

    [SerializeField] PlayerStats stat;
    [SerializeField] GameObject bullet;

    private int hp;
    private int level;
    private int exp;
    private bool attackDown;
    private Coroutine fireCoroutine;

    float fireInterval;
    float attackCooltime;

     

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = stat.maxHealth;
        

    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z)&&attackCooltime<=0f)
        {   
            attackDown = true;
            if (fireCoroutine == null)
                fireCoroutine = StartCoroutine(Fire());
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            attackDown = false;
            
            if (fireCoroutine != null)
            {
                StopCoroutine(fireCoroutine);
                attackCooltime = fireInterval;
                fireCoroutine = null;
            }
        }

        if (attackCooltime >= 0) attackCooltime -= Time.deltaTime;
    }


    void Update()
    {
        Attack();
        fireInterval = 1f / stat.tear;
    }
    void FixedUpdate()
    {
        
        Move();
    }
    void Move()//프레임 상관없이 이동속도 고정
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(x, y).normalized * playerSpeed;

    }


    IEnumerator Fire()
    {
        while (attackDown)
        {
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(fireInterval);
        }

        fireCoroutine = null;
    }
    void UpgradeTear()
    {
        stat.tear++;
    }
    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    void LevelUp()
    {
        level++;
    }
    void LevelCheck(int level)
    {
        if (stat.GetRequiredExpForNextLevel(level) <= exp && level < stat.maxLevel)
        {
            LevelUp();
        }
    }
    
    
    
}
