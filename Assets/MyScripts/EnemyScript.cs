using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public GameObject player;
    public GameObject enemyHead;

    private BoxCollider2D enemyBoxCollider;
    private BoxCollider2D playerBoxCollider;
    private BoxCollider2D enemyHeadBoxCollider;

    private bool collision = false;
    private float waitForNextCollision = 1.0f;

    public GameObject enemyProjectilePrefab;
    private float shootProjectile = 2.0f;

    void Start () 
    {
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        playerBoxCollider = player.gameObject.GetComponent<BoxCollider2D>();
        enemyHeadBoxCollider = enemyHead.gameObject.GetComponent<BoxCollider2D>();
    }

	void Update () 
    {
        // shootProjectile float is a timer for the enemy to shoot a projectile every 2 seconds
        shootProjectile -= Time.deltaTime;

        if (shootProjectile < 0f)
        {
            InstantiateEnemyProjectile(EnemyMovement.enemyDirection);
            shootProjectile = 2.0f;
        }


        if (playerBoxCollider.IsTouching(enemyBoxCollider) && !collision)
        {
            PlayerScript.health--;
            collision = true;
        }

        // collision bool is made to prevent multiple collisions, player can loose only 1 heart per a second for collision with enemy
        if (collision)
        {
            waitForNextCollision -= Time.deltaTime;
            if (waitForNextCollision < 0f)
            {
                waitForNextCollision = 1.0f;
                collision = false;
            }
        }

        if (playerBoxCollider.IsTouching(enemyHeadBoxCollider) || transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }


    ProjectileScript InstantiateEnemyProjectile(int dir)
    {
        ProjectileScript newProjectile = Instantiate(enemyProjectilePrefab, new Vector3(transform.position.x + dir, transform.position.y), Quaternion.identity).GetComponent<ProjectileScript>();
        newProjectile.setDiretction(dir);
        return newProjectile;
    }
}
