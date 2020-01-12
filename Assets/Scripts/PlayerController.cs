using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerName;
    public float moveSpeed = 0.1f;
    public KeyCode upKey;
    public KeyCode downKey;
    public bool isBot;

    private bool isInWall = false;
    private BallController ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (!isBot)
            {
                if (!isInWall)
                {
                    if (Input.GetKey(upKey))
                    {
                        MoveUp();
                    }
                    else if (Input.GetKey(downKey))
                    {
                        MoveDown();
                    }
                }
                else
                {
                    if (transform.position.y < 0)
                    {
                        MoveUp();
                    }
                    else
                    {
                        MoveDown();
                    }
                }
            }
            else
            {
                float ballY = ball.gameObject.transform.position.y;
                if (ballY > transform.position.y + transform.localScale.y - 0.45f)
                {
                    MoveUp();
                }
                else if (ballY < transform.position.y - transform.localScale.y + 0.45f)
                {
                    MoveDown();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isInWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isInWall = false;
        }
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0f, moveSpeed, 0);
    }

    public void MoveDown()
    {
        transform.position -= new Vector3(0f, moveSpeed, 0);
    }
}
