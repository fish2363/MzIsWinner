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
    private int stage;

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
        if (Input.GetMouseButtonDown(0))
            NextStage();
        if (Input.GetMouseButtonDown(1))
            RestartScene();
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
                //���� ������
                SceneManager.LoadScene("MiniStage1");
                break;
            case 2:
                //���� ��
                SceneManager.LoadScene("FrogBoss");
                break;
            case 3:
                SceneManager.LoadScene("MiniStage2");

                //���� �Ź�
                break;
            case 4:
                SceneManager.LoadScene("BearBoss");
                //���� ������
                break;
            case 5:
                //���� ��
                SceneManager.LoadScene("MiniStage3");

                break;
            case 6:
                SceneManager.LoadScene("SpiderBoss");
                //���� �Ź�
                break;
        }
    }

    public void RestartScene()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\password.txt";
        print(path);
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.Write($"{stage}");
        writer.Close();

        switch (stage)
        {
            case 0:
                //���� ������
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                //���� ��
                SceneManager.LoadScene("MiniStage1");
                break;
            case 2:
                SceneManager.LoadScene("MiniStage1");
                stage--;
                //���� �Ź�
                break;
            case 3:
                SceneManager.LoadScene("MiniStage2");
                //���� ������
                break;
            case 4:
                //���� ��
                stage--;
                SceneManager.LoadScene("MiniStage2");
                break;
            case 5:
                //���� ��
                SceneManager.LoadScene("MiniStage3");
                break;
            case 6:
                //���� ��
                stage--;
                SceneManager.LoadScene("MiniStage3");
                break;
        }
    }
}
