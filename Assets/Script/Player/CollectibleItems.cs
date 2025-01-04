using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems : MonoBehaviour
{
    public int goldValue = 5; // Bu altýn nesnesinin deðeri

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu ile temas kontrolü
        {
            GoldManager.instance.AddGold(goldValue); // Altýn ekle
            Destroy(gameObject); // Altýný yok et
        }
    }
}
