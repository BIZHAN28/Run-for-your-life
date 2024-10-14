using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Text timerText;
    public GameObject winPanel;
    private float startTime; 
    private bool gameFinished = false;

    void Start()
    {
        startTime = Time.time;
        winPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameFinished)
        {
            float timeElapsed = Time.time - startTime;
            timerText.text = FormatTime(timeElapsed);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameFinished)
        {
            other.transform.gameObject.GetComponent<CharacterController>().enabled = false;
            FinishGame();
        }
    }

    void FinishGame()
    {
		Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameFinished = true;
        float finalTime = Time.time - startTime;
        winPanel.SetActive(true);

        Text winText = winPanel.GetComponentInChildren<Text>();
        winText.text = "Победа!\nВремя: " + FormatTime(finalTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}