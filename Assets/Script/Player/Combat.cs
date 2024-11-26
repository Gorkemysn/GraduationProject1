using UnityEngine;

public class CombatAndSword : MonoBehaviour
{
    // Animator ve sald�r� parametreleri
    private Animator animator;
    public string attackTriggerName = "Attack"; // Animator'da tan�ml� sald�r� trigger'�

    // K�l�� i�in gerekli bile�enler
    public GameObject sword; // K�l�� objesi
    public Transform swordHitbox; // K�l�c�n hasar b�lgesi
    public float swordDamage = 10f; // K�l�� hasar de�eri
    public LayerMask enemyLayers; // D��manlar� belirlemek i�in katman maskesi
    public float attackRange = 1f; // Sald�r� mesafesi

    // Sald�r� kontrol de�i�kenleri
    private bool isAttacking = false; // Sald�r� s�ras�nda ba�ka bir sald�r�y� �nlemek i�in kontrol

    void Start()
    {
        // Animator bile�enini al
        animator = GetComponent<Animator>();

        // K�l�c�n ba�lang��ta aktif oldu�undan emin olun
        if (sword == null)
        {
            Debug.LogError("Sword GameObject'i atanmad�!");
        }
        else
        {
            sword.SetActive(true);
        }
    }

    void Update()
    {
        // Sald�r� i�in giri� kontrol�
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Sol fare tu�u ile sald�r�
        {
            Attack();
        }
    }

    void Attack()
    {
        // Animator'da sald�r� tetikleyicisini �al��t�r
        if (animator != null)
        {
            animator.SetTrigger(attackTriggerName);
        }

        // Sald�r� s�ras�nda ba�ka bir sald�r�y� engelle
        isAttacking = true;

        // Sald�r�y� hasar verme i�lemiyle senkronize etmek i�in gecikme ekleyin
        Invoke("PerformAttack", 0.1f); // 0.3 saniye sonra hasar i�lemini �a��r
        Invoke("ResetAttack", 0.1f); // 1 saniye sonra sald�r� durumunu s�f�rla
    }

    void PerformAttack()
    {
        // K�l�c�n hitbox'�na temas eden d��manlar� tespit et
        Collider[] hitEnemies = Physics.OverlapSphere(swordHitbox.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // D��mana hasar ver
            Debug.Log(enemy.name + " has been hit!");

            // E�er d��manda bir sa�l�k sistemi varsa, hasar uygula
            var health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(swordDamage);
            }
        }
    }

    void ResetAttack()
    {
        // Sald�r� kontrol�n� s�f�rla
        isAttacking = false;
    }

    // Sald�r� menzilini g�rselle�tirmek i�in
    void OnDrawGizmosSelected()
    {
        if (swordHitbox == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swordHitbox.position, attackRange);
    }
}
