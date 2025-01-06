using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance; // Singleton yapýsý için
    public TextMeshProUGUI goldText; // UI text
    private int currentGold = 0; // Altýn miktarý

    private void Awake()
    {
        // Singleton yapýsý
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGoldUI();
    }

    // Altýn ekleme metodu
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    // UI'yý güncelle
    private void UpdateGoldUI()
    {
        goldText.text = "Altýn: " + currentGold.ToString();
    }

    public int CurrentGold
    {
        get { return currentGold; }
    }

}
