using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] Transform Book;
    public void EndingEnd()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(Book.DOLocalRotate(new Vector3(0, 90, 0), 0.55f));
        seq.AppendInterval(1);
        seq.AppendCallback(() => {
            Application.Quit();
        });
    }
}
