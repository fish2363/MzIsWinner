using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartFade : MonoBehaviour
{
    Image fade;
    private void Awake()
    {
        fade = GetComponent<Image>();
    }

    private void Start()
    {
        fade.DOFade(0, 1f);
        fade.raycastTarget = false;
    }
}
