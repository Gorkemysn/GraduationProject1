using System.Collections;
using UnityEngine;
using TMPro;

public class MenuAnimation : MonoBehaviour
{
    public TextMeshProUGUI firstNameText;     // Ýlk isim Text (ör. "My")
    public TextMeshProUGUI droppingPartText;  // Düþecek kýsým Text (ör. "Game")
    public TextMeshProUGUI secondNameText;    // Ýkinci isim Text (ör. "Reborn")
    public Transform secondNameStartPos;      // Ýkinci ismin baþlangýç noktasý
    public Transform secondNameEndPos;        // Ýkinci ismin bitiþ noktasý
    public GameObject startButton;            // Start Butonu
    public GameObject exitButton;             // Exit Butonu

    private float dropDuration = 1.0f;        // Düþme animasyonu süresi
    private float moveDuration = 2.0f;        // Ýsim hareket süresi

    void Start()
    {
        // Baþlangýç ayarlarý
        droppingPartText.gameObject.SetActive(true);
        firstNameText.gameObject.SetActive(true);
        secondNameText.gameObject.SetActive(false);
        startButton.SetActive(false);
        exitButton.SetActive(false);

        // Animasyon sýrasýný baþlat
        StartCoroutine(PlayMenuAnimation());
    }

    IEnumerator PlayMenuAnimation()
    {
        // Biraz bekle
        yield return new WaitForSeconds(2f);

        // Düþme animasyonu baþlat
        yield return StartCoroutine(DropPart());

        // Ýkinci isim perspektif hareketi
        secondNameText.gameObject.SetActive(true);
        yield return StartCoroutine(MoveAndScaleSecondName());

        // Menü butonlarýný göster
        yield return new WaitForSeconds(1f);
        startButton.SetActive(true);
        exitButton.SetActive(true);
    }

    IEnumerator DropPart()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = droppingPartText.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, -600f, 0); // Aþaðýya doðru hareket

        while (elapsedTime < dropDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dropDuration;

            // Konumu güncelle
            droppingPartText.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null;
        }
    }

    IEnumerator MoveAndScaleSecondName()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = secondNameStartPos.position;
        Vector3 endPosition = secondNameEndPos.position;
        Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f); // Küçük baþla
        Vector3 endScale = new Vector3(1f, 1f, 1f);         // Normal boyut

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Konum ve ölçeði Lerp ile güncelle
            secondNameText.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            secondNameText.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }
    }
}
