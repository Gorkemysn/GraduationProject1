using UnityEngine;
using TMPro;

public class NPCDialogueSystem : MonoBehaviour
{
    // Diyalog metinlerini tutmak için bir dizi
    public string[] dialogueLines;

    // Diyalog kutusunun UI objesi ve TextMeshPro bileþeni
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public GameObject questionMark;

    // JournalManager referansý
    public JournalUI journalUI;

    // Diyalog ilerlemesi için gerekli deðiþkenler
    private int currentLine = 0;
    private bool playerInRange = false; // Oyuncunun NPC'ye yakýn olup olmadýðýný kontrol etmek için
    private bool hasSpoken = false;

    // Her NPC'ye özgü benzersiz bir ID ve adý
    public string npcID;
    public string npcName;

    void Start()
    {
        dialogueUI.SetActive(false); // Diyalog kutusunu baþta gizle
        questionMark.SetActive(true);
    }

    void Update()
    {
        // Oyuncu NPC'ye yakýnken ve E tuþuna basýldýðýnda diyalogu baþlat/ilerlet
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
            currentLine = 0; // Diyalog sýrasýný sýfýrla

            if (!hasSpoken)
            {
                hasSpoken = true; // NPC ile konuþulduðunu iþaretle
                questionMark.SetActive(false); // Soru iþaretini gizle
            }

            return;
        }

        // Diyalog devam ediyorsa
        dialogueUI.SetActive(true); // Diyalog kutusunu göster
        dialogueText.text = dialogueLines[currentLine]; // Mevcut diyalog satýrýný göster

        // Eðer bu diyalog satýrý daha önce günlüðe eklenmemiþse kaydet
        if (!journalUI.HasEntry($"{npcID}-{dialogueLines[currentLine]}"))
        {
            journalUI.AddEntry(npcName, dialogueLines[currentLine]); // NPC adý ile diyalogu ekle
        }

        currentLine++; // Sonraki satýra geç
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Oyuncu yakýnda
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Oyuncu uzaklaþtý
            dialogueUI.SetActive(false); // Diyalog kutusunu gizle
            currentLine = 0; // Diyalog sýrasýný sýfýrla
        }
    }
}
