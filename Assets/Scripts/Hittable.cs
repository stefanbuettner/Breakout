using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Hittable : MonoBehaviour
{
	public delegate void BallHitNotification(Ball ball, Hittable hit);

	public event BallHitNotification OnBallHit;

	protected virtual void RaiseBallHit(Ball ball, Hittable hitObject)
	{
		BallHitNotification handler = OnBallHit;
		if (handler != null)
		{
			Debug.Log("Ball hit " + name);
			handler(ball, hitObject);
		}
	}
}
