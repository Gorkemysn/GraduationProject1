using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause menüsü UI'ýný temsil eder
    private bool isPaused = false;

    private CamFollow cameraScript; // Kamera script'ine eriþim için referans

    private void Start()
    {
        // Kamera script'ine eriþ
        cameraScript = FindObjectOfType<CamFollow>();
    }

    private void Update()
    {
        // ESC tuþuna basýldýðýnda pause menüsünü aç/kapat
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

        // Fareyi serbest býrak (kamera kontrolünü devre dýþý býrak)
        if (cameraScript != null)
        {
            cameraScript.UnlockCursor();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        pauseMenuUI.SetActive(false); // Pause menüsünü kapat
        isPaused = false;

        // Fareyi kilitle (kamera kontrolünü tekrar etkinleþtir)
        if (cameraScript != null)
        {
            cameraScript.LockCursor();
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesine geri dön
    }

    public bool IsGamePaused()
    {
        return isPaused; // Oyun duraklatýldý mý?
    }
}
