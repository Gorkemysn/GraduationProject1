using System.Collections;
using UnityEngine;
using TMPro;

public class MenuAnimation : MonoBehaviour
{
    public TextMeshProUGUI firstNameText;     // �lk isim Text (�r. "My")
    public TextMeshProUGUI droppingPartText;  // D��ecek k�s�m Text (�r. "Game")
    public TextMeshProUGUI secondNameText;    // �kinci isim Text (�r. "Reborn")
    public Transform secondNameStartPos;      // �kinci ismin ba�lang�� noktas�
    public Transform secondNameEndPos;        // �kinci ismin biti� noktas�
    public GameObject startButton;            // Start Butonu
    public GameObject exitButton;             // Exit Butonu

    private float dropDuration = 1.0f;        // D��me animasyonu s�resi
    private float moveDuration = 2.0f;        // �sim hareket s�resi

    void Start()
    {
        // Ba�lang�� ayarlar�
        droppingPartText.gameObject.SetActive(true);
        firstNameText.gameObject.SetActive(true);
        secondNameText.gameObject.SetActive(false);
        startButton.SetActive(false);
        exitButton.SetActive(false);

        // Animasyon s�ras�n� ba�lat
        StartCoroutine(PlayMenuAnimation());
    }

    IEnumerator PlayMenuAnimation()
    {
        // Biraz bekle
        yield return new WaitForSeconds(2f);

        // D��me animasyonu ba�lat
        yield return StartCoroutine(DropPart());

        // �kinci isim perspektif hareketi
        secondNameText.gameObject.SetActive(true);
        yield return StartCoroutine(MoveAndScaleSecondName());

        // Men� butonlar�n� g�ster
        yield return new WaitForSeconds(1f);
        startButton.SetActive(true);
        exitButton.SetActive(true);
    }

    IEnumerator DropPart()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = droppingPartText.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, -600f, 0); // A�a��ya do�ru hareket

        while (elapsedTime < dropDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dropDuration;

            // Konumu g�ncelle
            droppingPartText.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }
    }

    IEnumerator MoveAndScaleSecondName()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = secondNameStartPos.position;
        Vector3 endPosition = secondNameEndPos.position;
        Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f); // K���k ba�la
        Vector3 endScale = new Vector3(1f, 1f, 1f);         // Normal boyut

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Konum ve �l�e�i Lerp ile g�ncelle
            secondNameText.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            secondNameText.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }
    }
}
