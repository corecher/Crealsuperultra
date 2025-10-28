using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> enemy = new List<GameObject>();
    public int[] posY=new int[9]{-4,-3,-2,-1,0,1,2,3,4};
    public int[] posX=new int[4]{2,4,6,8};
    public int waveCount=0;
    public int enemyspCount=0;
    public int enemyCount=0;
    private int index=0;
    private bool WaveState=true;
    private float timer=0f;
    private int ii=0;
    void Update()
    {
        if(WaveState)
        WaveStart();
        else if(enemyspCount>0)
        WaveUpdate();
        Debug.Log(index);
        WaveStop();
    }
    void WaveStart()
    {
        waveCount++;
        enemyspCount=20;
        enemyCount=0;
        WaveState=false;
    }
    void WaveStop()
    {
        if(enemyspCount<=0)
        {
            StopCoroutine("SpawEn");
        }
    }
    void WaveUpdate()
    { 
        if(timer>5)
        {
            timer=0f;
            StartCoroutine(SpawEn());
        }
        else timer+=Time.deltaTime;
        
    }
    IEnumerator SpawEn()
    {
        while(enemyspCount>0)
        {
            enemyspCount--;
            enemyCount++;
            yield return new WaitForSeconds(5f);
            int i=Random.Range(0,3); 
            if(index>=posY.Count()-1) index=index%2;
            else index+=2;
            ii++;
            if(ii>posX.Count()-1) ii=0;
            Vector2 spwPos = new Vector2(posX[ii],posY[index]);
            Instantiate(enemy[i],spwPos,transform.rotation);
        }
    }

}
