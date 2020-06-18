using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }

    public event Action OnDeath;

    public void Initialize(ConfigSo configs)
    {
        CurrentHealth = configs.GetHealthCount;
    }
    private void ProcessChange(int amount)
    {
        CurrentHealth += amount;
        if(CurrentHealth <= 0)
            OnOnDeath();
    }

    public void TakeDamage(int damage) => ProcessChange(-damage);

    private void OnOnDeath() => OnDeath?.Invoke();
}
