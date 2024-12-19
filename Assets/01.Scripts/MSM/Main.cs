using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject Continue;
    public void StartButton()
    {
        Continue.SetActive(true);
        SoundManager.Instance.ChangeMainStageVolume("Click", true, ISOund.SFX);
    }
    public void ExitButton()
    {
        SoundManager.Instance.ChangeMainStageVolume("Click", true, ISOund.SFX);
        Application.Quit();
    }
    public void Story()
    {
        SoundManager.Instance.ChangeMainStageVolume("Click", true, ISOund.SFX);
        SceneManager.LoadScene("Story");
    }
}
