using UnityEngine;
using UnityEngine.SceneManagement; // Sahne ge�i�leri i�in gerekli

public class BackToMenuButton : MonoBehaviour
{
    // Bu fonksiyon, buton t�kland���nda �a�r�l�r
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ana men� sahnesini y�kler
    }
}
