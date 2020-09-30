using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public static int health;
    public static bool playing;
    public static bool finished;

    public GameObject playerProjectilePrefab;
    public GameObject mainCamera;

    void Start()
    {
        health = 3;
        playing = true;
        finished = false;
    }

    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.E) && playing)
        {
            InstantiatePlayerProjectile(PlayerMovement.playerDirection);
        }

        if (health == 0 || transform.position.y < -5f)
        {
            playing = false;
            mainCamera.transform.parent = null;
        }
    }

    ProjectileScript InstantiatePlayerProjectile(int dir)
    {
        ProjectileScript newProjectile = Instantiate(playerProjectilePrefab, new Vector3(transform.position.x + dir, transform.position.y), Quaternion.identity).GetComponent<ProjectileScript>();
        newProjectile.setDiretction(dir);
        return newProjectile;
    }
}
