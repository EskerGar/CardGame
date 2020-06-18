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
        _animController.FlipCardAnimation(false);
        _animController.FlipCardAnimation(false);
    }

    public void BackFlipCard()
    {
        _animController.FlipCardAnimation(true);
    }
    
    private void OnMouseDown()
    {
        if (GameManager.Instance.IsBlockedControll) return;
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
