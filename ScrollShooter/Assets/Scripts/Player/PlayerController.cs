using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject deathEffectPrefab;

    // Параметры скорости
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 8f;
    public float rollDuration = 0.5f;

    // Проверка на земле
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    // Части компонентов
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    // Параметры состояния
    private bool isRolling;
    private float rollTime;

    // Параметры стрельбы
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public GameObject impactEffectPrefab;// Префаб эффекта столкновения
    private Health health;
    void Start()
    {
        // Получение компонентов
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        Death();
    }

    private void HandleMovement()
    {
        if (!health.isDeath)
        {
            // Проверка на землю
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

            // Получение ввода
            float moveInput = Input.GetAxis("Horizontal");

            if (!isRolling)
            {
                // Перемещение персонажа
                rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

                // Поворот спрайта персонажа и оружия в сторону движения
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePosition.x > transform.position.x)
                {
                    sr.flipX = false;
                    firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);
                    firePoint.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    sr.flipX = true;
                    firePoint.localPosition = new Vector3(-Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);
                    firePoint.localRotation = Quaternion.Euler(0, 180, 0);
                }

                // Прыжок
                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                // Начало кувырка
                if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isRolling = true;
                    rollTime = rollDuration;
                    anim.SetBool("isRolling", true);
                    rb.velocity = new Vector2(moveInput * rollSpeed, rb.velocity.y); // Кувырок в ту сторону, куда нажата кнопка
                }
            }
            else
            {
                // Обновление времени кувырка
                rollTime -= Time.deltaTime;
                if (rollTime <= 0)
                {
                    isRolling = false;
                    anim.SetBool("isRolling", false);
                }
            }

            // Обновление параметров анимации
            anim.SetBool("isJumping", !isGrounded);
            anim.SetBool("isRunning", moveInput != 0 && isGrounded);
            anim.SetBool("IsEndJumping", isGrounded);
        }
       
    }

    private void HandleShooting()
    {
        // Стрельба
        if (Input.GetMouseButtonDown(0) && !PauseController.isPause)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Запуск анимации стрельбы
        anim.SetTrigger("isShooting");

        // Создание пули
        AudioManager.instance.PlayEffect("GunShot");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;

        // Привязка эффекта столкновения к пуле
        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        bulletScript.SetImpactEffect(impactEffectPrefab);
    }

    // Отрисовка круга проверки на землю в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

    }

    private void Death()
    {
        if (health.isDeath)
        {
            StartCoroutine(DeathTime());
        }
    }
}
