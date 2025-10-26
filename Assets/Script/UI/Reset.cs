using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject mainMenu;
     public GameObject help;
    public GameObject die;
    public bool state=false;
    void Start()
    {
        if(state) MainMenu();
        else Dead();
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        help.SetActive(false);
        die.SetActive(false);
    }
    void Dead()
    {
        mainMenu.SetActive(false);
        help.SetActive(false);
        die.SetActive(true);
    }

}
