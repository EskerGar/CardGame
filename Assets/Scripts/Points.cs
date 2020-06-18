using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public static float BestScore { get; private set; }
    private float currentScore;

    public event Action<float> OnChangeScore;

    private void Awake()
    {
        BestScore = PlayerPrefs.HasKey("BestScore") ? PlayerPrefs.GetFloat("BestScore") : 0;
    }

    private void Start()
    {
        AddPoints(0);
    }

    public static void NewBestScore(float newScore)
    {
        BestScore = newScore;
        PlayerPrefs.SetFloat("BestScore", BestScore);
    }

    public void AddPoints(float amount) => ProcessChangeScore(amount);

    private void ProcessChangeScore(float amount)
    {
        currentScore += amount;
        OnChangeScore?.Invoke(currentScore);

    }
}
