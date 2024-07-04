using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    private static SoundManagerScript instance;

    public static AudioClip playerShootSound, playerDashSound, playerHurtSound, enemyHitSound, enemyDeathSound, powerUpSound, blipSound, errorSound;
    static AudioSource audioSrc;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerShootSound = Resources.Load<AudioClip>("laserShoot");
        playerDashSound = Resources.Load<AudioClip>("dash");
        playerHurtSound = Resources.Load<AudioClip>("hurt");
        enemyHitSound = Resources.Load<AudioClip>("hit");
        enemyDeathSound = Resources.Load<AudioClip>("explosion");
        powerUpSound = Resources.Load<AudioClip>("powerUp");
        blipSound = Resources.Load<AudioClip>("blipSelect");
        errorSound = Resources.Load<AudioClip>("synth");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "shoot":
                audioSrc.PlayOneShot(playerShootSound, 0.3f);
                break;
            case "dash":
                audioSrc.PlayOneShot(playerDashSound, 0.7f);
                break;
            case "hurt":
                audioSrc.PlayOneShot(playerHurtSound, 1.0f);
                break;
            case "hit":
                audioSrc.PlayOneShot(enemyHitSound, 0.5f);
                break;
            case "kill":
                audioSrc.PlayOneShot(enemyDeathSound, 0.5f);
                break;
            case "powerup":
                audioSrc.PlayOneShot(powerUpSound, 1.0f);
                break;
            case "select":
                audioSrc.PlayOneShot(blipSound, 0.9f);
                break;
            case "error":
                audioSrc.PlayOneShot(errorSound, 0.8f);
                break;
        }
    }
}
