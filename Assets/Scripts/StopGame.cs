using UnityEngine;
using UnityEngine.SceneManagement;

public class StopGame : MonoBehaviour
{
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
