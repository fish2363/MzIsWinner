using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]GameObject setting;
    [SerializeField]AudioMixer audioMixer;
    [SerializeField] bool isCanESC = true;
    [SerializeField] Slider master, sFX, backGround;
    public static float Master = 1;
    public static float SFX = 1;
    public static float BackGround = 1;
    private void Awake()
    {
        setting.SetActive(false);
    }
    private void Start()
    {
        master.value = Master;
        sFX.value = SFX;
        backGround.value = BackGround;
        VolumeSet("MasterVolume" , Master);
        VolumeSet("SFXVolume" , SFX);
        VolumeSet("BackGroundVolume" , BackGround);
    }
    private void Update()
    {
        if (isCanESC)
        {    
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
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
    }
    public void VolumeSet(string name = "MasterVolume" , float newVolume = 1)
    {
        audioMixer.SetFloat(name, Mathf.Log10(newVolume) * 20);
    }
    public void MasterSet(float newVolume)
    {
        VolumeSet("MasterVolume", newVolume);
        Master = newVolume;
    }
    public void SFXSet(float newVolume)
    {
        VolumeSet("SFXVolume", newVolume);
        SFX = newVolume;
    }
    public void BackGroundSet(float newVolume)
    {
        VolumeSet("BackGroundVolume", newVolume);
        BackGround = newVolume;
    }
    public void SetActive()
    {
        SoundManager.Instance.ChangeMainStageVolume("Click", true, ISOund.SFX);
        setting.SetActive(!setting.activeInHierarchy);
    }
}
