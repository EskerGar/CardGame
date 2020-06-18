using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    private readonly int mouseDown = Animator.StringToHash("MouseDown");

    public void Initialize()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    public  void ChangeSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

    private void OnMouseDown()
    {
        FlipCard();
        GameManager.Instance.FlipCard(gameObject);
    }

    public void FlipCard()
    {
        _anim.SetBool(mouseDown, true);
    }

    public void DeleteCard()
    {
        StartCoroutine(DeleteCardCoroutine());
    }

    private IEnumerator DeleteCardCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
