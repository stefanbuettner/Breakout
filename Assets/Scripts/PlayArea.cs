using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayArea : Hittable
{
	public event BallHitNotification OnBallExit;

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Ball"))
		{
			Debug.Log("PlayArea Ball entered");
			RaiseBallHit(col.GetComponent<Ball>(), this);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Ball"))
		{
			Debug.Log("PlayArea ball exited");
			OnBallExit(col.GetComponent<Ball>(), this);
		}
	}
}
