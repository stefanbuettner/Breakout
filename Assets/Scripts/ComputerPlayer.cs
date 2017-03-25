using System.Collections;
using UnityEngine;

public class ComputerPlayer : Player
{
    private Paddle paddle;
    private Ball ball;
    public float maxPaddleOffset;
    private float paddleOffset;

    void Awake()
    {
        /* In case it was not set in the editor. */
        if (paddle == null)
            paddle = FindObjectOfType<Paddle>();
        GetComponent<ConfigurableJoint>().connectedBody = paddle.GetComponent<Rigidbody>();
        paddleOffset = Random.Range(-maxPaddleOffset, maxPaddleOffset);
    }

    void OnEnable()
    {
        paddle.OnHit += PaddleHit;
    }

    void OnDisable()
    {
        paddle.OnHit -= PaddleHit;
    }

    public override void LevelReset()
    {
        paddle.LevelReset();
		StartCoroutine(ShootBall(1.0f));
    }

    public override void TurnReset()
    {
        paddle.TurnReset();
		StartCoroutine(ShootBall(1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (ball)
        {
            transform.position = new Vector3(ball.transform.position.x - paddleOffset, 0.0f, 0.0f);
        }
    }

	public void OnBallDestroyed(Ball ball)
	{
		ball.OnDestroyEvent -= OnBallDestroyed;
        ball = null;
	}

	IEnumerator ShootBall(float delay)
	{
		yield return new WaitForSeconds(delay);
		ball = paddle.ShootBall(new Vector3(Random.Range(-1f, 1f), 1.0f, 0.0f));
		ball.OnDestroyEvent += OnBallDestroyed;
	}

    public void PaddleHit(GameObject go)
    {
        if (go.GetComponent<Ball>() != null)
        {
            paddleOffset = Random.Range(-maxPaddleOffset, maxPaddleOffset);
            Debug.Log("New paddle offset " + paddleOffset);
        }
    }
}
