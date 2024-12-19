using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private int idx;
    [SerializeField]
    private string[] chat;

    [SerializeField]
    private GameObject spiderBall;

    [SerializeField]
    private TestBot testBot;
    public GameObject aim;

    private bool isis = true;

    private void Awake()
    {
        player.isStopMove = true;
    }

    public void pLAYERmOVEeNABLE()
    {
        player.isStopMove = false;
    }

    public void SpiderBall()
    {
        if(!isis)
        {
            spiderBall.GetComponent<SpriteRenderer>().DOFade(0f,0.2f);
        }
        spiderBall.SetActive(isis);
        isis = !isis;
    }

    public void AttackStart()
    {
        testBot.isAttack = true;
        player.isAttackLock = false;
        aim.SetActive(true);
    }
    
    public void Chat()
    {
        ChatManager.Instance.TextMessage(chat[idx],1,Color.white,TextStyle.UI);
        idx++;
    }

    public void Skill()
    {
        player.isSkillLock = false;
    }
}
