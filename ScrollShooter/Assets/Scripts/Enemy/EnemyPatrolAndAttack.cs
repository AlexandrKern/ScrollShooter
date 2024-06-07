using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrolAndAttack : EnemyBase
{
    public Transform[] patrolPoints;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject deathEffectPrefab;

    private int currentPointIndex = 0;
    private float nextFireTime = 0f;
    private Animator animator;
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    protected override void Patrol()
    {
        if (!health.isDeath)
        {
            if (patrolPoints.Length == 0) return;

            Transform targetPoint = patrolPoints[currentPointIndex];
            Vector2 direction = (targetPoint.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                direction = (patrolPoints[currentPointIndex].position - transform.position).normalized;

                if ((direction.x > 0 && !isMovingRight) || (direction.x < 0 && isMovingRight))
                {
                    Flip();
                }
            }
        }
       
    }

    protected override void Attack()
    {
        if (!health.isDeath)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if ((direction.x > 0 && !isMovingRight) || (direction.x < 0 && isMovingRight))
            {
                Flip();
            }
            animator.SetBool("isWalking", false);
            animator.SetBool("IsIdle", true);
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
  
    }

    private void Shoot()
    {
        if (!health.isDeath)
        {
            AudioManager.instance.PlayEffect("EnemyLaserShot");
            Vector2 direction = (player.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            animator.SetBool("IsIdle", false);
            animator.SetTrigger("shoot");
        }
      
    }

    private IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(2);
        Instantiate(deathEffectPrefab, transform.position,Quaternion.identity);

    }

    protected override void Death()
    {
        if (health.isDeath)
        {
            StartCoroutine(DeathTime());
        }
    }
}
