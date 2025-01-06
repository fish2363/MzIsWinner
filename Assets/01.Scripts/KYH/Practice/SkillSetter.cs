using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetter : MonoBehaviour
{
    private Dictionary<BeeType, Bee> beeCategory = new Dictionary<BeeType, Bee>();


    public SkillSetter()
    {
        foreach(BeeType beeType in Enum.GetValues(typeof(BeeType)))
        {
            Type t = Type.GetType($"{beeType}Bee");
            Bee bee = Activator.CreateInstance(t) as Bee;
            beeCategory.Add(beeType,bee);
        }
    }

    public void UseSkill(BeeType currentBee,Player player)
    {
        beeCategory[currentBee].Skill(player);
    }
}
