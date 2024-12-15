using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    public HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun baþlangýç caný
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasarý mevcut candan çýkar
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player Health: " + currentHealth);
        
        Animator animator = GetComponent<Animator>();
        // Hasar alma animasyonunu tetikle
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }

        // Can sýfýra ulaþtýðýnda ölme iþlemini tetikle
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
            animator.ResetTrigger("TakeDamage");
        }

        // Oyun mekaniðine göre hareketleri durdurabilirsiniz
        GetComponent<PlayerController>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
    }
}
