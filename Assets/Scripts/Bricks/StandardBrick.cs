using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StandardBrick : MonoBehaviour
{
	private BrickManager gameControl;
	public int hitPoints = 1;

	void Start()
	{
		gameControl = FindObjectOfType<BrickManager>();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.CompareTag("Ball"))
		{
			gameObject.SetActive(false);
			gameControl.BrickHit(hitPoints, col.gameObject.GetComponent<Ball>());
		}
	}
}
