using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject scoreCounter;
    public GameObject player;

    void Start()
    {
        scoreCounter = GameObject.Find("ScoreCounter");
        player = GameObject.Find("Player");
        if (PlayerPrefs.GetInt("Loop") == 1)
        {
            PlayerPrefs.SetInt("Loop", 0);
            scoreCounter.GetComponent<ScoreCounterScript>().score = PlayerPrefs.GetFloat("Score");
            player.GetComponent<PlayerController>().gunId = PlayerPrefs.GetFloat("GunId");
            player.GetComponent<PlayerController>().gun1 = PlayerPrefs.GetFloat("Gun1");
            player.GetComponent<PlayerController>().gun2 = PlayerPrefs.GetFloat("Gun2");
            player.GetComponent<PlayerController>().health = PlayerPrefs.GetFloat("Health");
        }
        else
        {
            scoreCounter.GetComponent<ScoreCounterScript>().score = 0;
            player.GetComponent<PlayerController>().gunId = 0;
            player.GetComponent<PlayerController>().health = player.GetComponent<PlayerController>().maxHealth;
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                GameObject player = other.gameObject;
                PlayerPrefs.SetInt("Loop", 1);
                PlayerPrefs.SetFloat("Score", scoreCounter.GetComponent<ScoreCounterScript>().score);
                PlayerPrefs.SetFloat("GunId", player.GetComponent<PlayerController>().gunId);
                PlayerPrefs.SetFloat("Gun1", player.GetComponent<PlayerController>().gun1);
                PlayerPrefs.SetFloat("Gun2", player.GetComponent<PlayerController>().gun2);
                PlayerPrefs.SetFloat("Health", player.GetComponent<PlayerController>().health);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }
}
