using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    public static bool gameOver = false;
    private bool isHandling = false;

    void Start()
    {
        gameOver = false;
        isHandling = false;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Car") && !isHandling)
        {
            isHandling = true;
            HandleGameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && !isHandling)
        {
            isHandling = true;
            HandleGameOver();
        }
    }

    void HandleGameOver()
    {
        if (gameOver) return;

        gameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "OH NO!\nYou got hit by a car!\n\nScore: "
                + ScoreManager.Instance.GetScore()
                + "\n\nPress R to restart!";

        Time.timeScale = 0f;

        Debug.Log("GAME OVER - Hit by car!");
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        gameOver = false;
        isHandling = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}