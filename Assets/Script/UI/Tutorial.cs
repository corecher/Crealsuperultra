using TreeEditor;
using Unity.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int index=0;
    private bool upDown;
    void Update()
    {
        Move();    
    }
    void Move()
    {
        Vector2 pos = transform.position;
        if(upDown)
        {
            pos.x+=Time.deltaTime;
            pos.y+=Time.deltaTime;
        }
        else
        {
            pos.x-=Time.deltaTime;
            pos.y-=Time.deltaTime;
        }
        transform.position=pos;
        if(transform.position.y<-1.3f)
        {
            upDown=true;
        }
        else if(transform.position.y>1.3f)
        {
            upDown=false;
        }
    }
}
