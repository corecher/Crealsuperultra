using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
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
    void Awake()
    {
        Talkmanager = this;
        timer=maxTime;
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
                    Debug.Log("대화 종료");
                    SceneManager.LoadScene("UI_UX");
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
