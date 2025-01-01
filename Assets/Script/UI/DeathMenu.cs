using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathScreenUI;

    private void Start()
    {
        HideDeathScreen();
    }

    public void ShowDeathScreen()
    {
        // Oyun zamanýný durdur
        Time.timeScale = 0f;
        deathScreenUI.SetActive(true); // Ölüm ekranýný göster
        Cursor.lockState = CursorLockMode.None; // Mouse'u serbest býrak
        Cursor.visible = true; // Mouse'u görünür yap
    }

    public void HideDeathScreen()
    {
        // Oyun zamanýný devam ettir
        Time.timeScale = 1f;
        deathScreenUI.SetActive(false); // Ölüm ekranýný kapat
        Cursor.lockState = CursorLockMode.Locked; // Mouse'u kilitle
        Cursor.visible = false; // Mouse'u gizle
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Oyun zamanýný devam ettir
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesine geri dön
    }
}
