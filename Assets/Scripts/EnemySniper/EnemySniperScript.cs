using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniperScript : MonoBehaviour
{
    public float aggroDistance;
    public Transform playerPosition;
    public Rigidbody2D rb;

    private bool stunned;

    public float activationTime;

    public float timeToTarget;
    private bool targeted;

    public EnemySniperGunScript gun;

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
        if (playerPosition != null && !stunned)
        {
            float distance = Vector2.Distance(transform.position, playerPosition.position);
            if (distance <= aggroDistance)
            {
                Vector2 aimDirection = playerPosition.position - transform.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = aimAngle;

                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;

                int layerMask = 1 << 9;
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, aggroDistance, ~layerMask);
                if (hitInfo.collider != null)
                {
                    Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        if (!targeted)
                        {
                            StartCoroutine(TargetPlayer(timeToTarget));
                        }
                        else
                        {
                            gun.Fire();
                        }
                    }
                    else
                    {
                        targeted = false;
                    }
                }
                else
                {
                    Debug.DrawLine(transform.position, transform.position + transform.up * aggroDistance, Color.green);
                    targeted = false;
                }
            }
        }
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

    public IEnumerator TargetPlayer(float targetTime)
    {
        targeted = false;
        yield return new WaitForSeconds(targetTime);
        targeted = true;
    }
}
