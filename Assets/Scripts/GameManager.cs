using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ConfigSo configs;
    [SerializeField] private GameObject cardPrefab;

    private const int OPEN_CARD_NUMBER = 3;
    public static GameManager Instance { get; private set; }
    public bool IsBlockedControll { get; private set; }
    private bool _isWin;

    private List<Sprite> _spriteList;
    private readonly Dictionary<GameObject, Sprite> _cardsDictionary = new Dictionary<GameObject, Sprite>();
    private ConfigSo.FieldProperty _fieldProperty;
    private readonly Stack<GameObject> openedCardsStack = new Stack<GameObject>();
    private Health _health;
    private Points _points;
    private int _damage = 1;
    private GameObject _lastCard;

    public event Action<bool> OnEndGame; 

    public Points GetPointClass => _points;
    public Health GetHealthClass => _health;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _spriteList = configs.GetSpriteList;
        _fieldProperty = configs.GetFieldProperty;
        _health = GetComponent<Health>();
        _points = GetComponent<Points>();
        _health.Initialize(configs);
    }

    private void Start()
    {
        GenerateCards();
        _health.OnDeath += EndGame;
        StartCoroutine(StartGame());
    }

    private Sprite RandomSprite(Dictionary<Sprite, int> spriteCount, int cardsWithSprite)
    {
        while (true)
        {
            var sprite = _spriteList[Random.Range(0, _spriteList.Count)];
            if (spriteCount[sprite] >= cardsWithSprite) continue;
            spriteCount[sprite] += 1;
            return sprite;
        }
    }

    private void GenerateCards()
    {
        var fieldSize = _fieldProperty.columnsAmount * _fieldProperty.rowsAmount;
        Assert.IsTrue(fieldSize % OPEN_CARD_NUMBER == 0, "Field size should be devided by the number of sprites");
        var cardsWithSprite = fieldSize / _spriteList.Count; 
        var spriteCount = _spriteList.ToDictionary(sprite => sprite, sprite => 0);
        for (int i = 0; i < _fieldProperty.rowsAmount; i++)
        {
            for(int j = 0; j < _fieldProperty.columnsAmount; j++)
            {
                var sprite = RandomSprite(spriteCount, cardsWithSprite);
                _cardsDictionary.Add(CreateCard.Create(cardPrefab, configs, i, j, sprite), sprite);
            }
        }
    }

    private void CheckWin()
    {
        if(_cardsDictionary.Count == 0)
            EndGame();
    }

    private void DeleteCards(IEnumerable collect)
    {
        foreach (GameObject card in collect)
        {
            _lastCard = card;
            _cardsDictionary.Remove(card);
            card.GetComponent<Card>().DeleteCard();
        }
        CheckWin();
    }

    private void StackClear(IEnumerable collect)
    {
        foreach (var openedCard in openedCardsStack)
            openedCard.GetComponent<Card>().BackFlipCard();
        openedCardsStack.Clear();
    }

    public void FlipCard(GameObject card) 
    {
        if (openedCardsStack.Count > 0)
        {
            var prevCard = openedCardsStack.Peek();
            if (!_cardsDictionary[card].Equals(_cardsDictionary[prevCard]))
            {
                openedCardsStack.Push(card);
                StackClear(openedCardsStack);
                _health.TakeDamage(_damage);
            }
            else if (!openedCardsStack.Peek().Equals(card))
                openedCardsStack.Push(card);
        }
        else
            openedCardsStack.Push(card);

        if(openedCardsStack.Count == 2)
            _points.AddPoints(_health.CurrentHealth);
        
        if (openedCardsStack.Count != OPEN_CARD_NUMBER) return;
        
        _points.AddPoints(3 * _health.CurrentHealth);
        DeleteCards(openedCardsStack);
        openedCardsStack.Clear();
    }

    private void EndGame() => StartCoroutine(EndGameCoroutine());

    private IEnumerator EndGameCoroutine()
    {
        yield return new WaitUntil( () => _lastCard == null);
        IsBlockedControll = true;
        if (_health.CurrentHealth > 0)
            _isWin = true;
        OnEndGame?.Invoke(_isWin);
    }

    private IEnumerator StartGame()
    {
        IsBlockedControll = true;
        var allCards = _cardsDictionary.Keys.Select(card => card.GetComponent<Card>()).ToList();
        yield return new WaitForSeconds(configs.GetTimeToFlip);
        foreach (var card in allCards)
            card.BackFlipCard();
        IsBlockedControll = false;
    }
}
