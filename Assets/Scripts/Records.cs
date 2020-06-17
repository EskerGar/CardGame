using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Records : MonoBehaviour
{
    public static float BestScore { get; private set; }

    private void Awake()
    {
        BestScore = PlayerPrefs.HasKey("BestScore") ? PlayerPrefs.GetFloat("BestScore") : 0;
    }

    public static void NewBestScore(float newScore)
    {
        BestScore = newScore;
        PlayerPrefs.SetFloat("BestScore", BestScore);
    }
}
