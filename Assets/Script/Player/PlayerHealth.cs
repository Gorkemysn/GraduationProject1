using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    public HealthBar healthBar;
    public DeathMenu deathMenu; // DeathMenu referansý

    private bool isDead = false; // Ölüm durumunu takip etmek için bir bayrak

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun baþlangýç caný
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Eðer zaten ölü ise iþlem yapma

        currentHealth -= damage; // Hasarý mevcut candan çýkar
        healthBar.SetHealth(currentHealth); // UI güncelle

        Debug.Log("Player Health: " + currentHealth);

        // Can sýfýra ulaþtýðýnda ölme iþlemini tetikle
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Ölüm iþlemi tekrar çalýþmasýn
        isDead = true;

        Debug.Log("Player Died!");

        // Karakterin kontrolünü kapat
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Ölüm ekranýný göster
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
