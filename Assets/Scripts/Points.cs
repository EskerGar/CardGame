using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public static float BestScore { get; private set; }
    private float currentScore;

    public event Action<float> OnChangeScore;
    public event Action OnNewBestScore;
    public event Action<float, float> OnViewRecords; 

    private void Awake()
    {
        BestScore = PlayerPrefs.HasKey("BestScore") ? PlayerPrefs.GetFloat("BestScore") : 0;
    }

    private void Start()
    {
        AddPoints(0);
        GameManager.Instance.OnEndGame += CheckScore;
    }

    private void CheckScore(bool isWin)
    {
        if (currentScore > BestScore)
        {
            BestScore = currentScore;
            PlayerPrefs.SetFloat("BestScore", BestScore);
            OnNewBestScore?.Invoke();
        }
        OnViewRecords?.Invoke(BestScore, currentScore);
    }

    public void AddPoints(float amount) => ProcessChangeScore(amount);

    private void ProcessChangeScore(float amount)
    {
        currentScore += amount;
        OnChangeScore?.Invoke(currentScore);

    }
}
