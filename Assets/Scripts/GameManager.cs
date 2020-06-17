using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ConfigSo configs;
    [SerializeField] private GameObject cardPrefab;
    public static GameManager Instance { get; private set; }

    private Vector2 _offset = new Vector2(1.79f, -1.79f);
    private List<Sprite> spriteList;

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        spriteList = configs.GetSpriteList;
    }

    private void Start()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
        var startPos = configs.GetStartCord;
        for (int i = 0; i < spriteList.Count; i++)
        {
            for(int j = 0; j < spriteList.Count; j++)
            {
                Instantiate(cardPrefab, startPos + _offset * new Vector2(j, i), Quaternion.identity);
            }
        }
    }
}
