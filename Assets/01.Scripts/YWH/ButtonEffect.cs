using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private RectTransform button;
    private Vector2 temp; private Vector2 scaled;
    public UnityEvent OnClick;

    private void Awake()
    {
        button = GetComponentInChildren<RectTransform>();

        temp = button.localScale;

        scaled = temp * 1.3f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Click());

    }
    IEnumerator Click()
    {
        OnClick.Invoke();
        yield return new WaitForSeconds(0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.DOScale(scaled, 0.25f);
        button.DORotate(new Vector3(0,0,20), 1);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.DOScale(temp, 0.25f);
        button.DORotate(new Vector3(0, 0, 0), 1);
    }
}
