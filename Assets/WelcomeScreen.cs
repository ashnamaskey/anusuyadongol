using UnityEngine;
using TMPro;

public class WelcomeScreen : MonoBehaviour
{
    public GameObject welcomePanel;
    public TextMeshProUGUI scoreText;

    public static bool gameIsStarted = false;

    void Start()
    {
        gameIsStarted = false;

        // Show cursor so button can be clicked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;

        welcomePanel.SetActive(true);

        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log("BUTTON WORKS!");
        gameIsStarted = true;

        welcomePanel.SetActive(false);

        if (scoreText != null)
            scoreText.gameObject.SetActive(true);
    }
}