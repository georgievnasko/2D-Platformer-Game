using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    private float movingSpeed = 5.0f;
    private Vector2 startPosition;
    private int direction = 1;



    void Start () 
    {
        startPosition = transform.position;
	}

    public void setDiretction(int dir)
    {
        direction = dir;
    }


    void Update () 
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * movingSpeed * direction, transform.position.y);

        if (transform.position.x > startPosition.x + 15f && direction == 1)
            Destroy(gameObject);
        else if (transform.position.x < startPosition.x - 15f && direction == -1)
            Destroy(gameObject);
    }
}
