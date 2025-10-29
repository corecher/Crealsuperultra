
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    const float bulletSpeed = 0.5f;
    
    public BulletStats bulletstat;
    void Start()
    {
        Destroy(gameObject,3f);
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(bulletSpeed, 0, 0);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = bulletstat.bulletDamage+15;
        //포이즌 샷
        if (bulletstat.isPoison && Random.value <= 0.1f)
        {
            float extra = bulletstat.bulletDamage * (10f / Mathf.Max(bulletstat.luck, 1f));
            extra = Mathf.Min(extra, bulletstat.bulletDamage); // 100%까지만
            damage += extra;
        }


        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetDamage(damage);
            if (!bulletstat.isSharp) Destroy(gameObject);//샤프샷
        }
        
    }
}