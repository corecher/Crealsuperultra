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
    private bool mouseDown;
    private Coroutine fireCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = stat.maxHealth;


    }
    void Attack()
{
    if (Input.GetKeyDown(KeyCode.Z))
    {
        mouseDown = true;
        if (fireCoroutine == null)
            fireCoroutine = StartCoroutine(Fire());
    }

    if (Input.GetKeyUp(KeyCode.Z))
    {
        mouseDown = false;
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }
}


    void Update()
    {
        Attack();
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
        while (mouseDown)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            float fireInterval = 1f / stat.tear;
            yield return new WaitForSeconds(fireInterval);
        }

        fireCoroutine = null;
    }
    void UpgradeTear()
    {
        stat.tear++;
    }


    void LevelUp()
    {
        
    }
    void LevelCheck(int level)
    {
        if (stat.GetRequiredExpForNextLevel(level) <= exp && level < stat.maxLevel)
        {
            LevelUp();
        }
    }
    
    
    
}
