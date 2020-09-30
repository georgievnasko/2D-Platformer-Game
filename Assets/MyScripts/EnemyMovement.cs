using UnityEngine;

public class EnemyMovement : CharacterMovement {

    private float timeToMove = 3.0f;
    private float movingSpeed = 1.5f;
    private bool changeDirection = false;

    public static int enemyDirection = 1;

    public GameObject enemyEye;
    public GameObject enemyMouth;

    public GameObject particleEffectPrefab;

    private Vector2 enemyEyePositionRight;
    private Vector2 enemyEyePositionLeft;
    private Vector2 enemyMouthPositionRight;
    private Vector2 enemyMouthPositionLeft;


    void Start () {
        enemyEyePositionRight = new Vector2(0.04f, 0.025f);
        enemyEyePositionLeft = new Vector2(-0.04f, 0.025f);
        enemyMouthPositionRight = new Vector2(0.04f, -0.025f);
        enemyMouthPositionLeft = new Vector2(-0.04f, -0.025f);
    }

    void Update () 
    {
        enemyDirection = getDirection();

        // timeToMove float is a timer for the enemy to change its direction randomly
        timeToMove -= Time.deltaTime;

        if (!changeDirection)
        {
            MoveLeft(transform.position, movingSpeed);

            enemyEye.transform.localPosition = enemyEyePositionLeft;
            enemyMouth.transform.localPosition = enemyMouthPositionLeft;
        }
        else
        {
            MoveRight(transform.position, movingSpeed);

            enemyEye.transform.localPosition = enemyEyePositionRight;
            enemyMouth.transform.localPosition = enemyMouthPositionRight;
        }

        if (timeToMove < 0f)
        {
            changeDirection = !changeDirection;
            timeToMove = Random.Range(1, 5);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerProjectile(Clone)")
        {
            GameObject part = Instantiate(particleEffectPrefab, collision.transform.position, Quaternion.identity);
            Destroy(part, part.gameObject.GetComponent<ParticleSystem>().main.duration);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "EnemyProjectile(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }
}
