using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    public HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun ba�lang�� can�
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar� mevcut candan ��kar
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player Health: " + currentHealth);
        
        Animator animator = GetComponent<Animator>();
        // Hasar alma animasyonunu tetikle
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }

        // Can s�f�ra ula�t���nda �lme i�lemini tetikle
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
            animator.SetTrigger("Die"); // �l�m animasyonunu tetikleyin
            animator.ResetTrigger("TakeDamage");
        }

        // Oyun mekani�ine g�re hareketleri durdurabilirsiniz
        GetComponent<PlayerController>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
    }
}
