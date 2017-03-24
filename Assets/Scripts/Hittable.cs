using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Hittable : MonoBehaviour
{
	public delegate void BallHitNotification(Ball ball, Hittable hit);
}
