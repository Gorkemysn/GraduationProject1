using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþleri için gerekli

public class BackToMenuButton : MonoBehaviour
{
    // Bu fonksiyon, buton týklandýðýnda çaðrýlýr
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükler
    }
}
