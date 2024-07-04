using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform position;
    public float initialLifespan;
    public float initialDamage;
    public float lifespan;
    public float damage;

    public GameObject hitEffect;
    public GameObject deathEffect;

    public bool enemyHit;

    void Start()
    {
        StartCoroutine(StartLifespan());

        enemyHit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                EnemyHealthScript enemy = other.GetComponent<EnemyHealthScript>();
                if (enemyHit || enemy.health <= 0)
                {
                    break;
                }
                enemyHit = true;
                enemy.health = enemy.health - damage;
                if (enemy.health <= 0)
                {
                    SoundManagerScript.PlaySound("kill");
                    Instantiate(deathEffect, transform.position, transform.rotation);
                }
                else
                {
                    SoundManagerScript.PlaySound("hit");
                    Instantiate(hitEffect, transform.position, transform.rotation);
                }
                Destroy(gameObject);
                break;
        }
    }

    public IEnumerator StartLifespan()
    {
        yield return new WaitForSeconds(lifespan);

        Destroy(gameObject);
    }
}
