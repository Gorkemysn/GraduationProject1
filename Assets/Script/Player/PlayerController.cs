using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    public Transform cameraTransform; // Kamerayý referans olarak ekledik
    public float rollDistance = 3f; // Takla mesafesi
    public float rollCooldown = 1f; // Takla bekleme süresi

    private Rigidbody rb;
    private bool isGrounded;
    private bool canRoll = true; // Takla atabilme durumu

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Hareket Giriþleri
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Eðer hareket varsa animasyonlarý kontrol et
        bool isWalking = direction.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift);
        bool isRunning = direction.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);

        // Kamera bazlý hareket yönü ayarý
        if (direction.magnitude >= 0.1f)
        {
            // Kameranýn ileri ve sað yönlerini hesapla
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Y eksenini sýfýrla
            forward.y = 0f;
            right.y = 0f;

            // Normalleþtir ki hareket düzgün çalýþsýn
            forward.Normalize();
            right.Normalize();

            // Kamera yönlü hareket vektörünü oluþtur
            Vector3 moveDirection = forward * vertical + right * horizontal;

            // Karakterin rotasyonunu hareket yönüne göre ayarla
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Hareket hýzý
            float speed = isRunning ? runSpeed : walkSpeed;
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }

        // Zýplama Kontrolü
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            isGrounded = false;
        }

        // Takla Kontrolü
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

        // Takla sýrasýnda karakteri ileri hareket ettir
        Vector3 rollDirection = transform.forward * rollDistance;
        float rollTime = 0.3f; // Takla süresi (animasyona baðlý olarak ayarlanabilir)
        float elapsedTime = 0f;

        while (elapsedTime < rollTime)
        {
            transform.Translate(rollDirection * (Time.deltaTime / rollTime), Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Takla bekleme süresi
        yield return new WaitForSeconds(rollCooldown);

        canRoll = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Karakter yere indiðinde isGrounded true olur
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
