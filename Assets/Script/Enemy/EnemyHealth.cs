using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    private HealthBar healthBar;

    // Hasar alma fonksiyonu

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun ba�lang�� can�
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

    // D��man�n �lme i�lemi
    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        Destroy(gameObject); // D��man� sahneden kald�r
    }
}
