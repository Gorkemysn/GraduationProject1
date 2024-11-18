using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class JournalUI : MonoBehaviour
{
    public GameObject journalUI; // G�nl�k UI'si (Panel)
    public TextMeshProUGUI journalText; // G�nl�kte g�r�necek diyalog metinleri

    // NPC ba��na diyaloglar� saklamak i�in Dictionary
    private Dictionary<string, List<string>> npcDialogues = new Dictionary<string, List<string>>();
    private bool isJournalOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Tab tu�una bas�ld���nda g�nl��� a�/kapat
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
        // E�er NPC i�in zaten bir liste yoksa, yeni bir liste olu�tur
        if (!npcDialogues.ContainsKey(npcName))
        {
            npcDialogues[npcName] = new List<string>();
        }

        // Diyalog daha �nce eklenmediyse ekle
        if (!npcDialogues[npcName].Contains(entryText))
        {
            npcDialogues[npcName].Add(entryText);
            UpdateJournalText();
        }
    }

    public bool HasEntry(string entryText)
    {
        // npcDialogues s�zl���nde t�m NPC'lerin diyaloglar� kontrol ediliyor
        foreach (var dialogues in npcDialogues.Values)
        {
            if (dialogues.Contains(entryText))
            {
                return true; // E�er diyalog daha �nce eklenmi�se true d�nd�r
            }
        }
        return false; // Diyalog bulunamazsa false d�nd�r
    }

    private void UpdateJournalText()
    {
        journalText.text = ""; // G�nl�k metnini s�f�rla

        // Her NPC'nin diyaloglar�n� yazd�r
        foreach (var npcEntry in npcDialogues)
        {
            string npcName = npcEntry.Key;
            List<string> dialogues = npcEntry.Value;

            journalText.text += npcName + ":\n"; // NPC ad�n� ba�l�k olarak ekle

            // Her bir diyalog sat�r�n� alt sat�ra ekle
            foreach (string dialogue in dialogues)
            {
                journalText.text += "  - " + dialogue + "\n";
            }

            journalText.text += "\n"; // Her NPC i�in araya bo�luk b�rak
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

