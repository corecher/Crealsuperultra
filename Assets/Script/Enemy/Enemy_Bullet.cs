using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float destroyTime;
    [SerializeField] int wave; //���� �������� GameManager.instance.wave�� ��ü


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
            Debug.Log(" ÷  ̾  hp -1     ");
            //player.GetDamage((wave >= 10) ? 2 : 1);
        }
    }
}
