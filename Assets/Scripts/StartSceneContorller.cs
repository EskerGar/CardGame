using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneContorller : MonoBehaviour
{
    [SerializeField] private Text bestScore;
        
    void Start()
    {
        bestScore.GetComponent<ViewScore>().ShowScore(Records.BestScore);
    }
    
}
