using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 movement;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            movement = new Vector3(movement.x, -movement.y, movement.z);
        }
        else if (collision.tag == "PointTrigger")
        {
            GameManager.Instance.ScorePoint();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            float ballCenter = transform.position.y;
            float playerCenter = collision.gameObject.transform.position.y;
            float proportion = ballCenter - playerCenter;
            Vector3 movementMod = new Vector3();

            float maxSpeedX = 0.5f;
            if (movement.x > maxSpeedX)
            {
                movement.x = maxSpeedX;

            }

            if (proportion < -0.8)
            {
                movementMod = new Vector3(-movement.x * 1.15f, 0.1f);
            }
            else if (proportion < -0.5)
            {
                movementMod = new Vector3(-movement.x * 1.1f, 0.05f);
            }
            else if (proportion < -0.2)
            {
                movementMod = new Vector3(-movement.x * 1.05f, 0.01f);
            }
            else if (proportion < 0.2)
            {
                movementMod = new Vector3(-movement.x * 1.05f, -0.01f);
            }
            else if (proportion < 0.5)
            {
                movementMod = new Vector3(-movement.x * 1.1f, -0.05f);
            }
            else
            {
                movementMod = new Vector3(-movement.x * 1.15f, -0.1f);
            }

            movement = movementMod;
        }
    }
}
