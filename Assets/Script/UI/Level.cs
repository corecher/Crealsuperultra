using UnityEngine;
using UnityEngine.UI;


public class Level_UI : MonoBehaviour
{
    private Image Lev;
    private Player_Controller player_Controller;
    void Start()
    {
        Lev = GetComponent<Image>();
        GameObject player = GameObject.Find("Player");
        player_Controller=player.GetComponent<Player_Controller>();
    }
    void Update()
    {
        Lev.fillAmount=0.05f*player_Controller.level;
    }
}
