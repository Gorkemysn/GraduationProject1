using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;      // K�rm�z� slider
    [SerializeField] private Slider easeHealthSlider; // Sar� slider
    public float lerpSpeed;    // Sar� slider'�n azalma h�z�

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        if (easeHealthSlider != null)
        {
            easeHealthSlider.maxValue = health;
            easeHealthSlider.value = health; // Ba�lang��ta e�it olsun
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
