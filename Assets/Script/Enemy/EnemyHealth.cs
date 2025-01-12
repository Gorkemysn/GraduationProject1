using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    public int goldReward = 10; // Düþman öldüðünde kazanýlacak altýn miktarý
    private int currentHealth;
    private HealthBar healthBar;
    private Animator animator; // Animasyon bileþeni
    private bool isDead = false; // Ölüm kontrolü

    void Start()
    {
        currentHealth = maxHealth; // Baþlangýç caný
        healthBar = GetComponentInChildren<HealthBar>();
        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Düþman öldüyse hasar almayý durdur

        currentHealth -= amount;
        Debug.Log("Enemy Health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // Ölüm iþlemi birden fazla kez çalýþtýrýlmasýn
        isDead = true;

        Debug.Log(gameObject.name + " has been defeated!");

        // Altýn ekle
        GoldManager.instance.AddGold(goldReward);
        Debug.Log("Player earned " + goldReward + " gold!");

        // Ölüm animasyonunu tetikle
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Ölüm animasyonu tamamlandýðýnda düþmaný yok et
        Destroy(gameObject, 1f); // 2 saniye sonra düþman yok edilir
    }
}
