using UnityEngine;

public class Potato : MonoBehaviour
{
    //other objects and prefabs
    public GameObject player;
    public GameObject spikes;

    //sprites
    public Sprite[] spriteStates;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

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
                animator.SetBool("Walking", false);
                Invoke(nameof(attack), attackCooldown);
                attacking = true;
            }
            else if (distance < aggroDistance)   //want to be aggro
            {
                animator.SetBool("Walking", true);
                Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
                if (direction.x > 0)  //flip sprite based on direction
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);//move enemy
            }
            else  //default  patrol
            {
                if (posOnPatrolRange > patrolRange || posOnPatrolRange < 0)  //if above or below range
                {
                    patrolDirection.x *= -1;  //flip direvction
                }
                animator.SetBool("Walking", true);
                transform.position += patrolDirection * speed * Time.deltaTime;  //move
                posOnPatrolRange += patrolDirection.x * speed * Time.deltaTime;  //keep track of relative to range pos
                if (patrolDirection.x > 0)  //flip sprite based on direction
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }

            }
            
        }
    }

    private void attack()
    {
        animator.SetBool("Spike Up", true);
        attacking = true;
        spikes.SetActive(attacking);
        spriteRenderer.sprite = spriteStates[1];
        Invoke(nameof(endAttack), attackDuration);
    }
    private void endAttack()
    {
        animator.SetBool("Spike Up", false);
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
