using UnityEngine;
using UnityEngine.UI;

public class StartSceneContorller : MonoBehaviour
{
    [SerializeField] private Text bestScore;

    private void Start() => bestScore.GetComponent<ViewScore>().ShowScore(Points.BestScore);
}
