using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupScript : MonoBehaviour
{
    public float damageBoost;

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                SoundManagerScript.PlaySound("powerup");
                float newGun = Random.Range(0, 3);
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                while (newGun == player.gun1 || newGun == player.gun2)
                {
                    newGun = Random.Range(0, 3);
                }
                if (player.gun2 == -1)
                {
                    player.gun2 = newGun;
                }
                else if (player.gunId == player.gun1)
                {
                    player.gun1 = newGun;
                }
                else if (player.gunId == player.gun2)
                {
                    player.gun2 = newGun;
                }
                else
                {
                    player.gun1 = newGun;
                }
                player.gunId = newGun;

                Destroy(gameObject);
                break;
        }
    }
}
