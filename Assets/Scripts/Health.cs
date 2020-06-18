using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth { get; private set; }

    public event Action OnDeath, OnHealthDecrease;

    public void Initialize(ConfigSo configs)
    {
        CurrentHealth = configs.GetHealthCount;
    }
    private void ProcessChange(int amount)
    {
        var prevHealth = CurrentHealth;
        CurrentHealth += amount;
        if(CurrentHealth < prevHealth)
            OnHealthDecrease?.Invoke();
        if (CurrentHealth <= 0)
            OnDeath?.Invoke();
    }

    public void TakeDamage(int damage) => ProcessChange(-damage);
    
}
