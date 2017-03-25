using UnityEngine;

public class Paddle : MonoBehaviour
{
	public GameObject ballPrefab;

	public Vector3 initialBallPosition = new Vector3(0f, 1.5f, 0f);
	[SerializeField]
    private float defaultShotSpeed = 8f;
	[HideInInspector]
    public float shotSpeed;
	private Ball ballToShoot;
	public float paddleSpeed = 2f;
	private float initialWidth;

	void Awake()
	{
		initialWidth = transform.localScale.x;
	}

	public void LevelReset()
	{
		shotSpeed = defaultShotSpeed;
		Vector3 newScale = transform.localScale;
		newScale.x = initialWidth;
		transform.localScale = newScale;
		SpawnNewBall();
	}

	public void TurnReset()
	{
		SpawnNewBall();
	}

    public void SpawnNewBall()
    {
        if (ballToShoot != null)
            Destroy(ballToShoot.gameObject);

        GameObject newBall = GameObject.Instantiate(ballPrefab, transform) as GameObject;
        newBall.transform.localPosition = initialBallPosition;
        ballToShoot = newBall.GetComponent<Ball>();
    }

	public void ScalePaddleWidth(float factor)
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(currentScale.x * factor, currentScale.y, currentScale.z);
    }

	public Ball ShootBall()
	{
		Ball shotBall = null;
		if (ballToShoot != null)
		{
			shotBall = ballToShoot;
			ballToShoot.Shoot(Vector3.up * shotSpeed);
			ballToShoot = null;
		}
		return shotBall;
	}
}
