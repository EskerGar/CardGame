using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs")]
public class ConfigSo : ScriptableObject
{
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private float timeToFlip;
    [SerializeField] private int healthAmount;
    [SerializeField] private Vector2 startCord = new Vector2(-1.8f, 3.92f);
    [SerializeField] private FieldProperty fieldProperty;
    public List<Sprite> GetSpriteList => spriteList;

    public float GetTimeToFlip => timeToFlip;

    public int GetHealthCount => healthAmount;

    public Vector2 GetStartCord => startCord;
    
    [Serializable]
    private class FieldProperty
    {
        public int columnsAmount;
        public int rowsAmount;
    }
}



