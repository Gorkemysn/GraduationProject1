using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    private HealthBar healthBar;

    // Hasar alma fonksiyonu

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun baþlangýç caný
        healthBar = GetComponentInChildren<HealthBar>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy Health: " + currentHealth);

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Düþmanýn ölme iþlemi
    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        Destroy(gameObject); // Düþmaný sahneden kaldýr
    }
}
