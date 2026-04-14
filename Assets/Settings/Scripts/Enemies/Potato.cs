using UnityEngine;

public class Potato : MonoBehaviour
{
    //other objects and prefabs
    public GameObject player;
    public GameObject spikes;

    //sprites
    public Sprite[] spriteStates;
    private SpriteRenderer spriteRenderer;

    //patroll
    public float speed;
    public float patrolRange = 2f;
    private float posOnPatrolRange = 0f;
    private Vector3 patrolDirection = Vector2.right;


    //agrro   
    private float distance;
    private Vector2 direction;
    private bool attacking = false;
    public float aggroDistance;
    public float attackDistance = 2f;
    public float attackDuration = 4f;
    public float attackCooldown = 1f;


    //death
    public float deathDelay = 0.1f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spikes.SetActive(attacking);
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)  //pause updating if attacking
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < attackDistance)    //want to attack
            {
                Invoke(nameof(attack), attackCooldown);
                attacking = true;
            }
            else if (distance < aggroDistance)   //want to be aggro
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);//move enemy
            }
            else  //default  patrol
            {
                if (posOnPatrolRange > patrolRange || posOnPatrolRange < 0)  //if above or below range
                {
                    patrolDirection.x *= -1;  //flip direvction
                }
                transform.position += patrolDirection * speed * Time.deltaTime;  //move
                posOnPatrolRange += patrolDirection.x * speed * Time.deltaTime;  //keep track of relative to range pos

            }
        }
    }

    private void attack()
    {
        attacking = true;
        spikes.SetActive(attacking);
        spriteRenderer.sprite = spriteStates[1];
        Invoke(nameof(endAttack), attackDuration);
    }
    private void endAttack()
    {
        attacking = false;
        spriteRenderer.sprite = spriteStates[0];
        spikes.SetActive(attacking);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Projectile")
        {
            Invoke(nameof(killMeNow), deathDelay);
        }
        else if (other.gameObject.tag == "PlayerFeet")
        {
            if (other.transform.position.y > transform.position.y)
            {
                Invoke(nameof(killMeNow), deathDelay);
            }
        }
        else if (other.gameObject.tag == "DeathZone")
        {
            Invoke(nameof(killMeNow), deathDelay);
        }
    }



    private void killMeNow()
    {
        Destroy(gameObject);
    }
}
