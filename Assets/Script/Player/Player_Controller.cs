using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] PlayerStats stat;
    [SerializeField] GameObject bullet;

    public int hp;
    private int level;
    private int exp;

    private bool attackDown;
    private Coroutine fireCoroutine;

    float fireInterval;
    float attackCooltime;

    

     
    private SynergyManager synergyManager;

    void Start()
    {
        synergyManager = new SynergyManager(this.stat);
    }
    
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

        rb.linearVelocity = new Vector2(x, y).normalized * stat.moveSpeed;

    }


    IEnumerator Fire()
    {
        while (attackDown)
        {
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<Player_bullet>().bullet_damage = stat.damage;
            yield return new WaitForSeconds(fireInterval);
        }

        fireCoroutine = null;
    }
    
    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    void ExpUp()
    {
        exp++;
        LevelCheck(level);
    }
    void LevelCheck(int level)
    {
        if (stat.GetRequiredExpForNextLevel(level) <= exp && level < stat.maxLevel)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        level++;
    }
    

    public void AddItem(string combined )
    {

        string[] parts = combined.Split(' ');
        Item newItem = new Item(parts[0], parts[1]);
        stat.items.Add(newItem);
        ApplyItemEffect(newItem);
        synergyManager.CheckSynergies();
    }

    void ApplyItemEffect(Item item)
    {
        switch (item.name)
        {
            case "Turbo"://데미지 0.5 증가
                stat.damage += 0.5f;
                break;
            case "Rocket"://연사 30% 증가
                stat.tear *= 1.3f;
                break;
            case "Jet"://행운 1 증가
                stat.luck += 1;
                break;
            case "Poison":// 탄환에 독속성 부여 (10% 확률 공격력 * 10/행운 만큼의 추가 피해, 다만 공격력의 100%까지만)
                
                break;
            case "Ruster"://데미지 7 증가 연사 0.5로 떨어짐
                stat.damage += 7;
                stat.tear = 0.5f;
                break;
            case "Electric"://탄환이 적에게 맞으면 주변 3명에게 현재 데미지의 30% 해당하는  피해를 줌
                // 스플래시 플래그
                break;
            case "Tripple":// 3방향 발사 
                
                break;
            case "Sharp":
                // 관통 플래그
                break;
            case "Mini":
                stat.damage *= 0.3f;
                stat.tear += 10f;
                break;
            case "Super":
                // 10번째 탄환 강화
                break;
        }
    }
    
    
    
}
