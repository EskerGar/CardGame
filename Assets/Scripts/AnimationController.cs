using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _anim;
    private readonly int cardFlip = Animator.StringToHash("CardFlip");
    private readonly Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();
    private Coroutine allAnimations;
    private bool isBlockedControl;
    private void Awake() 
    {
        _anim = GetComponent<Animator>();
    }

    public void FlipCardAnimation(bool backFlip, bool blockControl)
    {
        isBlockedControl = blockControl;
        AddAnimation(backFlip);
        if (allAnimations == null)
            allAnimations = StartCoroutine(AllAnimations());
    }

    private void AddAnimation(bool backFlip) => animationQueue.Enqueue((PlayAnimation(cardFlip, backFlip)));

    private IEnumerator AllAnimations()
    {
        while (animationQueue.Count > 0)
        {
            if (isBlockedControl)
                GameManager.Instance.IsBlockedControl = true;
            yield return StartCoroutine(animationQueue.Dequeue());
        }
        allAnimations = null;
        if (isBlockedControl)
            GameManager.Instance.IsBlockedControl = false;
    }

    private IEnumerator PlayAnimation(int hash, bool state)
    {
        _anim.SetBool(hash, state);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
