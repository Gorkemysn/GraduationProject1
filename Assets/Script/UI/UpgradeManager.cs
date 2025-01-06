using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    public GameObject menuUI; // Menü UI objesi
    private bool isMenuActive = false;

    PlayerInput playerInput;

    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Main.Enable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            isMenuActive = !isMenuActive;
            ToggleMenu(isMenuActive);
        }
    }

    void ToggleMenu(bool isMenuActive)
    {
        if (menuUI != null)
        {
            menuUI.SetActive(isMenuActive);
        }

        if (isMenuActive)
        {
            Cursor.lockState = CursorLockMode.None; // Mouse'u serbest býrak
            Cursor.visible = true;                 // Mouse imlecini görünür yap
            Time.timeScale = 0;                    // Oyunu durdur
            playerInput.Main.Disable();            // Kontrolleri devre dýþý býrak
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Mouse'u kilitle
            Cursor.visible = false;                  // Mouse imlecini gizle
            Time.timeScale = 1;                      // Oyunu devam ettir
            playerInput.Main.Enable();               // Kontrolleri etkinleþtir
        }
    }
}
