using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void sceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
