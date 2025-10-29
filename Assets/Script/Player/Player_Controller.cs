using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] public PlayerStats stat;

    [SerializeField] BulletStats bulletStat;
    [SerializeField] GameObject bullet;

    [SerializeField]GameObject levelUp;
    [SerializeField]GameObject statSelect;

    public int hp;
    public int level;
    public int exp;

    private bool attackDown;
    private Coroutine fireCoroutine;

    float fireInterval;
    float attackCooltime;

    private bool superShot;
    
    

     
    //private SynergyManager synergyManager;

    void Start()
    {
        
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
        int bulletcnt = 0;
        while (attackDown)
        {
            if (superShot&&bulletcnt % 10 == 0) bulletStat.isSuper = true;//슈퍼샷
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<Player_bullet>().bulletstat = bulletStat;
            bulletcnt++;
            bulletStat.isSuper = false;
            yield return new WaitForSeconds(fireInterval);
            
        }

        fireCoroutine = null;
    }
    
    public void GetDamage(int damage)
    {
        hp -= damage;
    }

    public void ExpUp(int exp)
    {
        this.exp+=exp;
        
        LevelCheck(level);
    }
    void LevelCheck(int level)
    {
        if (stat.GetRequiredExpForNextLevel(level) <= exp && level < stat.maxLevel)
        {
            LevelUp();
            exp = 0;
        }
    }

    public void PowerUp(int index)
    {
        switch (index)
        {
            case 0:
                stat.maxHealth++;
                break;
            case 1:
                hp++;
                break;
            case 2:
                stat.tear += 0.1f;
                break;
        }
        levelUp.SetActive(false);

    }
    void LevelUp()
    {
        Debug.Log("와 레벨업!");
        
        level++;
        if (level % 5 == 0)
        {
            statSelect.SetActive(true);
        }
        else
        {
            levelUp.SetActive(true);
        }
    }


    public void AddItem(string itemName)
    {

        if (!stat.items.ContainsKey(itemName))
        {
            stat.items[itemName] = 0;
        }
        stat.items[itemName]++;
        ApplyItemEffect(itemName);
        statSelect.SetActive(false);
        
    }

    void ApplyItemEffect(string item)
    {
        switch (item)
        {
            case "Turbo Engine"://데미지 1 증가
                bulletStat.bulletDamage += 1f;
                Debug.Log("터보 엔진?!");
                break;
            case "Rocket Engine"://연사 30% 증가
                stat.tear *= 1.3f;
                break;
            case "Jet Engine"://행운 1 증가
                bulletStat.luck += 1;
                break;
            case "Poison Shot":// 탄환에 독속성 부여 (10% 확률 공격력 * 10/행운 만큼의 추가 피해, 다만 공격력의 100%까지만)
                bulletStat.isPoison = true;
                break;
            case "Ruster Cannon"://데미지 7 증가 연사 0.5로 떨어짐
                bulletStat.bulletDamage += 7;
                stat.tear = 0.5f;
                break;
            
            case "Triple Cannon":// 3방향 발사 
                
                break;
            case "Sharp Shot":
                bulletStat.isSharp = true;
                break;
            case "Mini Shot":
                bulletStat.bulletDamage*=0.3f;
                stat.tear += 10f;
                break;
            case "Super Shot":
                superShot = true;
                break;
        }
    }
    
    
    
}
