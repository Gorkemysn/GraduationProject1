using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    public float distanceFromPlayer = 5f;
    public float verticalOffset = 2f;

    private float yaw = 0f;
    private float pitch = 0f;
    private bool isMouseLocked = true;

    private PauseMenu pauseMenu; // PauseMenu script'ine referans

    void Start()
    {
        LockCursor();
        pauseMenu = FindObjectOfType<PauseMenu>(); // PauseMenu script'ini bul
    }

    void LateUpdate()
    {
        // Pause durumunda kamerayý durdur
        if (pauseMenu != null && pauseMenu.IsGamePaused())
            return;

        if (isMouseLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -30f, 30f);

            Vector3 direction = new Vector3(0, 0, -distanceFromPlayer);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.position = player.position + rotation * direction + Vector3.up * verticalOffset;
            transform.LookAt(player.position + Vector3.up * verticalOffset);
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMouseLocked = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isMouseLocked = false;
    }
}
