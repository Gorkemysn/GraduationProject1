using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.MainActions input;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    [Header("Controller")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;
    public float sprintSpeed = 8f; // Koþma hýzý
    private bool isSprinting = false; // Koþma durumu
    public float dodgeDistance = 3f; // Dodge mesafesi
    public float dodgeDuration = 0.2f; // Dodge süresi
    private bool isDodging = false; // Dodge durumu

    Vector3 _PlayerVelocity;

    bool isGrounded;

    [Header("Stamina System")]
    public float maxStamina = 100f; // Maksimum stamina
    private float currentStamina; // Mevcut stamina
    public float staminaRegenRate = 5f; // Stamina yenilenme hýzý
    public float staminaUsageRateRun = 10f; // Koþarken harcanan stamina
    public float staminaUsageDodge = 20f; // Dodge sýrasýnda harcanan stamina
    public StaminaBar staminaBar; // UI bar referansý

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentStamina = maxStamina;

        if (staminaBar != null)
            staminaBar.SetMaxStamina(maxStamina); // Stamina barýný baþlat
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Repeat Inputs
        if (input.Attack.IsPressed())
        { Attack(); }

        SetAnimations();

        // Stamina yenilenmesi
        if (!isSprinting && !isDodging && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (staminaBar != null)
                staminaBar.SetStamina(currentStamina);
        }

        // Koþarken stamina tüketimi
        if (isSprinting && currentStamina > 0)
        {
            currentStamina -= staminaUsageRateRun * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (staminaBar != null)
                staminaBar.SetStamina(currentStamina);

            if (currentStamina <= 0)
            {
                StopSprint();
            }
        }
    }

    void FixedUpdate()
    { MoveInput(input.Movement.ReadValue<Vector2>()); }

    void LateUpdate()
    { LookInput(input.Look.ReadValue<Vector2>()); }

    void MoveInput(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);

        _PlayerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && _PlayerVelocity.y < 0)
            _PlayerVelocity.y = -2f;
        controller.Move(_PlayerVelocity * Time.deltaTime);
    }

    void LookInput(Vector3 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * sensitivity));
    }

    void OnEnable()
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

    void Jump()
    {
        // Adds force to the player rigidbody to jump
        if (isGrounded)
            _PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    void StartSprint()
    {
        if (currentStamina > 0)
        {
            isSprinting = true;
        }
    }

    void StopSprint()
    {
        isSprinting = false;
    }

    void Dodge()
    {
        if (isDodging || currentStamina < staminaUsageDodge) return;

        isDodging = true;
        currentStamina -= staminaUsageDodge;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (staminaBar != null)
            staminaBar.SetStamina(currentStamina);

        Vector3 dodgeDirection = -transform.forward; // Geriye doðru
        StartCoroutine(DodgeRoutine(dodgeDirection));
    }

    IEnumerator DodgeRoutine(Vector3 direction)
    {
        float elapsedTime = 0;
        while (elapsedTime < dodgeDuration)
        {
            controller.Move(direction * (dodgeDistance / dodgeDuration) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDodging = false;
    }

    void AssignInputs()
    {
        input.Jump.performed += ctx => Jump();
        input.Attack.started += ctx => Attack();
        input.Sprint.started += ctx => StartSprint();
        input.Sprint.canceled += ctx => StopSprint();
        input.Dodge.performed += ctx => Dodge();
    }

    // ---------- //
    // ANIMATIONS //
    // ---------- //

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        // If player is not attacking
        if (!attacking)
        {
            if (_PlayerVelocity.x == 0 && _PlayerVelocity.z == 0)
            { ChangeAnimationState(IDLE); }
            else
            { ChangeAnimationState(WALK); }
        }
    }

    // ------------------- //
    // ATTACKING BEHAVIOUR //
    // ------------------- //

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);

            if (hit.transform.TryGetComponent<Actor>(out Actor T))
            { T.TakeDamage(attackDamage); }
        }
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }
}
