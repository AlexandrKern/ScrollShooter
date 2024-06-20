using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 8f;
    public float rollDuration = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public GameObject impactEffectPrefab;
    private int currentAmmo;
    public BulletScale bulletScale;

    public GameObject deathEffectPrefab;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private bool isRolling;
    private float rollTime;
    private bool localIsDeath;
    private int maxAmmo = 100;
    private int direction;

    private Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        currentAmmo = maxAmmo;
        bulletScale.SetBullet(1f);
        localIsDeath = false;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        CheckDeath();
    }

    private void HandleMovement()
    {
        if (!health.isDeath)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

            float moveInput = Input.GetAxis("Horizontal");

            if (!isRolling)
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

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

                if (isGrounded && Input.GetKeyDown(KeyCode.Space))
                {
                    AudioManager.instance.PlayEffect("PlayerJump");
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                if (isGrounded && Input.GetMouseButtonDown(1) && moveInput != 0)
                {
                    if (moveInput<0)
                    {
                        direction = -1;
                    }
                    if (moveInput > 0)
                    {
                        direction = 1;
                    }
                    AudioManager.instance.PlayEffect("Somersault");
                    isRolling = true;
                    rollTime = rollDuration;
                    anim.SetBool("isRolling", true);
                    rb.velocity = new Vector2(direction * rollSpeed, rb.velocity.y);
                }
            }
            else
            {
                rollTime -= Time.deltaTime;
                if (rollTime <= 0)
                {
                    isRolling = false;
                    anim.SetBool("isRolling", false);
                }
            }

            anim.SetBool("isJumping", !isGrounded);
            anim.SetBool("isRunning", moveInput != 0 && isGrounded);
            anim.SetBool("IsEndJumping", isGrounded);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && !PauseController.isPause && !health.isDeath)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            anim.SetTrigger("isShooting");

            AudioManager.instance.PlayEffect("GunShot");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * bulletSpeed;

            PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
            bulletScript.SetImpactEffect(impactEffectPrefab);
            currentAmmo--;
            bulletScale.SetBullet((float)currentAmmo / maxAmmo);
        }
    }

    public void Reload(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        bulletScale.SetBullet((float)currentAmmo / maxAmmo);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void CheckDeath()
    {
        if (health.isDeath && !localIsDeath)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            localIsDeath = true;
        }
    }
}
