using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image heartUIDisplay;
    public Sprite[] heartSprites;
    public GameObject startOfLevel;
    public Transform lastCheckpoint;
    private BoxCollider2D playerHitBox;
    private Rigidbody2D playerRigidbody;
    [SerializeField] private float invulnerabilityDuration = 0.25f;
    private bool isInvulnerable = false;


    private void Awake()
    {
        playerHitBox = GetComponent<BoxCollider2D>();
        health = maxHealth;
        changeHeartSprite();
        playerRigidbody = GetComponent<Rigidbody2D>();
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else
        {
            //spawn at last checkpoint
            playerRigidbody.position = lastCheckpoint.transform.position;
            playerRigidbody.linearVelocity = Vector2.zero;
        }
        changeHeartSprite();
    }

    private void changeHeartSprite()
    {
        heartUIDisplay.sprite = heartSprites[health];
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
        if (other.gameObject.tag == "DeathZone")
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

