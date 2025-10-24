using System.Linq;
using UnityEngine;

public class DeadCredit : MonoBehaviour
{
    public Reset reset;
    void Start()
    {
        transform.position=new Vector2(0,-5);
    }
    void Update()
    {
       Vector2 mo = transform.position;
       mo.y+=Time.deltaTime;
       transform.position=mo;
       if(transform.position.y>32f)
       {
            reset.MainMenu();
       }
    }
}
