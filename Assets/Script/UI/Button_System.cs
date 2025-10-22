using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button_System : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 Mypos;
    [SerializeField] private float movepos = 20f;
    [SerializeField] private GameObject helpPanel;

    void Start()
    {
        Mypos = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Mypos.x += movepos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Mypos.x -= movepos;
    }

    void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition,Mypos,Time.deltaTime * 10);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void HelpGame(bool ch)
    {
        helpPanel.SetActive(ch);
    }
    public void LeaveGame()
    {
       Debug.Log("게임 종료");
       Application.Quit();
    }
}

