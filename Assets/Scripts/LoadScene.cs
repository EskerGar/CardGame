using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void SceneLoad(string sceneName) => SceneManager.LoadScene(sceneName);
}
