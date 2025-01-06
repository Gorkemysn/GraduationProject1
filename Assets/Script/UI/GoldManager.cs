using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance; // Singleton yap�s� i�in
    public TextMeshProUGUI goldText; // UI text
    private int currentGold = 0; // Alt�n miktar�

    private void Awake()
    {
        // Singleton yap�s�
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

    // Alt�n ekleme metodu
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    // UI'y� g�ncelle
    private void UpdateGoldUI()
    {
        goldText.text = "Alt�n: " + currentGold.ToString();
    }

    public int CurrentGold
    {
        get { return currentGold; }
    }

}
