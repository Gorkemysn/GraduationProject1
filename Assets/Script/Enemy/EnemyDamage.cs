using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10; // K�l�c�n verece�i hasar miktar�
    public string targetTag = "Player"; // Hasar verilecek hedefin tag'i

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // K�l��, oyuncu ile temas ederse
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Oyuncuya hasar ver
                Debug.Log("Player hit by sword! Health decreased.");
            }
        }
    }
}