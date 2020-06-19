using UnityEngine;
using UnityEngine.UI;

public class StartSceneContorller : MonoBehaviour
{
    [SerializeField] private Text bestScore;

    private void Start()
    {
        Points.Initialize();
        bestScore.GetComponent<ViewScore>().ShowScore(Points.BestScore);
    }
}
