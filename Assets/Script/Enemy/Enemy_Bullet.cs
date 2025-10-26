using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float destroyTime;
    [SerializeField] int wave; //���̺�� ����ü ���� �����ϸ� ��� �޾ƾ��ϴ��� ���̾�!!!
    [SerializeField] private Player_Controller player_Controller;

    void Start()
    {
        Destroy(gameObject, destroyTime);
        GameObject player = GameObject.Find("Player");
        player_Controller = player.GetComponent<Player_Controller>();
    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� hp -1 ����");
            player_Controller.GetDamage((wave >= 10) ? 2 : 1);
        }
    }
}
