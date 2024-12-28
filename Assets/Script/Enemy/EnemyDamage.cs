using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10; // Kýlýcýn vereceði hasar miktarý
    public string targetTag = "Player"; // Hasar verilecek hedefin tag'i

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Kýlýç, oyuncu ile temas ederse
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