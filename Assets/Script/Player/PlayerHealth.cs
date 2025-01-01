using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    public HealthBar healthBar;
    public DeathMenu deathMenu; // DeathMenu referans�

    private bool isDead = false; // �l�m durumunu takip etmek i�in bir bayrak

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun ba�lang�� can�
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // E�er zaten �l� ise i�lem yapma

        currentHealth -= damage; // Hasar� mevcut candan ��kar
        healthBar.SetHealth(currentHealth); // UI g�ncelle

        Debug.Log("Player Health: " + currentHealth);

        // Can s�f�ra ula�t���nda �lme i�lemini tetikle
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // �l�m i�lemi tekrar �al��mas�n
        isDead = true;

        Debug.Log("Player Died!");

        // Karakterin kontrol�n� kapat
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // �l�m ekran�n� g�ster
        if (deathMenu != null)
        {
            deathMenu.ShowDeathScreen();
        }
        else
        {
            Debug.LogError("DeathMenu is not assigned in the Inspector.");
        }
    }
}
