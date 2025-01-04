using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems : MonoBehaviour
{
    public int goldValue = 5; // Bu alt�n nesnesinin de�eri

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu ile temas kontrol�
        {
            GoldManager.instance.AddGold(goldValue); // Alt�n ekle
            Destroy(gameObject); // Alt�n� yok et
        }
    }
}
