using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserScript : MonoBehaviour
{
    public float aggroDistance;
    public Transform playerPosition;
    public Rigidbody2D rb;

    public float moveSpeed;

    public float activationTime;

    private bool stunned;

    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
    }

    void OnEnable()
    {
        StartCoroutine(StartStun(activationTime));
    }

    void Update()
    {
        if (playerPosition != null)
        {
            float distance = Vector2.Distance(transform.position, playerPosition.position);
            if (distance <= aggroDistance && !stunned)
            {
                Vector2 aimDirection = playerPosition.position - transform.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = aimAngle;

                rb.velocity = aimDirection.normalized * moveSpeed;
                rb.angularVelocity = 0f;
            }
        }
    }

    void FixedUpdate()
    {

    }

    public IEnumerator StartStun(float stunDuration)
    {
        if (stunDuration > 0)
        {
            stunned = true;
            yield return new WaitForSeconds(stunDuration);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            stunned = false;
        }
    }
}
