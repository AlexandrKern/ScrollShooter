using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Параметры скорости
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 8f;
    public float rollTime;
    public float rollDuration = 0.5f;

    // Проверка на земле
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool isRolling;


    // Параметры стрельбы
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    // Части компонентов
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        // Получение компонентов
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Проверка на землю
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Получение ввода
        float moveInput = Input.GetAxis("Horizontal");
        if (!isRolling)
        {
            // Перемещение персонажа
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x > transform.position.x|| moveInput > 0)
            {
                sr.flipX = false;
                firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x), firePoint.localPosition.y, firePoint.localPosition.z);
                firePoint.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if(!(mousePosition.x > transform.position.x) || moveInput < 0)
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
            if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift)&& moveInput != 0)
            {
                isRolling = true;
                rollTime = rollDuration;
                anim.SetBool("isRolling", true);
                rb.velocity = new Vector2(sr.flipX ? -rollSpeed : rollSpeed, rb.velocity.y);
            }
            // Стрельба
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
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

    private void Shoot()
    {
        // Запуск анимации стрельбы
        anim.SetTrigger("isShooting");

        // Создание пули
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }

    // Отрисовка круга проверки на землю в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
