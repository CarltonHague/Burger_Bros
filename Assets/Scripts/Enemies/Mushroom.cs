using UnityEngine;

public class Mushroom : MonoBehaviour
{
    //other objects and prefabs
    public GameObject player;
    public GameObject centrePoint;

    //sprites
    public Sprite[] spriteStates;
    private SpriteRenderer spriteRenderer;

    //patroll
    public float speed = 1;
    public float patrolRange = 2f;
    private float posOnPatrolRange = 0f;
    private Vector3 patrolDirection = Vector2.right;


    //agrro   
    private float distance;
    private bool blocking = false;
    private Vector2 blockDirection;
    public float aggroDistance;
    public float blockDistance = 4f;
    public float blockDuration = 4f;
    public float blockCooldown = 1f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!blocking)  //pause updating if blocking
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < blockDistance)    //want to block
            {
                if (player.transform.position.y < transform.position.y +1)  //only bother blocking if the player is not above us
                {
                    Invoke(nameof(block), blockCooldown);
                    blocking = true;
                }
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

    private void block()
    {
        blocking = true;
        if (player.transform.position.x < transform.position.x) //if player on left
        {
            //flip sprite left
            spriteRenderer.flipX = true;
            //rotate centre left
            centrePoint.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else
        {
            //flip sprite right
            spriteRenderer.flipX = false;
            //rotate centre right
            centrePoint.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        spriteRenderer.sprite = spriteStates[1];  //change to the block sprite
        Invoke(nameof(endBlock), blockDuration);
    }
    private void endBlock()
    {
        blocking = false;
        spriteRenderer.sprite = spriteStates[0]; //change sprite back
        spriteRenderer.flipX = false;
        centrePoint.transform.eulerAngles = new Vector3(0, 0, 0); //rotate centre back
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Projectile")
        {
            killMeNow();
        }
        else if (other.gameObject.tag == "PlayerFeet" && blocking)
        {
            if (other.transform.position.y > transform.position.y)
            {
                killMeNow();
            }
        }
        else if (other.gameObject.tag == "DeathZone")
        {
            killMeNow();
        }
    }



    private void killMeNow()
    {
        Destroy(gameObject);
    }
}
