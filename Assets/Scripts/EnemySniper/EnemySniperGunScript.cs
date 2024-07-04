using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniperGunScript : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform firePoint;
    public float fireForce;
    public float spread;
    public float cooldownDuration;

    private bool onCooldown;

    public void Fire()
    {
        if (onCooldown == false)
        {
            float randomAngle = Random.Range(-spread, spread);
            GameObject projectile = Instantiate(enemyBullet, firePoint.position, Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + randomAngle));
            Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
            rbProjectile.AddForce((Quaternion.Euler(0, 0, firePoint.rotation.z + randomAngle) * firePoint.up) * fireForce, ForceMode2D.Impulse);

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
