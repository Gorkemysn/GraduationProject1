using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class JournalUI : MonoBehaviour
{
    public GameObject journalUI; // Günlük UI'si (Panel)
    public TextMeshProUGUI journalText; // Günlükte görünecek diyalog metinleri

    // NPC baþýna diyaloglarý saklamak için Dictionary
    private Dictionary<string, List<string>> npcDialogues = new Dictionary<string, List<string>>();
    private bool isJournalOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab tuþuna basýldýðýnda günlüðü aç/kapat
        {
            if (isJournalOpen)
            {
                CloseJournal();
            }
            else
            {
                OpenJournal();
            }
        }
    }

    public void AddEntry(string npcName, string entryText)
    {
        // Eðer NPC için zaten bir liste yoksa, yeni bir liste oluþtur
        if (!npcDialogues.ContainsKey(npcName))
        {
            npcDialogues[npcName] = new List<string>();
        }

        // Diyalog daha önce eklenmediyse ekle
        if (!npcDialogues[npcName].Contains(entryText))
        {
            npcDialogues[npcName].Add(entryText);
            UpdateJournalText();
        }
    }

    public bool HasEntry(string entryText)
    {
        // npcDialogues sözlüðünde tüm NPC'lerin diyaloglarý kontrol ediliyor
        foreach (var dialogues in npcDialogues.Values)
        {
            if (dialogues.Contains(entryText))
            {
                return true; // Eðer diyalog daha önce eklenmiþse true döndür
            }
        }
        return false; // Diyalog bulunamazsa false döndür
    }

    private void UpdateJournalText()
    {
        journalText.text = ""; // Günlük metnini sýfýrla

        // Her NPC'nin diyaloglarýný yazdýr
        foreach (var npcEntry in npcDialogues)
        {
            string npcName = npcEntry.Key;
            List<string> dialogues = npcEntry.Value;

            journalText.text += npcName + ":\n"; // NPC adýný baþlýk olarak ekle

            // Her bir diyalog satýrýný alt satýra ekle
            foreach (string dialogue in dialogues)
            {
                journalText.text += "  - " + dialogue + "\n";
            }

            journalText.text += "\n"; // Her NPC için araya boþluk býrak
        }
    }

    public void OpenJournal()
    {
        journalUI.SetActive(true);
        isJournalOpen = true;
    }

    public void CloseJournal()
    {
        journalUI.SetActive(false);
        isJournalOpen = false;
    }
}

