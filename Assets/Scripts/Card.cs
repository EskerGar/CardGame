using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;
    public void Initialize()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public  void ChangeSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;
}
