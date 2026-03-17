using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image heartUIDisplay;
    public Sprite[] heartSprites;
    public GameObject startOfLevel;
    public Transform lastCheckpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void takeDamage()
    {
        health--;
        if (health < 0)
        {
            //spawn at start
            transform.position = startOfLevel.transform.position;
            lastCheckpoint = startOfLevel.transform;
            health = maxHealth;
        }
        else
        {
            //spawn at last checkpoint
            transform.position = lastCheckpoint.transform.position;
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
    }
}
