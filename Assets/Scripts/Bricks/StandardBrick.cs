using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StandardBrick : Brick
{
	[SerializeField]
	private int hitPoints = 1;

	public override int GetHitpoints()
	{
		return hitPoints;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Ball"))
		{
			gameObject.SetActive(false);
			RaiseBallHit(col.gameObject.GetComponent<Ball>(), this);
		}
	}
}
