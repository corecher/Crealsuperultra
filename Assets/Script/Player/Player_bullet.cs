using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    const float bulletSpeed = 0.5f;
    public int bullet_damage;
    void Start()
    {
        Destroy(gameObject,3f);
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(bulletSpeed, 0, 0);
    }
}