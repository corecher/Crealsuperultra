using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
public class TalkManager : MonoBehaviour
{
    public static TalkManager Talkmanager;
    [Header("대화 리스트")]
    public Text talkText;
    public string[] talkLines;
    [Header("대화창 설정")]
    public float typingSpeed = 0.05f;
    public int currentLineIndex = 0;
    private bool isTyping = false;
    private float timer=0f;
    [SerializeField]private float maxTime=5f;
    public Reset reset;
    void Awake()
    {
        Talkmanager = this;
    }
    private void Update()
    {
       if(timer>maxTime)
        {
            timer=0f;
            Talk();
        }
       else timer+=Time.deltaTime; 
    }
    public void Talk()
    {
            if (isTyping)
            {
                StopAllCoroutines();
                talkText.text = talkLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                if (currentLineIndex < talkLines.Length)
                {
                    StartCoroutine(TypeLine());
                }
                else
                {
                    reset.MainMenu();
                    Debug.Log("대화 종료");
                    return;
                }
            }
            currentLineIndex++;
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        talkText.text = "";
        string line = talkLines[currentLineIndex];

        foreach (char letter in line)
        {
            talkText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
