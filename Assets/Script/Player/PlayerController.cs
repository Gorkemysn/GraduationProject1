using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    public Transform cameraTransform; // Kameray� referans olarak ekledik
    public float rollDistance = 3f; // Takla mesafesi
    public float rollCooldown = 1f; // Takla bekleme s�resi

    private Rigidbody rb;
    private bool isGrounded;
    private bool canRoll = true; // Takla atabilme durumu

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Hareket Giri�leri
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // E�er hareket varsa animasyonlar� kontrol et
        bool isWalking = direction.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift);
        bool isRunning = direction.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);

        // Kamera bazl� hareket y�n� ayar�
        if (direction.magnitude >= 0.1f)
        {
            // Kameran�n ileri ve sa� y�nlerini hesapla
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Y eksenini s�f�rla
            forward.y = 0f;
            right.y = 0f;

            // Normalle�tir ki hareket d�zg�n �al��s�n
            forward.Normalize();
            right.Normalize();

            // Kamera y�nl� hareket vekt�r�n� olu�tur
            Vector3 moveDirection = forward * vertical + right * horizontal;

            // Karakterin rotasyonunu hareket y�n�ne g�re ayarla
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Hareket h�z�
            float speed = isRunning ? runSpeed : walkSpeed;
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }

        // Z�plama Kontrol�
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
        }

        // Takla Kontrol�
        if (Input.GetKeyDown(KeyCode.C) && canRoll)
        {
            StartCoroutine(PerformRoll());
        }
    }

    private IEnumerator PerformRoll()
    {
        canRoll = false;

        // Takla animasyonunu tetikle
        animator.SetTrigger("Roll");

        // Takla s�ras�nda karakteri ileri hareket ettir
        Vector3 rollDirection = transform.forward * rollDistance;
        float rollTime = 0.3f; // Takla s�resi (animasyona ba�l� olarak ayarlanabilir)
        float elapsedTime = 0f;

        while (elapsedTime < rollTime)
        {
            transform.Translate(rollDirection * (Time.deltaTime / rollTime), Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Takla bekleme s�resi
        yield return new WaitForSeconds(rollCooldown);

        canRoll = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Karakter yere indi�inde isGrounded true olur
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
