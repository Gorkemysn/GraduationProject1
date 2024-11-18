using UnityEngine;
using TMPro;

public class NPCDialogueSystem : MonoBehaviour
{
    // Diyalog metinlerini tutmak i�in bir dizi
    public string[] dialogueLines;

    // Diyalog kutusunun UI objesi ve TextMeshPro bile�eni
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public GameObject questionMark;

    // JournalManager referans�
    public JournalUI journalUI;

    // Diyalog ilerlemesi i�in gerekli de�i�kenler
    private int currentLine = 0;
    private bool playerInRange = false; // Oyuncunun NPC'ye yak�n olup olmad���n� kontrol etmek i�in
    private bool hasSpoken = false;

    // Her NPC'ye �zg� benzersiz bir ID ve ad�
    public string npcID;
    public string npcName;

    void Start()
    {
        dialogueUI.SetActive(false); // Diyalog kutusunu ba�ta gizle
        questionMark.SetActive(true);
    }

    void Update()
    {
        // Oyuncu NPC'ye yak�nken ve E tu�una bas�ld���nda diyalogu ba�lat/ilerlet
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextDialogueLine();
        }
    }

    private void ShowNextDialogueLine()
    {
        // Diyalog sona erdiyse
        if (currentLine >= dialogueLines.Length)
        {
            dialogueUI.SetActive(false); // Diyalog kutusunu gizle
            currentLine = 0; // Diyalog s�ras�n� s�f�rla

            if (!hasSpoken)
            {
                hasSpoken = true; // NPC ile konu�uldu�unu i�aretle
                questionMark.SetActive(false); // Soru i�aretini gizle
            }

            return;
        }

        // Diyalog devam ediyorsa
        dialogueUI.SetActive(true); // Diyalog kutusunu g�ster
        dialogueText.text = dialogueLines[currentLine]; // Mevcut diyalog sat�r�n� g�ster

        // E�er bu diyalog sat�r� daha �nce g�nl��e eklenmemi�se kaydet
        if (!journalUI.HasEntry($"{npcID}-{dialogueLines[currentLine]}"))
        {
            journalUI.AddEntry(npcName, dialogueLines[currentLine]); // NPC ad� ile diyalogu ekle
        }

        currentLine++; // Sonraki sat�ra ge�
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Oyuncu yak�nda
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Oyuncu uzakla�t�
            dialogueUI.SetActive(false); // Diyalog kutusunu gizle
            currentLine = 0; // Diyalog s�ras�n� s�f�rla
        }
    }
}
