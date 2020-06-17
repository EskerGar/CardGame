using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;
    private bool _cardFlip = false;
    private Animator _anim;
    public void Initialize()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    public  void ChangeSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

    private void OnMouseDown()
    {
        _cardFlip = !_cardFlip;
        _anim.SetBool("MouseDown", _cardFlip);
    }
}
