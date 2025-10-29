using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour
{
    [SerializeField]private List<Sprite> images;
    [SerializeField]private List<Image> Hearts;
    [SerializeField]private Player_Controller player_Controller;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        player_Controller = player.GetComponent<Player_Controller>();
    }
    void Update()
    {
        HeartChange();
        
    }
    void HeartChange()
    {
     if(player_Controller.hp>=0)
     switch(player_Controller.hp)
        {
           case 0: for(int i=0;i<Hearts.Count;i++)
           {
                Hearts[i].sprite = images[0];
           } StartCoroutine(GameOver());break;
           case 1: Hearts[0].sprite = images[1];
           for(int i=1;i<Hearts.Count;i++)
           {
                Hearts[i].sprite = images[0];
           } break;
           case 2:
            Hearts[0].sprite = images[2];
            for(int i=1;i<Hearts.Count;i++)
           {
                Hearts[i].sprite = images[0];
           }break;
           case 3: 
            for(int i=0;i<Hearts.Count;i++)
            {
                Hearts[i].sprite=images[2-i];
            }break;
           case 4: Hearts[2].sprite = images[0]; 
           for(int i=0;i<Hearts.Count-1;i++)
           {
                Hearts[i].sprite = images[2];
           } break;
           case 5: Hearts[2].sprite = images[1];
           for(int i=0;i<Hearts.Count-1;i++)
           {
                Hearts[i].sprite = images[2];
           } break;
           default:for(int i=0;i<Hearts.Count;i++)
           {
                Hearts[i].sprite = images[2];
           } break;
        }
    }
    IEnumerator GameOver()
    {
      yield return new WaitForSeconds(2f);
      SceneManager.LoadScene("Dead");
    }
}
