using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image fillBar;  // Dolum �ubu�u
    public float fillSpeed = 2f;  // Dolum h�z�
    private bool isFilling = false;  // Dolum i�lemi devam ediyor mu?
    private bool isSkillSelected = false;  // Yetenek se�ildi mi?

    public int goldCost = 20;  // Yetenek maliyeti
    public float moveSpeedIncrease = 0f;  // Hareket h�z� art���
    public float sprintSpeedIncrease = 0f;  // Sprint h�z� art���
    public int healthIncrease = 0;  // Sa�l�k art���
    public int staminaIncrease = 0;  // Stamina art���
    public int damageIncrease = 0;  // Hasar art���

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
        // Dolum i�lemi devam ediyorsa dolum �ubu�unu art�r
        if (isFilling && fillBar.fillAmount < 1f && !isSkillSelected)
        {
            fillBar.fillAmount += fillSpeed * Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSkillSelected) return;  // Yetenek zaten se�ildiyse i�lem yapma
        isFilling = true;  // Dolum i�lemine ba�la
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSkillSelected) return;  // Yetenek zaten se�ildiyse i�lem yapma
        isFilling = false;  // Dolum i�lemine son ver

        if (fillBar.fillAmount >= 1f)
        {
            TrySelectSkill();  // E�er dolum tamamland�ysa yetene�i se�
        }
        else
        {
            fillBar.fillAmount = 0f;  // Dolum tamamlanmad�ysa s�f�rla
        }
    }

    private void TrySelectSkill()
    {
        if (GoldManager.instance == null)
        {
            Debug.LogError("GoldManager bulunamad�!");
            return;
        }

        if (GoldManager.instance.CurrentGold >= goldCost)
        {
            GoldManager.instance.AddGold(-goldCost);  // Alt�n� d���r

            // Hareket ve sprint h�z�n� art�r
            if (moveSpeedIncrease > 0)
            {
                playerController.moveSpeed += moveSpeedIncrease;
            }

            if (sprintSpeedIncrease > 0)
            {
                playerController.sprintSpeed += sprintSpeedIncrease;
            }

            // Sa�l�k ve stamina art���
            if (healthIncrease > 0)
            {
                playerHealth.maxHealth += healthIncrease;
                playerHealth.currentHealth += healthIncrease;  // Mevcut sa�l��� da art�r
            }

            if (staminaIncrease > 0)
            {
                playerController.maxStamina += staminaIncrease;
            }

            // Hasar art���
            if (damageIncrease > 0)
            {
                combatAndSword.swordDamage += damageIncrease;
            }

            isSkillSelected = true;  // Yetenek ba�ar�yla se�ildi
            Debug.Log("Yetenek ba�ar�yla se�ildi!");
        }
        else
        {
            Debug.Log("Yeterli alt�n yok!");
        }
    }

    public void PointerDownWrapper()
    {
        OnPointerDown(null);  // PointerEventData null ge�ilebilir
    }

    public void PointerUpWrapper()
    {
        OnPointerUp(null);  // PointerEventData null ge�ilebilir
    }
}
