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

    [SerializeField]
    int stageNum;

    public SpawnManager spawnManager;
    public bool isTitorialEnd;

    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";

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
        blackImage.raycastTarget = true;
    }

    public void FadeOut()
    {
        print("DSDA");
        blackImage.DOFade(0, 1);
        blackImage.raycastTarget = false;
    }
    public void WaitFadeOut()
    {
        blackImage.DOFade(0, 1).SetDelay(2);
        blackImage.raycastTarget = false;
    }

    public void NewScene()
    {
        ResetStage();
        ReadStage();
    }

    public void LoadScene()
    {
        if (UnCheckProgress())
        {
            ResetStage();
        }
        ReadStage();
    }
    private bool UnCheckProgress()//확인 필요
    {
        var reader = new StreamReader(new FileStream(path, FileMode.OpenOrCreate));
        stageNum = int.Parse(reader.ReadLine());
        reader.Close();
        if (stageNum == null) return true;
        else return false;
    }

    private void ResetStage()
    {
        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        writer.WriteLine($"{0}");
        writer.Close();
    }

    private void ReadStage()
    {
        var reader = new StreamReader(new FileStream(path, FileMode.OpenOrCreate));
        stageNum = int.Parse(reader.ReadLine());
        reader.Close();
    }

    public void MainLoadScene()
    {
        //    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";

        //    try
        //    {
        //        var reader = new StreamReader(new FileStream(path, FileMode.Open));
        //        stageNum = reader.ReadLine();
        //        reader.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        //        writer.WriteLine($"{stage}");
        //        writer.Close();
        //        var reader = new StreamReader(new FileStream(path, FileMode.Open));
        //        stageNum = reader.ReadLine();
        //        reader.Close();
        //    }

        //    stage = int.Parse(stageNum);

        //    switch (stage)
        //    {
        //        case 0:
        //            //보스 개구리
        //            SceneManager.LoadScene("Tutorial");
        //            break;
        //        case 1:
        //            //보스 개구리
        //            SceneManager.LoadScene("River");
        //            break;
        //        case 2:
        //            //보스 곰
        //            SceneManager.LoadScene("River");
        //            stage--;
        //            string paAth = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";
        //            print(paAth);
        //            var wriWWter = new StreamWriter(File.Open(paAth, FileMode.OpenOrCreate));
        //            wriWWter.WriteLine($"{stage}");
        //            wriWWter.Close();
        //            break;
        //        case 3:
        //            SceneManager.LoadScene("Forest");

        //            //보스 거미
        //            break;
        //        case 4:
        //            SceneManager.LoadScene("Forest");
        //            stage--;
        //            string pSSath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";
        //            print(pSSath);
        //            var wrSiter = new StreamWriter(File.Open(pSSath, FileMode.OpenOrCreate));
        //            wrSiter.WriteLine($"{stage}");
        //            wrSiter.Close();
        //            //보스 개구리
        //            break;
        //        case 5:
        //            //보스 곰
        //            SceneManager.LoadScene("Cave");

        //            break;
        //        case 6:
        //            SceneManager.LoadScene("Cave");
        //            stage--;
        //            string pX = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";
        //            print(pX);
        //            var wrSiSter = new StreamWriter(File.Open(pX, FileMode.OpenOrCreate));
        //            wrSiSter.WriteLine($"{stage}");
        //            wrSiSter.Close();
        //            //보스 거미
        //            break;
        //    }
        }

        //public void LoadScene()
        //{
        ////    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";

        ////    try
        ////    {
        ////        var reader = new StreamReader(new FileStream(path, FileMode.OpenOrCreate));
        ////        stageNum = reader.ReadLine();
        ////        reader.Close();
        ////    }
        ////    catch(Exception e)
        ////    {
        ////        var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        ////        writer.WriteLine($"{stage}");
        ////        writer.Close();
        ////        var reader = new StreamReader(new FileStream(path, FileMode.Open));
        ////        stageNum = reader.ReadLine();
        ////        reader.Close();
        ////    }

        ////    stage = int.Parse(stageNum);

        ////    switch (stage)
        ////    {
        ////        case 0:
        ////            //보스 개구리
        ////            SceneManager.LoadScene("Tutorial");
        ////            break;
        ////        case 1:
        ////            //보스 개구리
        ////            SceneManager.LoadScene("River");
        ////            break;
        ////        case 2:
        ////            //보스 곰
        ////            SceneManager.LoadScene("FrogBoss");
        ////            break;
        ////        case 3:
        ////            SceneManager.LoadScene("Forest");

        ////            //보스 거미
        ////            break;
        ////        case 4:
        ////            SceneManager.LoadScene("BearBoss");
        ////            //보스 개구리
        ////            break;
        ////        case 5:
        ////            //보스 곰
        ////            SceneManager.LoadScene("Cave");

        ////            break;
        ////        case 6:
        ////            SceneManager.LoadScene("SpiderBoss");
        ////            //보스 거미
        ////            break;
        ////            FadeOut();
        ////    }
        //}


        public void NextStage()
        {
        //    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\stage.txt";


        //    var reader = new StreamReader(new FileStream(path, FileMode.Open));
        //    stageNum = reader.ReadLine();
        //    reader.Close();
        //    stage = int.Parse(stageNum);
        //    stage++;

        //    print(path);
        //    var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate));
        //    writer.WriteLine($"{stage}");
        //    writer.Close();
        //    LoadScene();
        }

        public void RestartScene()
        {
        //    FadeOut();
        //    switch (stage)
        //    {
        //        case 0:
        //            //보스 개구리
        //            SceneManager.LoadScene("Tutorial");
        //            break;
        //        case 1:
        //            //보스 곰
        //            SceneManager.LoadScene("River");
        //            break;
        //        case 2:
        //            SceneManager.LoadScene("River");
        //            stage--;
        //            //보스 거미
        //            break;
        //        case 3:
        //            SceneManager.LoadScene("Forest");
        //            //보스 개구리
        //            break;
        //        case 4:
        //            //보스 곰
        //            stage--;
        //            SceneManager.LoadScene("Forest");
        //            break;
        //        case 5:
        //            //보스 곰
        //            SceneManager.LoadScene("Cave");
        //            break;
        //        case 6:
        //            //보스 곰
        //            stage--;
        //            SceneManager.LoadScene("Cave");
        //            break;

        //    }
        }
    }
