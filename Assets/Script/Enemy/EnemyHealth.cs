using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;
    private HealthBar healthBar;
    private Animator animator; // Animasyon bile�eni
    private bool isDead = false; // �l�m kontrol�

    void Start()
    {
        currentHealth = maxHealth; // Ba�lang�� can�
        healthBar = GetComponentInChildren<HealthBar>();
        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // D��man �ld�yse hasar almay� durdur

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
        if (isDead) return; // �l�m i�lemi birden fazla kez �al��t�r�lmas�n
        isDead = true;

        Debug.Log(gameObject.name + " has been defeated!");

        // �l�m animasyonunu tetikle
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // �l�m animasyonu tamamland���nda d��man� yok et
        Destroy(gameObject, 2f); // 2 saniye sonra d��man yok edilir
    }
}
