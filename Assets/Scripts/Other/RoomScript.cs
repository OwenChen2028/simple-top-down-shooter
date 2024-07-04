using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{

    public GameObject enemies;
    public GameObject doors;
    public GameObject rewards;

    private bool entered = false;
    private bool cleared = false;

    public ScoreCounterScript scoreCounter;
    public float pointBounty;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        enemies.SetActive(false);
        doors.SetActive(false);
        rewards.SetActive(false);

        scoreCounter = GameObject.Find("ScoreCounter").GetComponent<ScoreCounterScript>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        player.moveSpeed = player.baseSpeed * player.speedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && !cleared && enemies.transform.childCount == 0)
        {
            cleared = true;

            clearBullets();

            enemies.SetActive(false);
            doors.SetActive(false);
            rewards.SetActive(true);

            scoreCounter.score += pointBounty;

            player.moveSpeed = player.baseSpeed * 1.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                if (!entered && !cleared)
                {
                    entered = true;

                    clearBullets();

                    enemies.SetActive(true);
                    doors.SetActive(true);

                    player.moveSpeed = player.baseSpeed;
                }
                break;
        }
    }

    void clearBullets()
    {
        GameObject[] playerBullets;
        playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        for (var i = 0; i < playerBullets.Length; i++)
        {
            Destroy(playerBullets[i]);
        }

        GameObject[] enemyBullets;
        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (var i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }
    }
}
