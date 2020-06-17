using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ConfigSo configs;
    [SerializeField] private GameObject cardPrefab;
    public static GameManager Instance { get; private set; }
    
    private List<Sprite> _spriteList;

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        _spriteList = configs.GetSpriteList;
    }

    private void Start()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
        var startPos = configs.GetStartCord;
        var fieldProperty = configs.GetFieldProperty;
        var offset = configs.GetOffsetCord;
        for (int i = 0; i < fieldProperty.rowsAmount; i++)
        {
            for(int j = 0; j < fieldProperty.columnsAmount; j++)
            {
                Instantiate(cardPrefab, startPos + offset * new Vector2(j, i), Quaternion.identity);
            }
        }
    }
}
