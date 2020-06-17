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
    public static GameManager Instance { get; private set; }
    public GameObject GetCardPrefab => cardPrefab;

    public ConfigSo GetConfigs => configs;
    
    private List<Sprite> _spriteList;
    private Dictionary<GameObject, Sprite> _cardsDictionary = new Dictionary<GameObject, Sprite>();
    private ConfigSo.FieldProperty _fieldProperty;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _spriteList = configs.GetSpriteList;
        _fieldProperty = configs.GetFieldProperty;
    }

    private void Start()
    {
        GenerateCards();
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
        Assert.IsTrue(fieldSize % _spriteList.Count == 0, "Field size should be devided by the number of sprites");
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
    
}
