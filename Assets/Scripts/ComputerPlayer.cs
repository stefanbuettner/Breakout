using System.Collections;
using UnityEngine;

public class ComputerPlayer : Player
{
    private Paddle paddle;

    void Awake()
    {
        /* In case it was not set in the editor. */
        if (paddle == null)
            paddle = FindObjectOfType<Paddle>();
        GetComponent<ConfigurableJoint>().connectedBody = paddle.GetComponent<Rigidbody>();
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
    }

	public void OnBallDestroyed(Ball ball)
	{
		ball.OnDestroyEvent -= OnBallDestroyed;
	}

	IEnumerator ShootBall(float delay)
	{
		yield return new WaitForSeconds(delay);
		Ball ball = paddle.ShootBall();
		ball.OnDestroyEvent += OnBallDestroyed;
	}
}
