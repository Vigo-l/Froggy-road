using UnityEngine;
using UnityEngine.SceneManagement; // For scene reloading

public class GoalController : MonoBehaviour
{
    public GameObject winScreen; // Assign the "You Win" screen in the inspector
    public float restartDelay = 3f; // Time before restarting the game

    private bool gameWon = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !gameWon)
        {
            gameWon = true;
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true); // Display the "You Win" screen
        }
        Invoke("RestartGame", restartDelay); // Schedule game restart
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
