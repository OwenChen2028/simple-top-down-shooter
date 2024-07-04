using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float health;

    public GameObject scoreCounter;
    public float pointBounty;

    void Start()
    {
        scoreCounter = GameObject.Find("ScoreCounter");
    }

    void Update()
    {
        if (health <= 0)
        {
            scoreCounter.GetComponent<ScoreCounterScript>().score += pointBounty;
            Destroy(gameObject);
        }
    }
}
