using UnityEngine;
using UnityEngine.EventSystems;

public class EnforceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rect;
    private Vector2 originalSize;
    private Vector2 targetSize;
    private float growAmount = 100f; 
    private float lerpSpeed = 10f;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        originalSize = rect.sizeDelta;
        targetSize = originalSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetSize = originalSize + new Vector2(growAmount, growAmount);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetSize = originalSize;
    }

    void Update()
    {
        rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, targetSize, Time.deltaTime * lerpSpeed);
    }
}

