using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ScreenShakeManager : MonoSingleton<ScreenShakeManager>
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private CinemachineConfiner2D confiner;
    
    private CanvasGroup vignette;
    [SerializeField] private GameObject particleSystem;

    private bool isFalling;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        vignette = GetComponentInChildren<CanvasGroup>();
        confiner = GetComponent<CinemachineConfiner2D>();

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

    public void AttackEffect()
    {
        ScreenShake(5, true, 0.2f, true, 1.5f);
        if(particleSystem != null)
        particleSystem.gameObject.SetActive(true);

        var transposer = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
        if (transposer != null)
        {
            transposer.m_XDamping = 0f;
            transposer.m_YDamping = 0f;
        }

        vignette.DOFade(1, 0.2f);

       
        DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, x => virtualCamera.m_Lens.OrthographicSize = x, 2.3f, 1f).OnComplete(() => 
        { DOTween.To(() => virtualCamera.m_Lens.Dutch, x => virtualCamera.m_Lens.Dutch = x, 18f, 1f); });


        
    }
    public void SuccessAttack()
    {
        particleSystem.gameObject.SetActive(false);
        ScreenShake(20, true, 0.2f, true, 0.5f);
        vignette.DOFade(0, 0.2f);
        DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, x => virtualCamera.m_Lens.OrthographicSize = x, 4f, 0.5f).OnComplete(() =>
        { DOTween.To(() => virtualCamera.m_Lens.Dutch, x => virtualCamera.m_Lens.Dutch = x, 0f, 0.5f); });


    }



}
