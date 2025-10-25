using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float destroyTime;
    [SerializeField] int wave; //병합 과정에서 GameManager.instance.wave로 대체

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
