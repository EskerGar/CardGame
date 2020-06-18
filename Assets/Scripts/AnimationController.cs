using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _anim;
    private readonly int cardFlip = Animator.StringToHash("CardFlip");
    private readonly Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();
    private Coroutine allAnimations;
    private void Awake() 
    {
        _anim = GetComponent<Animator>();
    }

    public void FlipCardAnimation(bool backFlip) 
    {
        animationQueue.Enqueue((PlayAnimation(cardFlip, backFlip)));
        if (allAnimations == null)
            allAnimations = StartCoroutine(AllAnimations());
    }

    private IEnumerator AllAnimations()
    {
        while (animationQueue.Count > 0)
            yield return StartCoroutine(animationQueue.Dequeue());
        allAnimations = null;
    }

    private IEnumerator PlayAnimation(int hash, bool state)
    {
        _anim.SetBool(hash, state);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
