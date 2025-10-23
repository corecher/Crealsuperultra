using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float destroyTime;
    [SerializeField] int wave; //웨이브는 도대체 누가 구현하며 어디서 받아야하느냔 말이야!!!

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            Debug.Log("플레이어 hp -1 하하");
            //player.GetDamage((wave >= 10) ? 2 : 1);
        }
    }
}
