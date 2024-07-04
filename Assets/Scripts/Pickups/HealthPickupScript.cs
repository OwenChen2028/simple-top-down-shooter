using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    public float healthRestored;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                SoundManagerScript.PlaySound("powerup");
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                player.health = Mathf.Min(player.maxHealth, player.health + healthRestored);
                Destroy(gameObject);
                break;
        }
    }
}
