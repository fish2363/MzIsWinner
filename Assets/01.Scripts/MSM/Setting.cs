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
    private void Start()
    {
        VolumeSet("MasterVolume");
        VolumeSet("SFXVolume");
        VolumeSet("BackGroundVolume");
    }
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (setting.activeInHierarchy == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0;
            }
            setting.SetActive(setting.activeInHierarchy == false);
        }
    }
    public void VolumeSet(string name = "MasterVolume" , float newVolume = 1)
    {
        audioMixer.SetFloat(name, Mathf.Log10(newVolume) * 20);
    }
    public void MasterSet(float newVolume)
    {
        VolumeSet("MasterVolume", newVolume);
    }
    public void SFXSet(float newVolume)
    {
        VolumeSet("SFXVolume", newVolume);
    }
    public void BackGroundSet(float newVolume)
    {
        VolumeSet("BackGroundVolume", newVolume);
    }
}
