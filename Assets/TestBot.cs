using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour
{
    public bool isAttack;
    public Player player;

    private void Awake()
    {
        player.OnnAttack += DDASDA;

    }

    private void Start()
    {
        player.CurrentHp = 100;
    }
    public void DDASDA()
    {
        StartCoroutine(DS());
    }
    private IEnumerator DS()
    {
        GameManager.Instance.FadeIn();
        yield return new WaitForSeconds(3f);
        GameManager.Instance.FadeOut();
        GameManager.Instance.isTitorialEnd = true;
        GameManager.Instance.NextStage();
    }
}
