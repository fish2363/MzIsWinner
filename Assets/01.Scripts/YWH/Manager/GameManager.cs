using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform player;

    public Image blackImage;
    private int stage;

    public Vector3 GetPlayerPosition()
    {
        return player.position; 
    }

    public void FadeIn()
    {
        blackImage.DOFade(1,1);
    }

    public void FadeOut()
    {
        blackImage.DOFade(0, 1);
    }
    public void NextStage()
    {
        stage++;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\password.txt";
        print(path);
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.Write($"{stage}");
        writer.Close();

        switch (stage)
        {
            case 1:
                //미니스테이지 강
                break;
            case 2:
                //보스 개구리
                break;
            case 3:
                //미니스테이지 동굴
                break;
            case 4:
                //보스 동굴
                break;
            case 5:
                //미니스테이지 숲
                break;
            case 6:
                //보스 숲
                break;
        }
    }
}
