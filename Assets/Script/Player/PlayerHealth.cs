using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun baþlangýç caný
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasarý mevcut candan çýkar
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Ölüm animasyonunu tetikleyin
        }

        // Oyun mekaniðine göre hareketleri durdurabilirsiniz
        GetComponent<PlayerController>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
    }
}
