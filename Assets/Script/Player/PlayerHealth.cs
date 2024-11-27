using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Oyuncunun ba�lang�� can�
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Hasar� mevcut candan ��kar
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
            animator.SetTrigger("Die"); // �l�m animasyonunu tetikleyin
        }

        // Oyun mekani�ine g�re hareketleri durdurabilirsiniz
        GetComponent<PlayerController>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
    }
}
