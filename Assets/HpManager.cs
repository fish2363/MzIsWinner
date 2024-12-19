using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HpManager : MonoBehaviour
{
    public static HpManager Instance;

    public TextMeshProUGUI text;
    public int currentHp;
    public GameObject dsad;

    public void SetActiveHp()
    {
        dsad.SetActive(true);
        HpChange(SpawnManager.Instance.currentChracter.maxHp);
    }

    public void HpChange(int hp)
    {
        currentHp = hp;
        text.text = currentHp.ToString();
    }
}