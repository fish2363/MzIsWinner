using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Setting : MonoBehaviour
{
    [SerializeField]GameObject setting;
    [SerializeField]AudioMixer audioMixer;
    private void Awake()
    {
        setting.SetActive(false);
    }
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 0;
            setting.SetActive(setting.activeInHierarchy == false);
        }
    }
    public void MasterVolume(float newVolume)
    {
        audioMixer.SetFloat("Master",Mathf.Clamp01(newVolume));
    }
    //public void SetVolume(float newVolume)
    //{
    //    volume = Mathf.Clamp01(newVolume);
    //    _audioSource.volume = volume; // AudioSource º¼·ý ¹Ý¿µ
    //}

    //public void SetBgmVolume(float newBgmVolume)
    //{
    //    bgmVolume = Mathf.Clamp01(newBgmVolume);
    //}
}
