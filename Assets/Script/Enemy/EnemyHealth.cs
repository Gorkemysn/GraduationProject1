using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f; // Düþmanýn toplam saðlýðý

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

    // Düþmanýn ölme iþlemi
    void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");
        Destroy(gameObject); // Düþmaný sahneden kaldýr
    }
}
