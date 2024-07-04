using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniperBulletScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float lifespan;

    void Start()
    {
        StartCoroutine(StartLifespan());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Player":
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
