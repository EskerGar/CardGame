using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private Points _points;

    private void Start()
    {
        _points = GameManager.Instance.GetPointClass;
        _points.OnChangeScore += ChangeTextScore;
    }

    private void ChangeTextScore(float score) => scoreText.text = "Score: " + score.ToString();
}
