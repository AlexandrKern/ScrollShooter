using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoints : MonoBehaviour
{
    public int points = 10;
    private PlayerScore playerScore;
    private Health health;
    void Start()
    {
        health = GetComponent<Health>();
        GameObject player = GameObject.FindGameObjectWithTag("Score");
        if (player != null)
        {
            playerScore = player.GetComponent<PlayerScore>();
        }
    }

    void Update()
    {
        if (health.isDeath)
        {
            playerScore.AddScore(points);
        }
    }
}
