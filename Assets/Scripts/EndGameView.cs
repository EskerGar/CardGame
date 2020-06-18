using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    [SerializeField] private Text winText;
    [SerializeField] private Text newRecordText;
    [SerializeField] private Text bestRecordText;
    [SerializeField] private Text playerRecordText;

    private Points _points;

    private void Start()
    {
        gameObject.SetActive(false);
        _points = GameManager.Instance.GetPointClass;
        GameManager.Instance.OnEndGame += EndGame;
        _points.OnNewBestScore += NewBestScore;
        _points.OnViewRecords += ViewRecords;
    }

    private void EndGame(bool isWin)
    {
        gameObject.SetActive(true);
        winText.text = isWin ? "You Win" : "You Lose";
    }

    private void NewBestScore() => newRecordText.gameObject.SetActive(true);

    private void ViewRecords(float bestRecord, float playerRecord)
    {
        bestRecordText.GetComponent<ViewScore>().ShowScore(bestRecord);
        playerRecordText.GetComponent<ViewScore>().ShowScore(playerRecord);
    }
}
