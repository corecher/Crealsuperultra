using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

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
        switch(player_Controller.hp)
        {
           
        }
    }
}
