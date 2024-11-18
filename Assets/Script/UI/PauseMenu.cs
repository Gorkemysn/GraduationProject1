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
        Time.timeScale = 0f; // Oyun zaman�n� duraklat
        pauseMenuUI.SetActive(true); // Pause men�s�n� g�ster
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        pauseMenuUI.SetActive(false); // Pause men�s�n� kapat
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesine geri d�n
    }
}
