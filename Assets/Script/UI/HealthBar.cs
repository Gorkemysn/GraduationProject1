using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;      // Kýrmýzý slider
    [SerializeField] private Slider easeHealthSlider; // Sarý slider
    public float lerpSpeed;    // Sarý slider'ýn azalma hýzý

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        if (easeHealthSlider != null)
        {
            easeHealthSlider.maxValue = health;
            easeHealthSlider.value = health; // Baþlangýçta eþit olsun
        }
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    private void Update()
    {
        if (healthSlider.value != easeHealthSlider.value) 
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
}
