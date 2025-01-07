using UnityEngine;
using UnityEngine.SceneManagement; // Sahne ge�i�leri i�in gerekli

public class EndGameTrigger : MonoBehaviour
{
    // Oyuncunun tetikleyiciye dokunup dokunmad���n� kontrol eder
    private void OnTriggerEnter(Collider other)
    {
        // E�er �arp��an obje "Player" tag'ine sahipse
        if (other.CompareTag("Player"))
        {
            // Final sahnesini y�kle
            LoadEndScene();
        }
    }

    // Final sahnesini y�kleme fonksiyonu
    private void LoadEndScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // "EndScene" ad�ndaki sahneyi y�kler
        SceneManager.LoadScene("EndScene");
    }
}
