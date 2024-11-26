using UnityEngine;

public class CombatAndSword : MonoBehaviour
{
    // Animator ve saldýrý parametreleri
    private Animator animator;
    public string attackTriggerName = "Attack"; // Animator'da tanýmlý saldýrý trigger'ý

    // Kýlýç için gerekli bileþenler
    public GameObject sword; // Kýlýç objesi
    public Transform swordHitbox; // Kýlýcýn hasar bölgesi
    public float swordDamage = 10f; // Kýlýç hasar deðeri
    public LayerMask enemyLayers; // Düþmanlarý belirlemek için katman maskesi
    public float attackRange = 1f; // Saldýrý mesafesi

    // Saldýrý kontrol deðiþkenleri
    private bool isAttacking = false; // Saldýrý sýrasýnda baþka bir saldýrýyý önlemek için kontrol

    void Start()
    {
        // Animator bileþenini al
        animator = GetComponent<Animator>();

        // Kýlýcýn baþlangýçta aktif olduðundan emin olun
        if (sword == null)
        {
            Debug.LogError("Sword GameObject'i atanmadý!");
        }
        else
        {
            sword.SetActive(true);
        }
    }

    void Update()
    {
        // Saldýrý için giriþ kontrolü
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Sol fare tuþu ile saldýrý
        {
            Attack();
        }
    }

    void Attack()
    {
        // Animator'da saldýrý tetikleyicisini çalýþtýr
        if (animator != null)
        {
            animator.SetTrigger(attackTriggerName);
        }

        // Saldýrý sýrasýnda baþka bir saldýrýyý engelle
        isAttacking = true;

        // Saldýrýyý hasar verme iþlemiyle senkronize etmek için gecikme ekleyin
        Invoke("PerformAttack", 0.1f); // 0.3 saniye sonra hasar iþlemini çaðýr
        Invoke("ResetAttack", 0.1f); // 1 saniye sonra saldýrý durumunu sýfýrla
    }

    void PerformAttack()
    {
        // Kýlýcýn hitbox'ýna temas eden düþmanlarý tespit et
        Collider[] hitEnemies = Physics.OverlapSphere(swordHitbox.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // Düþmana hasar ver
            Debug.Log(enemy.name + " has been hit!");

            // Eðer düþmanda bir saðlýk sistemi varsa, hasar uygula
            var health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(swordDamage);
            }
        }
    }

    void ResetAttack()
    {
        // Saldýrý kontrolünü sýfýrla
        isAttacking = false;
    }

    // Saldýrý menzilini görselleþtirmek için
    void OnDrawGizmosSelected()
    {
        if (swordHitbox == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(swordHitbox.position, attackRange);
    }
}
