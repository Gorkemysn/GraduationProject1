using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Oyun zamanýný duraklat
        pauseMenuUI.SetActive(true); // Pause menüsünü göster
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        pauseMenuUI.SetActive(false); // Pause menüsünü kapat
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesine geri dön
    }
}
