using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAnimator : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public void AttackingAnimation()
    {
        print("½ÇÇàµÊ");
        AnimationPlayer.Instance.PlayAnimaiton(player.AnimatorCompo,"PlayerAttacking");
    }
}
