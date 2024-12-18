using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ScreenShakeManager : MonoSingleton<ScreenShakeManager>
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private bool isFalling;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ScreenShake(float power, bool isTween,float speed, bool autoEnd, float wait)
    {
        
        if (isTween)
        {
            DOTween.To(() => noise.m_AmplitudeGain, x => noise.m_AmplitudeGain = x, power, speed);
            if (autoEnd)
            {
                StartCoroutine(ScreenAutoEnd(wait, true, speed));
            }
        }
        else
        {
            noise.m_AmplitudeGain = power;
            if (autoEnd)
            {
                StartCoroutine(ScreenAutoEnd(wait, false, speed));
            }
        }

        
    }

    IEnumerator ScreenAutoEnd(float wait, bool tween, float speed)
    {
        yield return new WaitForSeconds(wait);
        if (isFalling)
            yield return null;
        if (tween)
            DOTween.To(() => noise.m_AmplitudeGain, x => noise.m_AmplitudeGain = x, 0, speed);
        else
            noise.m_AmplitudeGain = 0;

    }

    
    
}