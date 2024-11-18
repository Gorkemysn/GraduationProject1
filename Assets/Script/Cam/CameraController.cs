using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // takip edilecek hedef (karakter)
    public float distance = 10f; // kamera ile hedef arasýndaki mesafe
    public float height = 5f; // kameranýn hedefin üstünde olacak yüksekliði
    public float smoothSpeed = 0.125f; // kamera hareketinin yumuþaklýðý

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(height, -distance, 0f); // hedefin konumunu alarak kameranýn istenen konumunu hesapla
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // yumuþak bir hareket için Lerp fonksiyonu kullan
        transform.position = smoothedPosition;

        transform.LookAt(target); // hedefe bak
    }
}
