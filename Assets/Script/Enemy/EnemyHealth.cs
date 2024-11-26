using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f; // D��man�n toplam sa�l���

    // Hasar alma fonksiyonu
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy Health: " + health);

        if (health <= 0)
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
