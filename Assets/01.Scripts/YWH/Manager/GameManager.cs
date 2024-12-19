using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform player;

    public Image blackImage;
    private int stage = 0;

    string stageNum;

    public SpawnManager spawnManager;
    public bool isTitorialEnd;

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
    }

    
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

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    NextStage();
        //if (Input.GetMouseButtonDown(1))
        //    RestartScene();
    }
    public void NewScene()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.WriteLine($"{stage}");
        writer.Close();
        var reader = new StreamReader(new FileStream(path, FileMode.Open));
        stageNum = reader.ReadLine();
        reader.Close();

        stage = int.Parse(stageNum);

        SceneManager.LoadScene("Tutorial");
    }

    public void LoadScene()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";

        try
        {
            var reader = new StreamReader(new FileStream(path, FileMode.Open));
            stageNum = reader.ReadLine();
            reader.Close();
        }
        catch(Exception e)
        {
            var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
            writer.WriteLine($"{stage}");
            writer.Close();
            var reader = new StreamReader(new FileStream(path, FileMode.Open));
            stageNum = reader.ReadLine();
            reader.Close();
        }

        stage = int.Parse(stageNum);

        switch (stage)
        {
            case 0:
                //보스 개구리
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                //보스 개구리
                SceneManager.LoadScene("River");
                break;
            case 2:
                //보스 곰
                SceneManager.LoadScene("FrogBoss");
                break;
            case 3:
                SceneManager.LoadScene("Forest");

                //보스 거미
                break;
            case 4:
                SceneManager.LoadScene("BearBoss");
                //보스 개구리
                break;
            case 5:
                //보스 곰
                SceneManager.LoadScene("Cave");

                break;
            case 6:
                SceneManager.LoadScene("SpiderBoss");
                //보스 거미
                break;
        }
    }


    public void NextStage()
    {
        stage++;

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";
        print(path);
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.WriteLine($"{stage}");
        writer.Close();
        LoadScene();
    }

    public void RestartScene()
    {
        switch (stage)
        {
            case 0:
                //보스 개구리
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                //보스 곰
                SceneManager.LoadScene("MiniStage1");
                break;
            case 2:
                SceneManager.LoadScene("MiniStage1");
                stage--;
                //보스 거미
                break;
            case 3:
                SceneManager.LoadScene("MiniStage2");
                //보스 개구리
                break;
            case 4:
                //보스 곰
                stage--;
                SceneManager.LoadScene("MiniStage2");
                break;
            case 5:
                //보스 곰
                SceneManager.LoadScene("MiniStage3");
                break;
            case 6:
                //보스 곰
                stage--;
                SceneManager.LoadScene("MiniStage3");
                break;
        }
    }
}
