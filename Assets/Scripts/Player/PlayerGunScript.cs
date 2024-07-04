using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    public GameObject playerBullet;
    public Transform firePoint;
    public float fireForce;
    public float cooldownDuration;
    public float shots;
    public float spread;
    public bool firing;
    public bool onCooldown;

    void Start()
    {
        PlayerBulletScript playerBulletScript = playerBullet.GetComponent<PlayerBulletScript>();
        playerBulletScript.lifespan = playerBulletScript.initialLifespan;
        playerBulletScript.damage = playerBulletScript.initialDamage;
    }

    public void Fire()
    {
        if (onCooldown == false)
        {
            SoundManagerScript.PlaySound("shoot");
            for (int i = 0; i < shots; i++)
            {
                float randomAngle = Random.Range(-spread, spread);
                GameObject projectile = Instantiate(playerBullet, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + randomAngle));
                Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
                rbProjectile.AddForce((Quaternion.Euler(0, 0, firePoint.rotation.z + randomAngle) * firePoint.up) * fireForce, ForceMode2D.Impulse);
            }
            StartCoroutine(StartCooldown());
        }
    }

    public IEnumerator StartCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        onCooldown = false;
    }
}
