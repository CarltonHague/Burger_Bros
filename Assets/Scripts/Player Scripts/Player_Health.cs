using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class Player_Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image heartUIDisplay;
    public Sprite[] heartSprites;

    public Image deathScreenDisplay;
    public Sprite deathScreenSprite;
    public float deathScreenTimer = 3.5f;

    public GameObject startOfLevel;
    public Transform lastCheckpoint;
    private BoxCollider2D playerHitBox;
    private Rigidbody2D playerRigidbody;
    [SerializeField] private float invulnerabilityDuration = 0.25f;
    private bool isInvulnerable = false;
    [SerializeField] MovingDeathZone movingDeathZone;
    private Player_Shoot ammo;
    private Player_Coins coins;


    //sound
    public AudioClip SFX;
    public float volume = 0.9f;
    private AudioSource music;
    private bool isPlaying = false;

    private void Awake()
    {
        playerHitBox = GetComponent<BoxCollider2D>();
        health = maxHealth;
        changeHeartSprite();
        playerRigidbody = GetComponent<Rigidbody2D>();
        ammo = GetComponent<Player_Shoot>();
        coins = GetComponent<Player_Coins>();
        music = GetComponent<AudioSource>();
    }

    void playSound()
    {
        if (!isPlaying)
        {
            AudioSource.PlayClipAtPoint(SFX, transform.position, volume);
            isPlaying = true;
        }
    }

    //private void resetPlayer()
    //{
    //    health = maxHealth;
    //    changeHeartSprite();
    //    coins.resetCoins();
    //    ammo.resetAmmo();
    //    playerRigidbody.position = startOfLevel.transform.position;
    //    playerRigidbody.linearVelocity = Vector2.zero;
    //    if (movingDeathZone != null)
    //    {
    //        movingDeathZone.resetToCheckpoint(startOfLevel.transform);
    //    }
    //}

    private void deathScreen()
    {
        isInvulnerable = true;
        playSound();
        music.mute = true;
        deathScreenDisplay.enabled = true;
        deathScreenDisplay.sprite = deathScreenSprite;
        Invoke(nameof(respawnAtStart), deathScreenTimer);
    }
    private void respawnAtStart()
    {
        deathScreenDisplay.enabled = false;
        isInvulnerable = false;
        isPlaying = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void takeDamage()
    {
        if (isInvulnerable)
        {
            return;
        }
        StartCoroutine(InvulnerabilityCoroutine());
        health--;
        if (health <= 0)
        {
            //spawn at start
            deathScreen();
            

        }
        else
        {
            //spawn at last checkpoint
            playerRigidbody.position = lastCheckpoint.transform.position;
            playerRigidbody.linearVelocity = Vector2.zero;
            if (movingDeathZone != null)
            {
                movingDeathZone.resetToCheckpoint(lastCheckpoint);
            }
        }
        changeHeartSprite();
    }

    private void changeHeartSprite()
    {
        if ( health <= maxHealth && health >= 0)
        {
            heartUIDisplay.sprite = heartSprites[health];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy Projectile")
        {
            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < 2f)
            {
                takeDamage();
            }
        }
        else if (other.gameObject.tag == "CheckPoint")
        {
            lastCheckpoint = other.transform;
        }
        if (other.CompareTag("DeathZone") && !isInvulnerable)
        {
           if(playerHitBox.IsTouching(other))
            {
                takeDamage();
            }
        }





    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone") && !isInvulnerable)
        {
            if(playerHitBox.IsTouching(other))
            {
                takeDamage();
            }
        }
    }



    private IEnumerator InvulnerabilityCoroutine()
    {
        
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

}

