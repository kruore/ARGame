using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPrefAnimation : MonoBehaviour
{
    public void AnimaionAttack()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("attackA_1of2");
        }
    }
    public void AnimaionDamaged()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("damaged_back");
        }
    }
    public void AnimationSuccess()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("emotion_success");
        }
    }
    public void AnimationWin()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("emotion_success");
        }
    }
    public void AnimationLoss()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("standA_tired@loop");
        }
    }
    public void AnimationDraw()
    {
        Animator[] childAnimator;
        childAnimator = GetComponentsInChildren<Animator>();
        Debug.Log(childAnimator.Length);
        foreach (Animator anim in childAnimator)
        {
            anim.Play("standA_wave@loop");
        }
    }
}
