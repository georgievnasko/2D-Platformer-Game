using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour {

    private int direction = 1;

    public int getDirection()
    {
        return direction;
    }

    public void MoveLeft(Vector2 pos, float movingSpeed)
    {
        transform.position = new Vector2(pos.x - Time.deltaTime * movingSpeed, pos.y);

        direction = -1;
    }

    public void MoveRight(Vector2 pos, float movingSpeed)
    {
        transform.position = new Vector2(pos.x + Time.deltaTime * movingSpeed, pos.y);

        direction = 1;
    }

    public abstract void OnCollisionEnter2D(Collision2D collision);
}
