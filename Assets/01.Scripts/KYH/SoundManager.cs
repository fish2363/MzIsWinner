using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ISOund
{
    BGM,
    SFX
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("--------------BackGroundAudio--------------")]
    [SerializeField] AudioClip[] music;


    private ISOund soundType;

    [SerializeField]
    private AudioSource bgmAudioPlayer;
    [SerializeField]
    private AudioSource vfxAudioPlayer;

    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    //private string _currentScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (AudioClip audioClip in music)
        {
            audioDic.Add(audioClip.name, audioClip);
        }

        bgmAudioPlayer.clip = null;
    }

    public void ChangeMainStageVolume(string mainClip, bool isPlay, ISOund soundType)
    {
        if (soundType == ISOund.BGM)
        {
            bgmAudioPlayer.Pause();
            bgmAudioPlayer.clip = audioDic[mainClip];
            if (isPlay)
                bgmAudioPlayer.Play();
            else
                bgmAudioPlayer.Pause();
        }
        if (soundType == ISOund.SFX)
        {
            AudioSource soundPlayer = Instantiate(vfxAudioPlayer);
            soundPlayer.clip = audioDic[mainClip];
            soundPlayer.Play();
            StartCoroutine(SoundEnd(soundPlayer));
        }
    }

    private IEnumerator SoundEnd(AudioSource soundPlayer)
    {
        yield return new WaitForSeconds(soundPlayer.clip.length);
        Destroy(soundPlayer.gameObject);
    }
}
