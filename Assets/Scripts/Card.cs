using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private AnimationController _animController;
    private bool isTouched;

    public void Initialize()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animController = GetComponent<AnimationController>();
    }

    public  void ChangeSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

    private void FlipCard()
    {
        _animController.FlipCardAnimation(false, false);
    }

    public void BackFlipCard()
    {
        _animController.FlipCardAnimation(true, true);
    }
    
    private void OnMouseDown()
    {
        if (GameManager.Instance.IsBlockedControl) return;
        FlipCard();
        GameManager.Instance.FlipCard(gameObject);
    }

    public void DeleteCard() => StartCoroutine(DeleteCardCoroutine());

    private IEnumerator DeleteCardCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
