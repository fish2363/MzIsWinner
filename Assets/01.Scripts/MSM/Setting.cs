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
        VolumeSet("Master");
        VolumeSet("SFX");
        VolumeSet("BackGround");
    }
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Time.timeScale = 0;
            setting.SetActive(setting.activeInHierarchy == false);
        }
    }
    public void VolumeSet(string name = "Master" , float newVolume = 1)
    {
        audioMixer.SetFloat(name, Mathf.Clamp01(newVolume));
    }
    public void MasterSet(float newVolume)
    {
        VolumeSet("Master" , newVolume);
    }
    public void SFXSet(float newVolume)
    {
        VolumeSet("SFX", newVolume);
    }
    public void BackGroundSet(float newVolume)
    {
        VolumeSet("BackGround", newVolume);
    }
}
