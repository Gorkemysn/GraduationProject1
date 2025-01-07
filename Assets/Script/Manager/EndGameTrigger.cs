using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþleri için gerekli

public class EndGameTrigger : MonoBehaviour
{
    // Oyuncunun tetikleyiciye dokunup dokunmadýðýný kontrol eder
    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpýþan obje "Player" tag'ine sahipse
        if (other.CompareTag("Player"))
        {
            // Final sahnesini yükle
            LoadEndScene();
        }
    }

    // Final sahnesini yükleme fonksiyonu
    private void LoadEndScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // "EndScene" adýndaki sahneyi yükler
        SceneManager.LoadScene("EndScene");
    }
}
