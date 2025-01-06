using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image fillBar;  // Dolum çubuðu
    public float fillSpeed = 2f;  // Dolum hýzý
    private bool isFilling = false;  // Dolum iþlemi devam ediyor mu?
    private bool isSkillSelected = false;  // Yetenek seçildi mi?

    public int goldCost = 20;  // Yetenek maliyeti
    public float moveSpeedIncrease = 0f;  // Hareket hýzý artýþý
    public float sprintSpeedIncrease = 0f;  // Sprint hýzý artýþý
    public int healthIncrease = 0;  // Saðlýk artýþý
    public int staminaIncrease = 0;  // Stamina artýþý
    public int damageIncrease = 0;  // Hasar artýþý

    private PlayerController2 playerController;
    private PlayerHealth playerHealth;
    private CombatAndSword combatAndSword;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController2>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        combatAndSword = FindAnyObjectByType<CombatAndSword>();
    }

    void Update()
    {
        // Dolum iþlemi devam ediyorsa dolum çubuðunu artýr
        if (isFilling && fillBar.fillAmount < 1f && !isSkillSelected)
        {
            fillBar.fillAmount += fillSpeed * Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSkillSelected) return;  // Yetenek zaten seçildiyse iþlem yapma
        isFilling = true;  // Dolum iþlemine baþla
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSkillSelected) return;  // Yetenek zaten seçildiyse iþlem yapma
        isFilling = false;  // Dolum iþlemine son ver

        if (fillBar.fillAmount >= 1f)
        {
            TrySelectSkill();  // Eðer dolum tamamlandýysa yeteneði seç
        }
        else
        {
            fillBar.fillAmount = 0f;  // Dolum tamamlanmadýysa sýfýrla
        }
    }

    private void TrySelectSkill()
    {
        if (GoldManager.instance == null)
        {
            Debug.LogError("GoldManager bulunamadý!");
            return;
        }

        if (GoldManager.instance.CurrentGold >= goldCost)
        {
            GoldManager.instance.AddGold(-goldCost);  // Altýný düþür

            // Hareket ve sprint hýzýný artýr
            if (moveSpeedIncrease > 0)
            {
                playerController.moveSpeed += moveSpeedIncrease;
            }

            if (sprintSpeedIncrease > 0)
            {
                playerController.sprintSpeed += sprintSpeedIncrease;
            }

            // Saðlýk ve stamina artýþý
            if (healthIncrease > 0)
            {
                playerHealth.maxHealth += healthIncrease;
                playerHealth.currentHealth += healthIncrease;  // Mevcut saðlýðý da artýr
            }

            if (staminaIncrease > 0)
            {
                playerController.maxStamina += staminaIncrease;
            }

            // Hasar artýþý
            if (damageIncrease > 0)
            {
                combatAndSword.swordDamage += damageIncrease;
            }

            isSkillSelected = true;  // Yetenek baþarýyla seçildi
            Debug.Log("Yetenek baþarýyla seçildi!");
        }
        else
        {
            Debug.Log("Yeterli altýn yok!");
        }
    }

    public void PointerDownWrapper()
    {
        OnPointerDown(null);  // PointerEventData null geçilebilir
    }

    public void PointerUpWrapper()
    {
        OnPointerUp(null);  // PointerEventData null geçilebilir
    }
}
