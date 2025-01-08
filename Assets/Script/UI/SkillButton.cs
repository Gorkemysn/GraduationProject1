using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillButton : MonoBehaviour
{
    private bool isSkillSelected = false;  // Yetenek seçildi mi?
    private bool isSkillSelectable = false;  // Yetenek seçilebilir mi?

    public int goldCost = 20;  // Yetenek maliyeti
    public float moveSpeedIncrease = 0f;  // Hareket hýzý artýþý
    public float sprintSpeedIncrease = 0f;  // Sprint hýzý artýþý
    public int healthIncrease = 0;  // Saðlýk artýþý
    public int staminaIncrease = 0;  // Stamina artýþý
    public int damageIncrease = 0;  // Hasar artýþý

    public List<SkillButton> nextSkills; // Baðlý sonraki skilller

    public Color lockedColor = Color.gray; // Kilitli skill rengi
    public Color selectableColor = Color.white; // Seçilebilir skill rengi
    public Color selectedColor = Color.green; // Seçilen skill rengi

    private Image skillImage; // Skill düðmesinin görseli
    private PlayerController2 playerController;
    private PlayerHealth playerHealth;
    private CombatAndSword combatAndSword;

    void Start()
    {
        skillImage = GetComponent<Image>();
        playerController = FindObjectOfType<PlayerController2>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        combatAndSword = FindAnyObjectByType<CombatAndSword>();

        // Skill baþlangýç durumunu güncelle
        UpdateSkillVisual();

        // Baþlangýçta sadece 1. seviyedeki skill'leri seçilebilir yap
        if (gameObject.name == "HealthSkill-1" || gameObject.name == "MovementSkill-1" || gameObject.name == "AttackSkill-1")
        {
            SetSelectable(); // Bu skill'ler seçilebilir
        }
    }

    void Update()
    {
        // Seçim yapýlamazsa, baþlangýçta kilitli renk gösterilsin.
        if (!isSkillSelectable && !isSkillSelected)
        {
            skillImage.color = lockedColor;
        }
    }

    public void OnClick()
    {
        // Eðer skill seçilebilir deðilse veya zaten seçildiyse iþlem yapýlmaz.
        if (!isSkillSelectable || isSkillSelected)
        {
            Debug.Log("Bu yetenek seçilemez!");
            return;
        }

        TrySelectSkill();
    }

    private void TrySelectSkill()
    {
        if (GoldManager.instance == null)
        {
            Debug.LogError("GoldManager bulunamadý!");
            return;
        }

        // Altýn kontrolü
        if (GoldManager.instance.CurrentGold >= goldCost)
        {
            GoldManager.instance.AddGold(-goldCost);  // Altýný düþür

            // Yetenek etkilerini uygula
            ApplySkillEffects();

            isSkillSelected = true; // Yetenek seçildi
            Debug.Log("Yetenek baþarýyla seçildi!");

            // Baðlý skilleri aç
            SelectSkill();
        }
        else
        {
            Debug.Log("Yeterli altýn yok!");
        }
    }

    private void ApplySkillEffects()
    {
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
            playerHealth.currentHealth += healthIncrease;  // Mevcut saðlýðý artýr
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
    }

    private void SelectSkill()
    {
        UpdateSkillVisual(); // Mevcut skilli güncelle

        // Baðlý skill'leri aç
        foreach (SkillButton nextSkill in nextSkills)
        {
            nextSkill.SetSelectable();
        }
    }

    public void SetSelectable()
    {
        isSkillSelectable = true; // Skill seçilebilir oldu
        UpdateSkillVisual();
    }

    private void UpdateSkillVisual()
    {
        if (isSkillSelected)
        {
            skillImage.color = selectedColor; // Yetenek seçilmiþ rengi
        }
        else if (isSkillSelectable)
        {
            skillImage.color = selectableColor; // Seçilebilir rengi
        }
        else
        {
            skillImage.color = lockedColor; // Kilitli rengi
        }
    }
}
