using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float baseSpeed;
    public float moveSpeed;
    public float speedMultiplier;
    public Rigidbody2D rb;

    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun;

    public float gunId;
    public GameObject gun;

    public float gun1;
    public float gun2;

    public float swapCooldown;
    private bool swapOnCooldown;

    public float maxHealth;
    public float health;
    public float invincibilityTime;

    public PauseScript pause;

    private bool invincible;

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    private bool dashing;

    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private bool dashOnCooldown;
    private Vector2 dashDirection;

    public TrailRenderer dashTrail;

    public GameObject DamageFlashEffect;

    // Start is called before the first frame update
    void Start()
    {
        dashDirection = new Vector2(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.paused) {
            ProcessInputs();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (gun2 == -1 || swapOnCooldown)
            {
                SoundManagerScript.PlaySound("error");
            }
            else
            {
                StartCoroutine(StartSwapCooldown());
                if (gunId == gun1)
                {
                    gunId = gun2;
                }
                else if (gunId == gun2)
                {
                    gunId = gun1;
                }
                else
                {
                    gunId = gun1;
                }
            }
        }

        if (gunId == 0)
        {
            if (gun != pistol)
            {
                gun.GetComponent<PlayerGunScript>().firing = false;
                gun.GetComponent<PlayerGunScript>().StopAllCoroutines();
                gun.GetComponent<PlayerGunScript>().onCooldown = false;
            }
            gun = pistol;
            pistol.SetActive(true);
            rifle.SetActive(false);
            shotgun.SetActive(false);
        }
        else if (gunId == 1)
        {
            if (gun != rifle)
            {
                gun.GetComponent<PlayerGunScript>().firing = false;
                gun.GetComponent<PlayerGunScript>().StopAllCoroutines();
                gun.GetComponent<PlayerGunScript>().onCooldown = false;
            }
            gun = rifle;
            pistol.SetActive(false);
            rifle.SetActive(true);
            shotgun.SetActive(false);
        }
        else if (gunId == 2)
        {
            if (gun != shotgun)
            {
                gun.GetComponent<PlayerGunScript>().firing = false;
                gun.GetComponent<PlayerGunScript>().StopAllCoroutines();
                gun.GetComponent<PlayerGunScript>().onCooldown = false;
            }
            gun = shotgun;
            pistol.SetActive(false);
            rifle.SetActive(false);
            shotgun.SetActive(true);
        }

        if (gun.GetComponent<PlayerGunScript>().firing)
        {
            gun.GetComponent<PlayerGunScript>().Fire();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1"))
        {
            gun.GetComponent<PlayerGunScript>().firing = true;
        }
        else
        {
            gun.GetComponent<PlayerGunScript>().firing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!dashOnCooldown)
            {
                if (!dashing)
                {
                    SoundManagerScript.PlaySound("dash");
                }
                dashing = true;
            }
            else
            {
                SoundManagerScript.PlaySound("error");
            }
        }
    }

    void Move()
    {
        if (!dashing)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            if (moveDirection != Vector2.zero)
            {
                dashDirection = moveDirection;
            }
        }
        else if (!dashOnCooldown)
        {
            StartCoroutine(StartDash());
        }

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                if (!invincible)
                {
                    SoundManagerScript.PlaySound("hurt");
                    health = health - other.gameObject.GetComponent<EnemyDamageScript>().damage;
                    StartCoroutine(StartInvincibility());
                }
                if (health <= 0)
                {
                    DamageFlashEffect.SetActive(false);
                    Destroy(gameObject);
                    //Time.timeScale = 0f;
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "EnemyBullet":
                if (!invincible)
                {
                    SoundManagerScript.PlaySound("hurt");
                    health = health - other.gameObject.GetComponent<EnemyDamageScript>().damage;
                    StartCoroutine(StartInvincibility());
                }
                if (health <= 0)
                {
                    DamageFlashEffect.SetActive(false);
                    Destroy(gameObject);
                    //Time.timeScale = 0f;
                }
                break;
        }
    }

    public IEnumerator StartInvincibility()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        invincible = true;
        //DamageFlashEffect.SetActive(true);
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
        //DamageFlashEffect.SetActive(false);
    }

    public IEnumerator StartDash()
    {
        rb.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        dashing = false;
        dashTrail.emitting = false;
        StartCoroutine(StartDashCooldown());
    }

    public IEnumerator StartDashCooldown()
    {
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }

    public IEnumerator StartSwapCooldown()
    {
        swapOnCooldown = true;
        yield return new WaitForSeconds(swapCooldown);
        swapOnCooldown = false;
    }
}
