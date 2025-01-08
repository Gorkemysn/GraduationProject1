using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillButton : MonoBehaviour
{
    private bool isSkillSelected = false;  // Yetenek se�ildi mi?
    private bool isSkillSelectable = false;  // Yetenek se�ilebilir mi?

    public int goldCost = 20;  // Yetenek maliyeti
    public float moveSpeedIncrease = 0f;  // Hareket h�z� art���
    public float sprintSpeedIncrease = 0f;  // Sprint h�z� art���
    public int healthIncrease = 0;  // Sa�l�k art���
    public int staminaIncrease = 0;  // Stamina art���
    public int damageIncrease = 0;  // Hasar art���

    public List<SkillButton> nextSkills; // Ba�l� sonraki skilller

    public Color lockedColor = Color.gray; // Kilitli skill rengi
    public Color selectableColor = Color.white; // Se�ilebilir skill rengi
    public Color selectedColor = Color.green; // Se�ilen skill rengi

    private Image skillImage; // Skill d��mesinin g�rseli
    private PlayerController2 playerController;
    private PlayerHealth playerHealth;
    private CombatAndSword combatAndSword;

    void Start()
    {
        skillImage = GetComponent<Image>();
        playerController = FindObjectOfType<PlayerController2>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        combatAndSword = FindAnyObjectByType<CombatAndSword>();

        // Skill ba�lang�� durumunu g�ncelle
        UpdateSkillVisual();

        // Ba�lang��ta sadece 1. seviyedeki skill'leri se�ilebilir yap
        if (gameObject.name == "HealthSkill-1" || gameObject.name == "MovementSkill-1" || gameObject.name == "AttackSkill-1")
        {
            SetSelectable(); // Bu skill'ler se�ilebilir
        }
    }

    void Update()
    {
        // Se�im yap�lamazsa, ba�lang��ta kilitli renk g�sterilsin.
        if (!isSkillSelectable && !isSkillSelected)
        {
            skillImage.color = lockedColor;
        }
    }

    public void OnClick()
    {
        // E�er skill se�ilebilir de�ilse veya zaten se�ildiyse i�lem yap�lmaz.
        if (!isSkillSelectable || isSkillSelected)
        {
            Debug.Log("Bu yetenek se�ilemez!");
            return;
        }

        TrySelectSkill();
    }

    private void TrySelectSkill()
    {
        if (GoldManager.instance == null)
        {
            Debug.LogError("GoldManager bulunamad�!");
            return;
        }

        // Alt�n kontrol�
        if (GoldManager.instance.CurrentGold >= goldCost)
        {
            GoldManager.instance.AddGold(-goldCost);  // Alt�n� d���r

            // Yetenek etkilerini uygula
            ApplySkillEffects();

            isSkillSelected = true; // Yetenek se�ildi
            Debug.Log("Yetenek ba�ar�yla se�ildi!");

            // Ba�l� skilleri a�
            SelectSkill();
        }
        else
        {
            Debug.Log("Yeterli alt�n yok!");
        }
    }

    private void ApplySkillEffects()
    {
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
            playerHealth.currentHealth += healthIncrease;  // Mevcut sa�l��� art�r
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
    }

    private void SelectSkill()
    {
        UpdateSkillVisual(); // Mevcut skilli g�ncelle

        // Ba�l� skill'leri a�
        foreach (SkillButton nextSkill in nextSkills)
        {
            nextSkill.SetSelectable();
        }
    }

    public void SetSelectable()
    {
        isSkillSelectable = true; // Skill se�ilebilir oldu
        UpdateSkillVisual();
    }

    private void UpdateSkillVisual()
    {
        if (isSkillSelected)
        {
            skillImage.color = selectedColor; // Yetenek se�ilmi� rengi
        }
        else if (isSkillSelectable)
        {
            skillImage.color = selectableColor; // Se�ilebilir rengi
        }
        else
        {
            skillImage.color = lockedColor; // Kilitli rengi
        }
    }
}
