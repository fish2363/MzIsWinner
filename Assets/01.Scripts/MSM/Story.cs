using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Story : MonoBehaviour
{
    [SerializeField] RectTransform[] storys;
    [SerializeField] float timer;
    [SerializeField] RectTransform Book;
    [SerializeField] float bookTimer;
    private void Awake()
    {
        StoryStart();
    }
    private void StoryStart()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(timer);
        seq.Append(Book.DOLocalRotate(new Vector3(0,90,0), bookTimer));
        seq.AppendCallback(() => {
            storys[0].gameObject.SetActive(false);
            storys[1].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.AppendInterval(timer);

        seq.Append(Book.DOLocalRotate(new Vector3(0, 90, 0), bookTimer));
        seq.AppendCallback(() => {
            storys[2].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.AppendInterval(timer);

        seq.Append(Book.DOLocalRotate(new Vector3(0, 90, 0), bookTimer));
        seq.AppendCallback(() => {
            storys[1].gameObject.SetActive(false);
            storys[2].gameObject.SetActive(false);
            storys[3].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.AppendInterval(timer);

        seq.Append(Book.DOLocalRotate(new Vector3(0, 90, 0), bookTimer));
        seq.AppendCallback(() => {
            storys[3].gameObject.SetActive(false);
            storys[4].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.AppendInterval(timer);

        seq.Append(Book.DOLocalRotate(new Vector3(0, 90, 0), bookTimer));
        seq.AppendCallback(() => {
            storys[4].gameObject.SetActive(false);
            storys[5].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.AppendInterval(timer);

        seq.Append(Book.DOLocalRotate(new Vector3(0, 90, 0), bookTimer));
        seq.AppendCallback(() => {
            storys[5].gameObject.SetActive(false);
            storys[6].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.Append(storys[6].DOScaleX(-3.6f,0));
        seq.AppendInterval(0.75f);
        seq.Append(storys[6].DOScaleX(3.6f, 0));
        seq.AppendInterval(0.5f);
        for(int i = 0; i < 2; i++)
        {
            seq.Append(storys[6].DOScaleX(-3.6f, 0));
            seq.AppendInterval(0.25f);
            seq.Append(storys[6].DOScaleX(3.6f, 0));
            seq.AppendInterval(0.25f);
        }
        seq.AppendCallback(() => {
            storys[6].gameObject.SetActive(false);
            storys[7].gameObject.SetActive(true);
        });
        seq.Join(Book.DOLocalRotate(new Vector3(0, 0, 0), bookTimer));
        seq.AppendInterval(timer * 2);
    }
    public void RePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextScene()
    {
        SceneManager.LoadScene("Main");
    }
}
