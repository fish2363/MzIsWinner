using UnityEngine;
using DG.Tweening;

public class WaveEffect : MonoBehaviour
{
    public float floatDistanceX = 0.5f; 
    public float floatDistanceY = 0.3f;
    public float floatDuration = 1f;  

    private void Start()
    {
      
        transform.DOBlendableMoveBy(new Vector3(floatDistanceX, floatDistanceY, 0), floatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}


