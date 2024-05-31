using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��������� ��������
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 8f;
    public float rollTime;
    public float rollDuration = 0.5f;

    // �������� �� �����
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool isRolling;


    // ��������� ��������
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    // ����� �����������
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start()
    {
        // ��������� �����������
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �������� �� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // ��������� �����
        float moveInput = Input.GetAxis("Horizontal");
        if (!isRolling)
        {
            // ����������� ���������
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // ������� ������� ��������� � ������� ��������
            if (moveInput > 0)
            {
                sr.flipX = false;
            }
            else if (moveInput < 0)
            {
                sr.flipX = true;
            }

            // ������
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            // ������ �������
            if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRolling = true;
                rollTime = rollDuration;
                anim.SetBool("isRolling", true);
                rb.velocity = new Vector2(sr.flipX ? -rollSpeed : rollSpeed, rb.velocity.y);
            }
            // ��������
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            // ���������� ������� �������
            rollTime -= Time.deltaTime;
            if (rollTime <= 0)
            {
                isRolling = false;
                anim.SetBool("isRolling", false);
            }
        }


        // ���������� ���������� ��������
        anim.SetBool("isJumping", !isGrounded);
        anim.SetBool("isRunning", moveInput != 0 && isGrounded);
        anim.SetBool("IsEndJumping", isGrounded);
    }

    private void Shoot()
    {
        // ������ �������� ��������
        anim.SetTrigger("isShooting");

        // �������� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(sr.flipX ? -bulletSpeed : bulletSpeed, 0);
    }

    // ��������� ����� �������� �� ����� � ���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
