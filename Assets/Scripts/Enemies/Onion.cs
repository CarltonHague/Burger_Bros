using UnityEngine;

public class Onion : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    //other objects and prefabs
    public GameObject player;

    //patroll
    public float speed=1;
    public float patrolRange = 8f;
    private float posOnPatrolRange = 0f;
    private Vector3 patrolDirection = Vector2.right;
    private float distance;
    private Vector2 direction;
    public float aggroDistance=1;

    //death
    public float deathDelay = 0.1f;

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); //calculate distance from player
        if (distance < aggroDistance)   //want to be aggro
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);//move enemy
                
        }
        else   //default  patrol
        {
            if (posOnPatrolRange > patrolRange || posOnPatrolRange < 0)  //if above or below range
            {
                patrolDirection.x *= -1;  //flip direvction
            }
            transform.position += patrolDirection * speed * Time.deltaTime;  //move
            posOnPatrolRange += patrolDirection.x * speed * Time.deltaTime;  //keep track of relative to range pos
            
        }
        if (patrolDirection.x > 0)  //flip sprite based on direction
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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
