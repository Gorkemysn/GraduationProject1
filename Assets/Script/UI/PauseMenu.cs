using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Pause men�s� UI'�n� temsil eder
    private bool isPaused = false;

    private CamFollow cameraScript; // Kamera script'ine eri�im i�in referans

    private void Start()
    {
        // Kamera script'ine eri�
        cameraScript = FindObjectOfType<CamFollow>();
    }

    private void Update()
    {
        // ESC tu�una bas�ld���nda pause men�s�n� a�/kapat
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

        // Fareyi serbest b�rak (kamera kontrol�n� devre d��� b�rak)
        if (cameraScript != null)
        {
            cameraScript.UnlockCursor();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        pauseMenuUI.SetActive(false); // Pause men�s�n� kapat
        isPaused = false;

        // Fareyi kilitle (kamera kontrol�n� tekrar etkinle�tir)
        if (cameraScript != null)
        {
            cameraScript.LockCursor();
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zaman�n� devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesine geri d�n
    }

    public bool IsGamePaused()
    {
        return isPaused; // Oyun duraklat�ld� m�?
    }
}
