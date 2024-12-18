using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoSingleton<AnimationPlayer>
{
    private Animator PlayerAnimator;

    public void PlayAnimaiton(Animator animator,string animationType)
    {
        PlayerAnimator = animator;
        Play(animationType);
    }

    public void StopAnimation()
    {
        PlayerAnimator.enabled = false;
    }
    public void StartAnimation()
    {
        PlayerAnimator.enabled = true;
    }

    private void Play(string name)
    {
        PlayerAnimator.StopPlayback();
        PlayerAnimator.Play(name);
    }

}
